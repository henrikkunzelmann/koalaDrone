#pragma once

#include <Arduino.h>

#include "LED.h"
#include "Log.h"
#include "Config.h"
#include "Timer.h"
#include "SensorUpdateError.h"
#include "CalibrationData.h"

/// Interval between sensor resets in milliseconds
#define SENSOR_RESET_INTERVAL 10000 // 10 seconds

class SensorHAL;

/// Abstract class for a sensor attached to the drone.
class Sensor {
private:
	boolean dataOK = false;
	boolean calibrated = false;

	boolean calibrationRunning = false;
	uint32_t calibrationTicks = 0;
	uint32_t calibrationWrongDataCount = 0;
	uint32_t wrongDataTicks = 0;
	uint32_t lastReset = 0;

	uint32_t interval = 0;
	uint32_t invalidInterval = 0;

protected:
	SensorHAL* hal;
	Config* config;
	Timer* updateTimer;

	/// Forces the sensor to receive new data and returns true when valid data was received or false when an error occurred or invalid data was received.
	virtual SensorUpdateError collectData() = 0;

	/// Starts the calibration for the sensor and returns true when the calibation was loaded.
	virtual boolean startCalibration(uint8_t* savedData, size_t length) = 0;

	/// Runs the calibration and returns true when the calibation is finished.
	virtual boolean runCalibration(uint32_t ticks) = 0;

	/// interval is the time between each updates. invalidInterval is the time that has to pass for wasDataOK() to return false.
	explicit Sensor(SensorHAL* hal, Config* config, uint32_t interval, uint32_t invalidInterval);
public:
	virtual ~Sensor();

	/// Returns the name of the sensor.
	virtual const char* getName() const = 0;
	/// Returns the short name of the sensor used for logging.
	virtual const char* getShortName() const = 0;
	/// Returns true when the sensor is hardware based or false when when the sensor is software based.
	virtual boolean isHardwareBased() const = 0;

	/// Initializes the sensor. Returns true when the initialization process was successfully.
	virtual boolean init() = 0;
	/// Disables the sensor.
	virtual boolean disable() = 0;

	// Converts the time in milliseconds in ticks used by the update timer
	uint32_t convertTimeToTicks(uint32_t time);

	/// Begins the calibration of the sensor. savedData is the last collected calibration data of the sensor. It can be NULL if no data was saved.
	void calibrate(uint8_t* savedData, size_t length);
	/// Stops the calibration of the sensor.
	void stopCalibration();

	/// Updates the sensor if enough time has passed (more than interval milliseconds). Returns true when data was collected successfully or not enough time has passed.
	boolean update();

	/// Returns true when the data is currently valid.
	boolean isDataOK() const;
	/// Returns true when the data is currently valid or was valid less than invalidInterval milliseconds.
	boolean wasDataOK() const;

	/// Returns true when the sensor is calibrated.
	boolean isCalibrated() const;
	/// Returns true when the sensor is currently calibrating.
	boolean isCalibrating() const;
};