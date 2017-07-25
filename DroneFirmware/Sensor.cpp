#include "Sensor.h"

Sensor::Sensor(SensorHAL* hal, Config* config, uint32_t interval, uint32_t invalidInterval)
{
	this->hal = hal;
	this->config = config;
	this->interval = interval;
	this->invalidInterval = invalidInterval;
	this->wrongDataTicks = 0;
	this->updateTimer = new Timer(interval);
}

Sensor::~Sensor()
{
}

boolean Sensor::update()
{
	if (!updateTimer->shouldTick())
		return true;

	boolean beforeUpdateOK = wasDataOK();
	SensorUpdateError updateError = collectData();

	// Check if data is ok
	dataOK = (updateError != UpdateError);
	yield();

	if (dataOK) {
		// Check the sensor is currently calibrating
		if (calibrationRunning) {
			blinkLED(2, 1000);

			if (runCalibration(calibrationTicks++)) {
				Log::info(getShortName(), "Calibration is done");
				// done with calibration
				calibrated = true;
				calibrationRunning = false;
			}
			yield();
		}
	}
	
	// data has not changed or error occured
	if (updateError != UpdateOK) {
		if (wrongDataTicks < UINT32_MAX) // prevent overflow
			wrongDataTicks++;

		if (calibrationRunning) {
			calibrationWrongDataCount++;

			// when 1/4 of the data is wrong using the invalidInterval
			uint64_t time = (uint64_t)calibrationWrongDataCount * (uint64_t)interval;
			if (time > (uint64_t)invalidInterval / 4) {
				// Calibration was not successful because too much data was wrong
				Log::error(getShortName(), "Sensor: Calibration stopped because too much data is wrong");
				calibrated = false;
				calibrationRunning = false;
			}
		}
	}
	else 
		wrongDataTicks = 0;

	if (!wasDataOK()) {
		yield();
		if (beforeUpdateOK) {
			if (updateError == UpdateNotChanged)
				Log::error(getShortName(), "Sensor: data no longer ok, last reason: data not changed");
			else
				Log::error(getShortName(), "Sensor: data no longer ok, last reason: error");
		}

		// if too much data is wrong try resetting sensor
		if (millis() - lastReset > SENSOR_RESET_INTERVAL) {
			Log::error(getShortName(), "Sensor: try init(), data is not ok");
			if (!init())
				Log::error(getShortName(), "Sensor: init() was not successful");
			lastReset = millis();
		}
		yield();
	}

	yield();
	return dataOK;
}

uint32_t Sensor::convertTimeToTicks(uint32_t time)
{
	return (time / interval);
}

void Sensor::calibrate(uint8_t* savedData, size_t length)
{
	Log::info(getShortName(), "calibrate(length = %d)", length);
	calibrated = false;
	calibrationRunning = true;
	calibrationTicks = 0;
	calibrationWrongDataCount = 0;

	// check if the saved data could be loaded as calibration data
	if (startCalibration(savedData, length)) {
		Log::info(getShortName(), "Calibration loaded from saved data");
		calibrated = true;
	}
}

void Sensor::stopCalibration()
{
	calibrationRunning = false;
}

boolean Sensor::isDataOK() const
{
	return dataOK;
}

boolean Sensor::wasDataOK() const
{
	// wrongDataTicks may go to UINT32_MAX thus overflowing invalidTime
	uint64_t invalidTime = (uint64_t)wrongDataTicks * (uint64_t)interval;
	return invalidTime < (uint64_t)invalidInterval;
}

boolean Sensor::isCalibrated() const
{
	return calibrated;
}

boolean Sensor::isCalibrating() const
{
	return calibrationRunning;
}
