#pragma once

#include <Arduino.h>

#include "Config.h"
#include "FaultManager.h"
#include "Gyro.h"
#include "Gyro6050.h"
#include "Gyro9250.h"
#include "Baro.h"
#include "Baro280.h"
#include "SensorCalibration.h"

class SensorHAL
{
private:
	Config* config;
	Gyro* gyro;
	Baro* baro;

	void initSensors();
	bool initGyro(Gyro* gyro);
	bool initBaro(Baro* baro);

public:
	explicit SensorHAL(Config* config);
	
	const char* getGyroName() const;
	const char* getMagnetometerName() const;
	const char* getBaroName() const;

	Gyro* getGyro() const;
	Baro* getBaro() const;
	SensorCalibration* getCalibrationData();

	void update();
};