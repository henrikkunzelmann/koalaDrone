#ifndef _GYRO_h
#define _GYRO_h

#include <Arduino.h>

#include "LED.h"
#include "MathHelper.h"
#include "SensorCalibration.h"
#include "Log.h"
#include "Profiler.h"
#include "FaultManager.h"

struct GyroValues {
	float AccX, AccY, AccZ;
	float GyroX, GyroY, GyroZ;
	float MagnetX, MagnetY, MagnetZ;

	float Temperature;
};

enum CalibrationState {
	CalibrationNone,
	CalibrationGyro,
	CalibrationAcc,
	CalibrationOrientation,
	CalibrationMagnet
};

class Gyro
{
private:
	bool firstSample = true;
	uint32_t lastSample;
	GyroValues last;

	CalibrationState calibrationState;
	int32_t calibrationCount;
	int32_t calibrationWrongDataCount;
	float calibrationSum[3];

	CalibrationData gyroCalibration;
	CalibrationData accCalibration;
	CalibrationData orientationCalibration;

	bool validGyroData;
	bool validAccData;
	bool validMagData;
	bool validImu;

	GyroValues values;
	GyroValues rawValues;

	float roll;
	float pitch;
	float yaw;

	void calculateIMU();

	uint32_t lastMagnetData;
	float getMagnetStrength() const;
	bool isMagnetInterferenced() const;

	void resetCalibration(CalibrationData* data);
	void updateCalibrationData(CalibrationData* data, float x, float y, float z, bool averageByBounds);
	void logCalibration(CalibrationData* data);

	void runCalibration();
	void wrongCalibrationData();

	void processData();

	bool isGyroXRotating() const;
	bool isGyroYRotating() const;
	bool isGyroZRotating() const;

	bool isAccMoving() const;
	bool isGyroRotating() const;
	bool canUseMagneticData() const;

protected:
	Config* config;
	SensorCalibration* calibration;
	virtual bool getValues(GyroValues* values) = 0;

	explicit Gyro(Config* config);
public:
	virtual ~Gyro();

	virtual const char* name() const = 0;
	virtual const char* magnetometerName() const = 0;

	virtual bool init() = 0;
	virtual void reset() = 0;
	virtual void resetMagnet() = 0;

	virtual bool isOK() const = 0;
	virtual bool hasMagnetometer() const = 0;
	virtual bool hasIMU() const = 0;

	void update();

	void beginCalibration(CalibrationState state);
	bool inCalibration() const;

	bool hasValidGyroData() const;
	bool hasValidAccData() const;
	bool hasValidMagnetData() const;
	bool hasValidImuData() const;

	GyroValues getValues() const;

	float getRoll() const;
	float getPitch() const;
	float getYaw() const;

	bool isMoving() const;
	bool isFlat() const;
};

#endif

