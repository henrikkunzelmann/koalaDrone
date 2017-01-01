#ifndef _CONFIG_h
#define _CONFIG_h

#include "arduino.h"

#include "SensorCalibration.h"

struct PIDSettings {
	float Kp;
	float Ki;
	float Kd;
};

struct Config {
	//A user-friendly name for the drone
	char DroneName[20];

	bool SaveConfig;

	// Name des WiFi Netzwerks
	// leerer String bedeutet es wird keine Verbindung zu einem Netzwerk aufgebaut
	char NetworkSSID[30];

	//The password of the WiFi network
	char NetworkPassword[30];

	//The password for the WiFi access point
	char AccessPointPassword[30];

	//The UDP-Port for the hello packets
	uint16_t NetworkHelloPort;
	//The UDP-Port for the control packets
	uint16_t NetworkControlPort;
	//The UDP-Port for the data packets
	uint16_t NetworkDataPort;

	//The size of the buffer for incoming packets
	uint16_t NetworkPacketBufferSize;

	//The time, after when the drone stopps without any action sent
	uint32_t MaximumNetworkTimeout;

	//Toogles the debug output on Serial
	bool VerboseSerialLog;

	//The temperature, at which the drone starts to decent on turn off
	float MaxTemperature;

	//The minimum output value for the ESC's
	uint16_t ServoMin;
	//The maximum output value for the ESC's
	uint16_t ServoMax;
	//The output value for the ESC's, at which they start to turn
	uint16_t ServoIdle;

	//The pin of the front-left motor
	uint8_t PinFrontLeft;
	//The pin of the front-right motor
	uint8_t PinFrontRight;
	//The pin of the back-left motor
	uint8_t PinBackLeft;
	//The pin of the back-right motor
	uint8_t PinBackRight;
	//The pin of the LED
	uint8_t PinLed;

	PIDSettings PitchPid;
	PIDSettings RollPid;
	PIDSettings YawPid;

	float SafePitch;
	float SafeRoll;
	int32_t SafeServoValue;

	bool EnableStabilization;
	bool NegativeMixing;

	int32_t MaxThrustForFlying;
	bool OnlyArmWhenStill;

	PIDSettings AngleStabilization;

	// Debug Settings
	bool EnableImuAcc;
	bool EnableImuMag;
	float GyroFilter;
	float AccFilter;

	bool CalibrateServos;
	SensorCalibration SensorCalibrationData;
};

#endif

