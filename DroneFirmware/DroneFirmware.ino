#include <Wire.h>
#include <I2Cdev.h>
#include <WiFiUdp.h>
#include <ESP8266WiFi.h>
#include <Servo.h>
#include <EEPROM.h>
#include <MPU6050.h>
#include <MPU9250.h>
#include <BME280.h>

#include "Build.h"
#include "Hardware.h"
#include "Log.h"
#include "NetworkManager.h"
#include "Config.h"
#include "ConfigManager.h"
#include "EEPROM_MemoryAdapter.h"
#include "MemoryAdapter.h"
#include "MathHelper.h"
#include "DroneEngine.h"
#include "ServoManager.h"
#include "BinaryHelper.h"
#include "LED.h"
#include "VoltageReader.h"
#include "Profiler.h"
#include "SensorHAL.h"
#include "Gyro.h"
#include "Gyro6050.h"
#include "Gyro9250.h"
#include "PID.h"
#include "CycleTimes.h"
#include "Baro.h"
#include "Baro280.h"

Config config;

VoltageReader* voltageReader;
SensorHAL* sensor;

ServoManager* servos;
DroneEngine* engine;
NetworkManager* network;

void setup() {
	Serial.begin(115200);
	Serial.println();

	uint64_t heapBefore = Hardware::getFreeHeap();

	Log::emptyLine();
	Log::info("Boot", "=====================");
	Log::info("Boot", "Drone v%d booting...", BUILD_VERSION);
	Log::info("Boot", "Model: %s", MODEL_NAME);
	Log::info("Boot", "Build: %s", BUILD_NAME);

	Log::emptyLine();
	Log::info("Hardware", "Hardware: %s", Hardware::getName());
	Log::info("Hardware", "Arduino version: %d, core: %s", ARDUINO_VERSION, Hardware::getArduinoCoreVersion());
	Log::info("Hardware", "SDK version: %s", Hardware::getSDKVersion());
	Log::info("Hardware", "CPU freq: %u mhz", Hardware::getCPUFrequency());
	Log::info("Hardware", "Flash size: %u kbyte", Hardware::getFlashSize() / 1024u);

	Log::emptyLine();
	Log::info("Memory", "Free heap (before boot): %lu", heapBefore);

	rst_info* resetInfo = ESP.getResetInfoPtr();
	Log::emptyLine();
	Log::debug("Boot", "Reset info: r: %u, ex: %u   0x%x, 0x%x, 0x%x, 0x%x, 0x%x", 
		resetInfo->reason, resetInfo->exccause,
		resetInfo->epc1, resetInfo->epc2, resetInfo->epc3, resetInfo->excvaddr, resetInfo->depc);

	ESP.eraseConfig();

	// Serialnummer schreiben
	char serialCode[32];
	getBuildSerialCode(serialCode, sizeof(serialCode));
	Log::emptyLine();
	Log::info("Boot", "Serial code: %s", serialCode);

	config = ConfigManager::loadConfig();

	// Log setzen
	Log::setPrintToSerial(config.VerboseSerialLog);

	// ServoManager initialisieren
	servos = new ServoManager(&config);

	setupLED(&config);

	bool saveConfig = false;
	// Calibrate servos
	if (config.CalibrateServos) {
		Log::info("Boot", "Calibration of servos...");

		if (resetInfo->reason == REASON_DEFAULT_RST || resetInfo->reason == REASON_EXT_SYS_RST) {
			turnLedOn();
			servos->calibrate();
			turnLedOff();
			Log::info("Boot", "Done with calibration");

			blinkLED(5, 1000);
		}
		else
		{

			Log::error("Boot", "Invalid reset reason: %u", resetInfo->reason);
			blinkLED(10, 500);
		}

		config.CalibrateServos = false;
		saveConfig = true;
	}

	// Attach servos
	servos->attach();

	Log::emptyLine();
	Log::info("Boot", "Init network...");
	bool openOwnNetwork = true;

	// Hostname generieren
	char name[30];
	strncpy(name, config.DroneName, sizeof(name));
	strncat(name, "-", sizeof(name));
	strncat(name, serialCode, sizeof(name));

	// WiFi Einstellungen setzen
	WiFi.persistent(false);
	WiFi.hostname(name);
	WiFi.setOutputPower(20.5f);
	WiFi.setPhyMode(WIFI_PHY_MODE_11N);

	// versuchen mit dem in der Config gespeicherten AP zu verbinden
	if (strlen(config.NetworkSSID) > 0) {
		Log::info("Boot", "Trying to connect to %s", config.NetworkSSID);

		WiFi.mode(WIFI_STA);
		WiFi.setAutoReconnect(true);
		if (WiFi.begin(config.NetworkSSID, config.NetworkPassword) == WL_CONNECT_FAILED)
			Log::error("Boot", "WiFi.begin() failed");

		// auf Verbindung warten
		openOwnNetwork = WiFi.waitForConnectResult() != WL_CONNECTED;

		if (openOwnNetwork) {
			Log::error("Boot", "Could not connect to the access point!");
			Log::error("Boot", "Status: %d", WiFi.status());
		}
		else {
			Log::info("Boot", "Successfully connected to access point");
			Log::info("Boot", "IP address: %s", WiFi.localIP().toString().c_str());
		}
	}

	// eigenen AP erstellen
	if (openOwnNetwork) {
		Log::info("Boot", "Creating own network...");
		Log::info("Boot", "Network SSID: %s", name);

		WiFi.mode(WIFI_AP);
		WiFi.softAP(name, config.AccessPointPassword, 8);

		Log::info("Boot", "AP IP address: %s", WiFi.softAPIP().toString().c_str());
	}


	// I2C initialisieren
	Log::emptyLine();
	Log::info("Boot", "Init I2C...");
	Wire.begin(SDA, SCL);
	Wire.setClock(400000L);

	// Batterie Voltage Reader laden
	Log::info("Boot", "Init voltage reader...");
	voltageReader = new VoltageReader(A0, 16.894f);

	Log::info("Boot", "Init sensor hal...");
	sensor = new SensorHAL(&config);

	Log::info("Boot", "Init drone engine...");
	engine = new DroneEngine(sensor, servos, &config);

	// Netzwerkmanager starten
	Log::info("Boot", "Init network manager...");
	network = new NetworkManager(sensor, servos, engine, &config, voltageReader);
	if (saveConfig)
		network->beginSaveConfig();

	// Profiler laden
	Profiler::init();

	uint64_t heapAfter = Hardware::getFreeHeap();
	Log::emptyLine();
	Log::info("Memory", "Free heap (after boot): %lu", heapAfter);

	Log::emptyLine();
	Log::info("Boot", "done booting. ready.");
}

void loop() {
	Profiler::begin("loop()");

	if (WiFi.getMode() == WIFI_STA && !WiFi.isConnected() && engine->state() != StateStopped)
		engine->stop(WifiDisconnect);

	network->handlePackets();
	yield();

	if (engine->state() != StateOTA)
	{
		sensor->update();
		yield();

		engine->handle();
		yield();
	}
	handleBlink();

	if (engine->state() == StateArmed)
		servos->handleTick();


	network->handleData();
	Profiler::end();
	Profiler::finishFrame();
}

