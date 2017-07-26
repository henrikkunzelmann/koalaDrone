#pragma once

#include <Arduino.h>
#include <Wire.h>
#include <I2Cdev.h>
#include <MPU6050.h>

#include "Build.h"
#include "Gyro.h"
#include "CycleTimes.h"

class Gyro6050 : public Gyro
{
protected:
	bool mpuOK;

	MPU6050 mpu;
	double accRes;
	double gyroRes;

	bool setSettings();
	bool getValues(GyroValues* values);
public:
	explicit Gyro6050(SensorHAL* hal, Config* config);

	const char* getName() const;
	const char* getShortName() const;
	const char* getMagnetometerName() const;
	boolean isHardwareBased() const;

	boolean init();
	boolean disable();

	bool hasMagnetometer() const;
	bool hasIMU() const;
};