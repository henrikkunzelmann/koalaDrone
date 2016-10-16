#include "Gyro9250.h"

Gyro9250::Gyro9250(Config* config) : Gyro(config) {
}

const char* Gyro9250::name() {
	if (mpuOK)
		return "InvenSense MPU-9250";
	return "";
}

const char* Gyro9250::magnetometerName() {
	if (mpuOK)
		return "MPU-9250 (AK8963)";
	return "";
}

bool Gyro9250::init() {
	Log::info("Gyro9250", "init()");

	if (!mpu.testConnection()) {
		Log::error("Gyro9250", "Connection failed");
		mpuOK = false;
		return mpuOK;
	}

	Log::debug("Gyro9250", "mpu.reset()");
	mpu.reset();

	Log::debug("Gyro9250", "mpu.initialize()");
	mpu.initialize();

	mpu.setClockSource(MPU9250_CLOCK_PLL_ZGYRO);
	mpu.setFullScaleAccelRange(MPU9250_ACCEL_FS_16);
	mpu.setFullScaleGyroRange(MPU9250_GYRO_FS_2000);
	mpu.setDLPFMode(0);

	if (!mpu.magCheckConnection()) {
		Log::error("Gyro9250", "Mag connection failed");
		FaultManager::fault(FaultHardware, "Gyro9250", "magCheckConnection()");
	}

	mpu.magGetAxisSensitivity(&sx, &sy, &sz);

	double accRange[4] = { 2, 4, 8, 16 }; // g
	double gyroRange[4] = { 250, 500, 1000, 2000 }; // degress/s
	double magRange[2] = { 0.6, 0.15 }; // microT

	accRes = accRange[mpu.getFullScaleAccelRange()] / 32768.0; 
	gyroRes = gyroRange[mpu.getFullScaleGyroRange()] / 32768.0; 
	magRes = magRange[mpu.magGetSensitivity()];

	Log::info("Gyro9250", "done with init");

	mpuOK = true;
	return mpuOK;
}

bool Gyro9250::getValues(GyroValues* values) {
	if (!mpuOK)
		return false;

	Profiler::begin("Gyro9250::getValues()");

	int16_t ax, ay, az;
	int16_t gx, gy, gz;
	int16_t mx, my, mz;

	if (!mpu.getMotion9(&ax, &ay, &az, &gx, &gy, &gz, &mx, &my, &mz)) {
		FaultManager::fault(FaultHardware, "Gyro9250", "getMotion9()");
		Profiler::end();
		return false;
	}

	yield();

	values->AccX = ax * accRes;
	values->AccY = ay * accRes;
	values->AccZ = az * accRes;

	values->GyroX = gx * gyroRes;
	values->GyroY = gy * gyroRes;
	values->GyroZ = gz * gyroRes;

	values->MagnetX = my * sx * magRes;
	values->MagnetY = mx * sy * magRes;
	values->MagnetZ = -mz * sz * magRes;

	int16_t temp;
	if (!mpu.getTemperature(&temp)) {
		FaultManager::fault(FaultHardware, "Gyro9250", "getTemperature()");
		Profiler::end();
		return false;
	}

	yield();

	values->Temperature = temp / 333.87 + 21;
	Profiler::end();
	return true;
}

void Gyro9250::reset() {
	if (mpuOK)
		mpu.resetSensors();
}

void Gyro9250::resetMagnet() {
	if (mpuOK) {
		mpu.magSoftReset();
		mpu.magInit();
	}
}

bool Gyro9250::hasMagnetometer() const {
	return true;
}

bool Gyro9250::hasIMU() const {
	return false;
}