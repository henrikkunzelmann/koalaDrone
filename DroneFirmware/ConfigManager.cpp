#include "ConfigManager.h"

Config ConfigManager::loadConfig() {
#if HARDWARE_ESP8266
	EEPROM_MemoryAdapter* adapter = new EEPROM_MemoryAdapter(1024, 64);

	adapter->begin();
	Config config = loadConfig(adapter);
	adapter->end();

	delete adapter;
	return config;
#else
	Log::info("Config", "Loading config not supported on this hardware");
	return getDefault();
#endif
}

Config ConfigManager::loadConfig(MemoryAdapter* memory) {
	// wir nutzen erstes Byte um zu Erkennen ob schon Daten geschrieben wurden
	if (memory->readByte(0) != CONFIG_MAGIC_VALUE) {
		Log::info("Config", "Saved magic value does not match excepted magic value");
		return getDefault();
	}

	if (memory->readByte(1) != CONFIG_VERSION) {
		Log::info("Config", "Saved config version does not match excepted version");
		return getDefault();
	}

	// nach Magic Value folgt ein uint16_t für die Größe der Config
	uint8_t buffer[sizeof(uint16_t)];
	memory->read(2, buffer, sizeof(buffer));

	uint16_t size = BinaryHelper::readUint16(buffer, 0);

	// über die Größe erkennen wir ob sich die Structure geändert hat
	if (size != sizeof(Config)) {
		Log::info("Config", "Config size does not match saved size");
		return getDefault();
	}

	// nach der Größe folgen unsere eigentliche Daten
	Config* config = (Config*)malloc(sizeof(Config));
	memory->read(4, (uint8_t*)config, sizeof(Config));

	Log::info("Config", "Config loaded");
	return *config;
}

void ConfigManager::saveConfig(const Config config) {
#if HARDWARE_ESP8266
	Profiler::begin("saveConfig()");
	EEPROM_MemoryAdapter* adapter = new EEPROM_MemoryAdapter(1024, 64);

	adapter->begin();
	saveConfig(adapter, config);
	adapter->end();

	yield();

	delete adapter;
	Profiler::end();
#else
	Log::info("Config", "Saving config not supported on this hardware");
#endif
}

void ConfigManager::saveConfig(MemoryAdapter* memory, const Config config) {
	// Magic Value speichern
	memory->writeByte(0, CONFIG_MAGIC_VALUE);

	memory->writeByte(1, CONFIG_VERSION);

	// Größe der Config Structure speichern
	uint8_t buffer[sizeof(uint16_t)];
	BinaryHelper::writeUint16(buffer, 0, sizeof(Config));
	memory->write(2, buffer, sizeof(buffer));

	// eigentliche Daten speichern
	memory->write(4, (uint8_t*)(&config), sizeof(Config));

	Log::info("Config", "Config saved");
}

Config ConfigManager::getDefault() {
	Config config;

	strncpy(config.DroneName, "koalaDrone", sizeof(config.DroneName));
	config.SaveConfig = true;

	strncpy(config.NetworkSSID, "", sizeof(config.NetworkSSID));
	strncpy(config.NetworkPassword, "", sizeof(config.NetworkPassword));

	strncpy(config.AccessPointPassword, "12345678", sizeof(config.AccessPointPassword));

	config.NetworkHelloPort = 4710;
	config.NetworkControlPort = 4711;
	config.NetworkDataPort = 4712;
	config.NetworkPacketBufferSize = 1024;
	config.MaximumNetworkTimeout = 1000;

	config.VerboseSerialLog = true;
	config.MaxTemperature = 60;

	config.ServoMin = DEFAULT_SERVO_MIN;
	config.ServoMax = DEFAULT_SERVO_MAX;
	config.ServoIdle = DEFAULT_SERVO_IDLE;

	config.RollPid.Kp = 0.8f;
	config.RollPid.Ki = 0;
	config.RollPid.Kd = 0;

	config.PitchPid.Kp = 0.8f;
	config.PitchPid.Ki = 0;
	config.PitchPid.Kd = 0;

	config.YawPid.Kp = 3.5f;
	config.YawPid.Ki = 0;
	config.YawPid.Kd = 0;

	config.InputScale = 164.0f;

	config.SafeRoll = 100;
	config.SafePitch = 100;
	config.SafeServoValue = 1850;

	config.EnableStabilization = false;
	config.NegativeMixing = true;

	config.MaxThrustForFlying = 50;
	config.OnlyArmWhenStill = false;

	config.AngleStabilization.Kp = 2.5f;
	config.AngleStabilization.Ki = 0;
	config.AngleStabilization.Kd = 0.15f;

	config.EnableImuAcc = true;
	config.EnableImuMag = false;
	config.GyroFilter = 0.6f;
	config.AccFilter = 0.2f;

	config.StabOnlyHelp = true;
	config.StabInputScale = 40.0f;

	config.YawCorrectionFactor = 2.0f;
	config.YawMaxCorrection = 50.0f;

	config.PassThroughSerialNMEA = false;
	
	config.CalibrateServos = false;
	memset(&config.SensorCalibrationData, 0, sizeof(SensorCalibration));

	Log::info("Config", "Using default config");
	return config;
}
