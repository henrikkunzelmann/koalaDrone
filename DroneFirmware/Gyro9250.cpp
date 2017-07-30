#include "Gyro9250.h"

Gyro9250::Gyro9250(SensorHAL* hal, Config* config) : Gyro(hal, config) {
	mpuOK = false;
}

const char* Gyro9250::getName() const {
	return "InvenSense MPU-9250";
}

const char* Gyro9250::getShortName() const {
	return "Gyro9250";
}

const char* Gyro9250::getMagnetometerName() const {
	return "MPU-9250 (AK8963)";
}

boolean Gyro9250::isHardwareBased() const
{
	return true;
}

boolean Gyro9250::disable()
{
	return true;
}

boolean Gyro9250::init() {
	Log::info("Gyro9250", "init()");

	if (!mpu.testConnection()) {
		Log::error("Gyro9250", "Connection failed");
		mpuOK = false;
		return mpuOK;
	}

	Log::debug("Gyro9250", "mpu.reset()");
	mpu.reset();
	mpu.resetSensors();
	mpu.magSoftReset();
	mpu.magInit();

	Log::debug("Gyro9250", "mpu.initialize()");
	mpu.initialize();

	if (!mpu.magCheckConnection()) {
		Log::error("Gyro9250", "Mag connection failed");
		FaultManager::fault(FaultHardware, "Gyro9250", "magCheckConnection()");
	}

	mpu.magGetAxisSensitivity(&sx, &sy, &sz);

	setSettings();
	Log::info("Gyro9250", "done with init");

	mpuOK = true;
	return mpuOK;
}

bool Gyro9250::setSettings() {
	mpu.setClockSource(MPU9250_CLOCK_PLL_ZGYRO);
	mpu.setFullScaleAccelRange(MPU9250_ACCEL_FS_8);
	mpu.setFullScaleGyroRange(MPU9250_GYRO_FS_1000);
	if (config->GyroDLPF > MPU9250_DLPF_BW_5) {
		Log::error("Gyro9250", "Config.GyroDLPF is invalid (%d)", config->GyroDLPF);
		mpu.setDLPFMode(MPU9250_DLPF_BW_42);
	}
	else
		mpu.setDLPFMode(config->GyroDLPF);

	double accRange[4] = { 2, 4, 8, 16 }; // g
	double gyroRange[4] = { 250, 500, 1000, 2000 }; // degress/s
	double magRange[2] = { 0.6, 0.15 }; // microT

	accRes = accRange[mpu.getFullScaleAccelRange()] / 32768.0;
	gyroRes = gyroRange[mpu.getFullScaleGyroRange()] / 32768.0;
	magRes = magRange[mpu.magGetSensitivity()];
	return true;
}

bool Gyro9250::getValues(GyroValues* values) {
	if (!mpuOK)
		return false;

	Profiler::begin("Gyro9250::getValues()");

	if (mpu.getFullScaleGyroRange() != MPU9250_GYRO_FS_1000) {
		Log::error("Gyro9250", "FullScaleGyroRange set on chip does not match desired setting");
		FaultManager::fault(FaultHardware, "Gyro9250", "getFullScaleGyroRange()");
		setSettings();
		Profiler::end();
		return false;
	}

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

	values->MagnetX = my * sy * magRes;
	values->MagnetY = mx * sx * magRes;
	values->MagnetZ = -mz * sz * magRes;

	int16_t temp;
	if (!mpu.getTemperature(&temp)) {
		FaultManager::fault(FaultHardware, "Gyro9250", "getTemperature()");
		Profiler::end();
		return false;
	}

	yield();

	values->Temperature = temp / 333.87f + 21.0f;
	Profiler::end();
	return true;
}

bool Gyro9250::hasMagnetometer() const {
	return true;
}

bool Gyro9250::hasIMU() const {
	return false;
}