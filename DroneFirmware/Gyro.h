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

struct GyroValues {
	float AccX, AccY, AccZ;
	float GyroX, GyroY, GyroZ;
	float MagnetX, MagnetY, MagnetZ;

	float Temperature;
};

class Gyro : public Sensor
{
private:
	bool firstSample = true;
	GyroValues last;

	CalibrationData gyroCalibration;
	CalibrationData accCalibration;
	
	GyroValues values;
	GyroValues rawValues;

	uint32_t lastMagnetData;
	float getMagnetStrength() const;
	bool isMagnetInterferenced() const;

	boolean startCalibration(uint8_t* savedData, size_t length);
	boolean runCalibration(uint32_t ticks);
	boolean processData();

protected:
	SensorCalibration* calibration;
	virtual bool getValues(GyroValues* values) = 0;

	SensorUpdateError collectData();

	explicit Gyro(SensorHAL* hal, Config* config);
public:
	virtual ~Gyro();
	 
	virtual const char* getMagnetometerName() const = 0;

	virtual bool hasMagnetometer() const = 0;

	GyroValues getValues() const;

	bool isGyroXRotating() const;
	bool isGyroYRotating() const;
	bool isGyroZRotating() const;

	bool isAccMoving() const;
	bool isGyroRotating() const;
	bool canUseMagneticData() const;

	bool isMoving() const;
};

