#include "SensorHAL.h"

SensorHAL::SensorHAL(Config* config) {
	this->config = config;
	this->gyro = NULL;
	this->baro = NULL;

	initSensors();
}

void SensorHAL::initSensors() {
	bool gyroInit;
	
	gyroInit = initGyro(new Gyro9250(config));
	if (!gyroInit)
		gyroInit = initGyro(new Gyro6050(config));

	initBaro(new Baro280(config));
	
	Log::info("Boot", "Gyro sensor: \"%s\"", getGyroName());
	Log::info("Boot", "Magnetometer: \"%s\"", getMagnetometerName());
	Log::info("Boot", "Baro sensor: \"%s\"", getBaroName());

	if (!gyroInit)
		FaultManager::fault(FaultHardware, "SensorHAL", "initSensors() Gyro");
}

bool SensorHAL::initGyro(Gyro* gyro) {
	if (this->gyro != NULL)
		delete this->gyro;

	this->gyro = gyro;
	if (!gyro->init())
		return false;
	gyro->beginCalibration(CalibrationGyro);
	return true;
}

bool SensorHAL::initBaro(Baro* baro) {
	if (this->baro != NULL)
		delete this->baro;

	this->baro = baro;
	return baro->init();
}

const char* SensorHAL::getGyroName() const {
	if (gyro == NULL || !gyro->isOK())
		return "";
	return gyro->name();
}

const char* SensorHAL::getMagnetometerName() const {
	if (gyro == NULL || !gyro->isOK())
		return "";
	return gyro->magnetometerName();
}

const char* SensorHAL::getBaroName() const {
	if (baro == NULL || !baro->isOK())
		return "";
	return baro->name();
}


Gyro* SensorHAL::getGyro() const {
	return gyro;
}

Baro* SensorHAL::getBaro() const {
	return baro;
}

SensorCalibration* SensorHAL::getCalibrationData() {
	return &config->SensorCalibrationData;
}

void SensorHAL::update() {
	if (gyro != NULL)
		gyro->update();

	yield();

	if (baro != NULL)
		baro->update();
}