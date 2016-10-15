#ifndef _GYRO6050_h
#define _GYRO6050_h

#include "Build.h"
#include "Gyro.h"
#include "CycleTimes.h"

#include <Wire.h>
#include <I2Cdev.h>
#include <MPU6050.h>

class Gyro6050 : public Gyro
{
protected:
	bool mpuOK;

	MPU6050 mpu;
	double accRes;
	double gyroRes;

	bool getValues(GyroValues* values);
public:
	explicit Gyro6050(Config* config);

	const char* name();
	const char* magnetometerName();

	bool init();
	void reset();
	void resetMagnet();

	bool hasMagnetometer() const;
	bool hasIMU() const;
};

#endif