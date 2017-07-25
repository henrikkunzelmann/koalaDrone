#pragma once

#include <Arduino.h>

#include "Sensor.h"
#include "LED.h"
#include "MathHelper.h"
#include "SensorCalibration.h"
#include "Log.h"
#include "Profiler.h"
#include "FaultManager.h"
#include "Timer.h"

struct IMUValues {
	float roll;
	float pitch;
	float yaw;
};

class SensorIMU : public Sensor
{
private:
	IMUValues values;
	CalibrationData calibration;

	boolean startCalibration(uint8_t* savedData, size_t length);
	boolean runCalibration(uint32_t ticks);

protected:
	virtual bool getValues(IMUValues* values) = 0;

	SensorUpdateError collectData();

	explicit SensorIMU(SensorHAL* hal, Config* config);
public:
	virtual ~SensorIMU();

	float getRoll() const;
	float getPitch() const;
	float getYaw() const;

	bool isFlat() const;
};

