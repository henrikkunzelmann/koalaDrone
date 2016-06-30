#ifndef _GYRO6050_h
#define _GYRO6050_h
#include "Build.h"
#include "Gyro.h"
#include "CycleTimes.h"

#include <Wire.h>
#include <MPU6050_6Axis_MotionApps20.h>
#include <I2Cdev.h>

class Gyro6050 : public Gyro
{
protected:
	bool mpuOK;

	MPU6050 mpu;
	float accRes;
	float gyroRes;

	void getValues(GyroValues* values);
public:
	explicit Gyro6050(Config* config);

	char* name();
	char* magnetometerName();

	bool init();
	void reset();

	float getTemperature();

	bool hasMagnetometer();
	bool hasIMU();
};

#endif