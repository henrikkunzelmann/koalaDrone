#pragma once

#include <Arduino.h>

#include "Config.h"
#include "FaultManager.h"
#include "Gyro.h"
#include "Gyro6050.h"
#include "Gyro9250.h"
#include "SensorIMU.h"
#include "SoftwareIMU.h"
#include "Baro.h"
#include "Baro280.h"
#include "Position.h"
#include "PositionNMEA.h"
#include "SensorCalibration.h"

class SensorHAL
{
private:
	Config* config;
	Gyro* gyro;
	SensorIMU* imu;
	Baro* baro;
	Position* position;

	void initSensors();
	bool initGyro(Gyro* gyro);
	bool initIMU(SensorIMU* imu);
	bool initBaro(Baro* baro);
	bool initPosition(Position* position);

public:
	explicit SensorHAL(Config* config);
	
	const char* getGyroName() const;
	const char* getMagnetometerName() const;
	const char* getBaroName() const;
	const char* getPositionName() const;

	Gyro* getGyro() const;
	SensorIMU* getIMU() const;
	Baro* getBaro() const;
	Position* getPosition() const;
	SensorCalibration* getCalibrationData();

	void update();
};