#ifndef _GYRO_h
#define _GYRO_h

#include "arduino.h"
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

	float roll = 0;
	float pitch = 0;
	float yaw = 0;

	void calculateIMU();

	uint32_t lastMagnetData;
	float getMagnetStrength() const;
	boolean isMagnetInterferenced() const;

	void resetCalibration(CalibrationData* data);
	void updateCalibrationData(CalibrationData* data, float x, float y, float z, boolean averageByBounds);
	void logCalibration(CalibrationData* data);

	void runCalibration();

	void processData();

	boolean isGyroXRotating() const;
	boolean isGyroYRotating() const;
	boolean isGyroZRotating() const;

	boolean isAccMoving() const;
	boolean isGyroRotating() const;
	boolean canUseMagneticData() const;

protected:
	Config* config;
	SensorCalibration* calibration;
	virtual bool getValues(GyroValues* values) = 0;

public:
	explicit Gyro(Config* config);
	virtual ~Gyro();

	virtual const char* name() = 0;
	virtual const char* magnetometerName() = 0;

	virtual bool init() = 0;
	virtual void reset() = 0;
	virtual void resetMagnet() = 0;

	void update();

	virtual bool hasMagnetometer() const = 0;
	virtual bool hasIMU() const = 0;

	void beginCalibration(CalibrationState state);
	bool inCalibration();

	boolean hasValidGyroData() const;
	boolean hasValidAccData() const;
	boolean hasValidMagnetData() const;
	boolean hasValidImuData() const;

	GyroValues getValues() const;

	float getRoll() const;
	float getPitch() const;
	float getYaw() const;

	boolean isMoving() const;
	boolean isFlat() const;
};

#endif

