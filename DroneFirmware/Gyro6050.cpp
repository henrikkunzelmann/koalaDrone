#include "Gyro6050.h"

Gyro6050::Gyro6050(SensorCalibration* calibration) : Gyro(calibration) {
}

char* Gyro6050::name() {
	if (mpuOK)
		return "InvenSense MPU-6050";
	return "";
}

char* Gyro6050::magnetometerName() {
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
	mpu.setDLPFMode(0);

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

	values->AccX = -ax * accRes;
	values->AccY = -ay * accRes;
	values->AccZ = -az * accRes;

	values->RawGyroX = -(gx >> 2);
	values->RawGyroY = -(gy >> 2);
	values->RawGyroZ = -(gz >> 2);

	values->MagnetX = 0;
	values->MagnetY = 0;
	values->MagnetZ = 0;

	values->Temperature = mpu.getTemperature() / 340.00 + 36.53;

	Profiler::end();
	return true;
}

void Gyro6050::reset() {
	if (mpuOK)
		mpu.resetSensors();
}

void Gyro6050::resetMagnet() {
}

bool Gyro6050::hasMagnetometer() const {
	return false;
}

bool Gyro6050::hasIMU() const {
	return false;
}