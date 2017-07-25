#pragma once

#include <Arduino.h>

#include "SensorIMU.h"
#include "Build.h"
#include "Gyro.h"

class MadgwickIMU : public SensorIMU
{
private:
	float roll;
	float pitch;
	float yaw;

	float beta;				// algorithm gain
	float q0;
	float q1;
	float q2;
	float q3; // quaternion of sensor frame relative to auxiliary frame

	void updateIMU(float gx, float gy, float gz, float ax, float ay, float az);
	float invSqrt(float x);
	void computeAngles();

protected:
	bool getValues(IMUValues* values);
public:
	explicit MadgwickIMU(SensorHAL* hal, Config* config);

	const char* getName() const;
	const char* getShortName() const;
	boolean isHardwareBased() const;

	boolean init();
	boolean disable();
};