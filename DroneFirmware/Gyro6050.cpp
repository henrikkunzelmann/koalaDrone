#include "Gyro6050.h"

Gyro6050::Gyro6050(Config* config) : Gyro(config) {
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

	Wire.begin(SDA, SCL);
	Wire.setClock(400000L); // gotta go fast

	if (!mpu.testConnection()) {
		Log::error("Gyro6050", "Connection failed");
		mpuOK = false;
		return false;
	}

	Log::debug("Gyro6050", "mpu.reset()");
	mpu.reset();

	Log::debug("Gyro6050", "mpu.initialize()");
	mpu.initialize();

	mpu.setClockSource(MPU6050_CLOCK_PLL_ZGYRO);
	mpu.setFullScaleAccelRange(MPU6050_ACCEL_FS_8);
	mpu.setFullScaleGyroRange(MPU6050_GYRO_FS_2000);
	mpu.setDLPFMode(0);

	float accRange[4] = { 2, 4, 8, 16 }; // g
	float gyroRange[4] = { 250, 500, 1000, 2000 }; // degress/s

	accRes = accRange[mpu.getFullScaleAccelRange()] / 32768.0f;
	gyroRes = gyroRange[mpu.getFullScaleGyroRange()] / 32768.0f;

	Log::info("Gyro6050", "done with init");

	mpuOK = true;
	return mpuOK;
}

void Gyro6050::getValues(GyroValues* values) {
	if (!mpuOK)
		return;

	Profiler::begin("Gyro6050::getValues()");

	int16_t x, y, z;
	mpu.getAcceleration(&x, &y, &z);

	values->AccX = -x * accRes;
	values->AccY = -y * accRes;
	values->AccZ = -z * accRes;

	mpu.getRotation(&x, &y, &z);

	values->RawGyroX = -(x >> 2);
	values->RawGyroY = -(y >> 2);
	values->RawGyroZ = -(z >> 2);

	Profiler::end();
}

void Gyro6050::reset() {
	if (mpuOK)
		mpu.resetSensors();
}

float Gyro6050::getTemperature() {
	if (mpuOK)
		return mpu.getTemperature() / 340.00 + 36.53;
	return 0;
}

bool Gyro6050::hasMagnetometer() {
	return false;
}

bool Gyro6050::hasIMU() {
	return false;
}