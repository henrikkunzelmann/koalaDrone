#pragma once

#include <Arduino.h>
#include <Wire.h>
#include <I2Cdev.h>
#include <MPU9250.h>

#include "Build.h"
#include "Gyro.h"
#include "CycleTimes.h"

class Gyro9250 : public Gyro
{
protected:
	bool mpuOK;

	MPU9250 mpu;
	double accRes;
	double gyroRes;
	double magRes;

	float sx, sy, sz;

	bool setSettings();
	bool getValues(GyroValues* values);
public:
	explicit Gyro9250(SensorHAL* hal, Config* config);

	const char* getName() const;
	const char* getShortName() const;
	const char* getMagnetometerName() const;
	boolean isHardwareBased() const;
	boolean disable();

	boolean init();

	bool hasMagnetometer() const;
	bool hasIMU() const;
};