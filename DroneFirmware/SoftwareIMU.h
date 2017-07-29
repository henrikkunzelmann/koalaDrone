#pragma once

#include <Arduino.h>

#include "SensorIMU.h"
#include "Build.h"
#include "Gyro.h"
#include "CycleTimes.h"



class SoftwareIMU : public SensorIMU
{
private:
	float roll;
	float pitch;
	float yaw;

	boolean firstAccSample;

protected:
	bool getValues(IMUValues* values);
public:
	explicit SoftwareIMU(SensorHAL* hal, Config* config);

	const char* getName() const;
	const char* getShortName() const;
	boolean isHardwareBased() const;

	boolean init();
	boolean disable();
};