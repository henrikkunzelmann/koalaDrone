#ifndef _GYRO_h
#define _GYRO_h

#include "arduino.h"
#include "MathHelper.h"
#include "Config.h"
#include "Log.h"
#include "Profiler.h"

struct GyroValues {
	int16_t RawGyroX, RawGyroY, RawGyroZ;

	float AccX, AccY, AccZ;
	float GyroX, GyroY, GyroZ;
	float MagnetX, MagnetY, MagnetZ;
};

#define FILTER(x, y, w) ((1.0f - w) * x + w * y)

class Gyro
{
private:
	uint32_t lastSample;
	GyroValues last;

	bool calibration = false;
	bool calibrationOrientation = false;
	uint32_t calibrationCount;

	float rollOffset = 0;
	float pitchOffset = 0;
	float yawOffset = 0;

	int64_t gyroOffset[3];
	int16_t minGyro[3];
	int16_t maxGyro[3];

protected:
	Config* config;

	GyroValues values;

	float roll = 0;
	float pitch = 0;
	float yaw = 0;

	virtual void getValues(GyroValues* values) = 0;
	void calculateIMU();

public:
	explicit Gyro(Config* config);

	virtual char* name() = 0;
	virtual char* magnetometerName() = 0;

	virtual bool init() = 0;
	virtual void reset() = 0;

	void update();
	virtual float getTemperature() = 0;

	virtual bool hasMagnetometer() = 0;
	virtual bool hasIMU() = 0;

	void calibrate();
	bool inCalibration();

	GyroValues getValues() const;

	float getRoll() const;
	float getPitch() const;
	float getYaw() const;

	boolean isMoving() const;
	boolean isFlat() const;
};

#endif

