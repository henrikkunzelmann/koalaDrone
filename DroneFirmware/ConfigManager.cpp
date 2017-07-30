#include "ConfigManager.h"

Config ConfigManager::loadConfig() {
	MemoryAdapter* adapter = NULL;
#if MEMORY_I2C_ENABLE
	adapter = new EEPROMMemoryAdapter();
#endif
#if HARDWARE_ESP8266
	adapter = new FlashMemoryAdapter(1024, 64);
#endif

	if (adapter != NULL) {
		Config config;
		if (adapter->begin()) {
			config = loadConfig(adapter);
			adapter->end();
		}
		else {
			Log::error("Config", "Error while loading config");
			config = getDefault();
		}
		
		delete adapter;
		return config;
	}
	else {
		Log::info("Config", "Loading config not supported on this hardware");
		return getDefault();
	}
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

	Profiler::begin("saveConfig()");
	
	MemoryAdapter* adapter = NULL;
#if MEMORY_I2C_ENABLE
	adapter = new EEPROMMemoryAdapter();
#endif
#if HARDWARE_ESP8266
	adapter = new FlashMemoryAdapter(1024, 64);
#endif

	if (adapter != NULL) {
		if (adapter->begin()) {
			saveConfig(adapter, config);
			adapter->end();
		}
		else 
			Log::error("Config", "Error while saving config");
		yield();

		delete adapter;
	}
	else {
		Log::info("Config", "Saving config not supported on this hardware");
	}
	Profiler::end();

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

	config.RollPid.Kp = 0.75f;
	config.RollPid.Ki = 0.11f;
	config.RollPid.Kd = 0;

	config.PitchPid.Kp = 0.8f;
	config.PitchPid.Ki = 0.11f;
	config.PitchPid.Kd = 0;

	config.YawPid.Kp = 4.2f;
	config.YawPid.Ki = 0.25f;
	config.YawPid.Kd = 0;

	config.InputScale = 164.0f;

	config.IgnoreSafeOrientationWhileFlying = true;
	config.SafeRoll = 60;
	config.SafePitch = 60;
	config.SafeServoValue = 1850;

	config.EnableStabilization = true;

	config.MaxThrustForFlying = 50;
	config.OnlyArmWhenStill = false;

	config.RollTrim = 0;
	config.PitchTrim = 0;
	config.YawTrim = 0;

	config.AngleRoll.Kp = 1.8f;
	config.AngleRoll.Ki = 0.03f;
	config.AngleRoll.Kd = 0;

	config.AnglePitch.Kp = 1.8f;
	config.AnglePitch.Ki = 0.03f;
	config.AnglePitch.Kd = 0;

	config.AngleYaw.Kp = 2.0f;
	config.AngleYaw.Ki = 0;
	config.AngleYaw.Kd = 0.15f;

	config.EnableImuAcc = true;
	config.EnableImuMag = false;
	config.GyroDLPF = 3; // 42 Hz
	config.GyroFilter = 0.7f;
	config.AccFilter = 0.3f;

	config.StabInputScale = 160.0f;

	config.PassThroughSerialNMEA = false;
	config.IgnoreGyroSelfTest = false;
	
	config.CalibrateServos = false;
	memset(&config.SensorCalibrationData, 0, sizeof(SensorCalibration));

	applyCustomSettings(&config);

	Log::info("Config", "Using default config");
	return config;
}
