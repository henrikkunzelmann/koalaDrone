#ifndef _GYRO9250_h
#define _GYRO9250_h

#include "Build.h"
#include "Gyro.h"
#include "CycleTimes.h"

#include <Wire.h>
#include <I2Cdev.h>
#include <MPU9250.h>

class Gyro9250 : public Gyro
{
protected:
	bool mpuOK;

	MPU9250 mpu;
	double accRes;
	double gyroRes;
	double magRes;

	float sx, sy, sz;

	bool getValues(GyroValues* values);
public:
	explicit Gyro9250(SensorCalibration* calibration);

	char* name();
	char* magnetometerName();

	bool init();
	void reset();
	void resetMagnet();

	bool hasMagnetometer() const;
	bool hasIMU() const;
};

#endif