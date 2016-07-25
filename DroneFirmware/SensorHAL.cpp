#include "SensorHAL.h"

SensorHAL::SensorHAL(Config* config) {
	this->config = config;
	this->gyro = NULL;
	this->baro = NULL;

	initSensors();
}

void SensorHAL::initSensors() {
	boolean gyroInit = false;
	if (!gyroInit)
		gyroInit = initGyro(new Gyro9250(getCalibrationData()));
	if (!gyroInit)
		gyroInit = initGyro(new Gyro9250(getCalibrationData()));

	initBaro(new Baro280(getCalibrationData()));

	Log::info("Boot", "Gyro sensor: \"%s\"", getGyroName());
	Log::info("Boot", "Magnetometer: \"%s\"", getMagnetometerName());
	Log::info("Boot", "Baro sensor: \"%s\"", getBaroName());
}

boolean SensorHAL::initGyro(Gyro* gyro) {
	if (this->gyro != NULL)
		delete this->gyro;

	this->gyro = gyro;
	if (!gyro->init())
		return false;
	gyro->beginCalibration();
	return true;
}

boolean SensorHAL::initBaro(Baro* baro) {
	if (this->baro != NULL)
		delete this->baro;

	this->baro = baro;
	return baro->init();
}

char* SensorHAL::getGyroName() const {
	if (gyro == NULL)
		return "";
	return gyro->name();
}

char* SensorHAL::getMagnetometerName() const {
	if (gyro == NULL)
		return "";
	return gyro->magnetometerName();
}

char* SensorHAL::getBaroName() const {
	if (baro == NULL)
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

	if (baro != NULL)
		baro->update();
}