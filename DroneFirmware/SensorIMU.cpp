#include "SensorIMU.h"

SensorIMU::SensorIMU(SensorHAL* hal, Config* config) : Sensor(hal, config, CYCLE_GYRO, 1000)
{
}

SensorIMU::~SensorIMU()
{
}

boolean SensorIMU::startCalibration(uint8_t * savedData, size_t length)
{
	clearCalibrationData(&calibration);
	return false;
}

boolean SensorIMU::runCalibration(uint32_t ticks)
{
	if (ticks > convertTimeToTicks(1000))
		return true;

	updateCalibrationData(&calibration, values.roll, values.pitch, values.yaw, false);
	return false;
}

SensorUpdateError SensorIMU::collectData()
{
	if (!getValues(&values))
		return UpdateError;

	if (abs(values.roll) > 200 || abs(values.pitch) > 200 || abs(values.yaw) > 500) {
		Log::error("SensorIMU", "Invalid values... roll: %f, pitch: %f, yaw: %f", values.roll, values.pitch, values.yaw);
		return UpdateError;
	}
	
	if (isCalibrated()) {
		values.roll -= calibration.average[0];
		values.pitch -= calibration.average[1];
		values.yaw -= calibration.average[2];
	}

	return UpdateOK;
}

float SensorIMU::getRoll() const
{
	return values.roll;
}

float SensorIMU::getPitch() const
{
	return values.pitch;
}

float SensorIMU::getYaw() const
{
	return values.yaw;
}

bool SensorIMU::isFlat() const
{
	return abs(getRoll()) < 1.0f && abs(getPitch()) < 1.0f;
}