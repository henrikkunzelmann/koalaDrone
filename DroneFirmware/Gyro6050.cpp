#include "Gyro6050.h"

Gyro6050::Gyro6050(Config* config) : Gyro(config) {
	mpuOK = false;
}

const char* Gyro6050::name() const {
	return "InvenSense MPU-6050";
}

const char* Gyro6050::magnetometerName() const {
	return "";
}

bool Gyro6050::init() {
	Log::info("Gyro6050", "init()");

	if (!mpu.testConnection()) {
		Log::error("Gyro6050", "Connection failed");
		mpuOK = false;
		return mpuOK;
	}

	Log::debug("Gyro6050", "mpu.reset()");
	mpu.reset();

	Log::debug("Gyro6050", "mpu.initialize()");
	mpu.initialize();

	mpu.setClockSource(MPU6050_CLOCK_PLL_ZGYRO);
	mpu.setFullScaleAccelRange(MPU6050_ACCEL_FS_8);
	mpu.setFullScaleGyroRange(MPU6050_GYRO_FS_2000);
	mpu.setDLPFMode(MPU6050_DLPF_BW_20);

	double accRange[4] = { 2, 4, 8, 16 }; // g
	double gyroRange[4] = { 250, 500, 1000, 2000 }; // degress/s

	accRes = accRange[mpu.getFullScaleAccelRange()] / 32768.0;
	gyroRes = gyroRange[mpu.getFullScaleGyroRange()] / 32768.0;

	Log::info("Gyro6050", "done with init");

	mpuOK = true;
	return mpuOK;
}

bool Gyro6050::getValues(GyroValues* values) {
	if (!mpuOK)
		return false;

	Profiler::begin("Gyro6050::getValues()");

	int16_t ax, ay, az;
	int16_t gx, gy, gz;
	mpu.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);
	yield();

	values->AccX = ay * accRes;
	values->AccY = ax * accRes;
	values->AccZ = -az * accRes;

	values->GyroX = gy * gyroRes;
	values->GyroY = gx * gyroRes;
	values->GyroZ = -gz * gyroRes;

	values->MagnetX = 0;
	values->MagnetY = 0;
	values->MagnetZ = 0;

	values->Temperature = mpu.getTemperature() / 340.0f + 36.53f;
	yield();

	Profiler::end();
	return true;
}

void Gyro6050::reset() {
	if (mpuOK)
		mpu.resetSensors();
}

void Gyro6050::resetMagnet() {
}

bool Gyro6050::isOK() const {
	return mpuOK;
}

bool Gyro6050::hasMagnetometer() const {
	return false;
}

bool Gyro6050::hasIMU() const {
	return false;
}