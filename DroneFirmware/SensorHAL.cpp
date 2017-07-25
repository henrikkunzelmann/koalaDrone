#include "SensorHAL.h"

SensorHAL::SensorHAL(Config* config) {
	this->config = config;
	this->gyro = NULL;
	this->imu = NULL;
	this->baro = NULL;
	this->position = NULL;

	initSensors();
}

void SensorHAL::initSensors() {
	bool gyroInit;
	
	gyroInit = initGyro(new Gyro9250(this, config));
	if (!gyroInit)
		gyroInit = initGyro(new Gyro6050(this, config));

	initIMU(new MadgwickIMU(this, config));
	initBaro(new Baro280(config));
	initPosition(new PositionNMEA(config));
	
	Log::info("Boot", "Gyro sensor: \"%s\"", getGyroName());
	Log::info("Boot", "Magnetometer: \"%s\"", getMagnetometerName());
	Log::info("Boot", "Baro sensor: \"%s\"", getBaroName());
	Log::info("Boot", "Position sensor: \"%s\"", getPositionName());

	if (!gyroInit)
		FaultManager::fault(FaultHardware, "SensorHAL", "initSensors() Gyro");

	Log::emptyLine();
}

bool SensorHAL::initGyro(Gyro* gyro) {
	if (this->gyro != NULL)
		delete this->gyro;

	this->gyro = gyro;
	if (!gyro->init())
		return false;
	gyro->calibrate(NULL, 0);
	return true;
}

bool SensorHAL::initIMU(SensorIMU* imu) {
	if (this->imu != NULL)
		delete this->imu;

	this->imu = imu;
	if (!imu->init())
		return false;
	return true;
}

bool SensorHAL::initBaro(Baro* baro) {
	if (this->baro != NULL)
		delete this->baro;

	this->baro = baro;
	return baro->init();
}

bool SensorHAL::initPosition(Position* position) {
	if (this->position != NULL)
		delete this->position;

	this->position = position;
	return position->init();
}

const char* SensorHAL::getGyroName() const {
	if (gyro == NULL)
		return "";
	return gyro->getName();
}

const char* SensorHAL::getMagnetometerName() const {
	if (gyro == NULL)
		return "";
	return gyro->getMagnetometerName();
}

const char* SensorHAL::getBaroName() const {
	if (baro == NULL || !baro->isOK())
		return "";
	return baro->name();
}

const char* SensorHAL::getPositionName() const {
	if (position == NULL || !position->isOK())
		return "";
	return position->name();
}


Gyro* SensorHAL::getGyro() const {
	return gyro;
}

SensorIMU* SensorHAL::getIMU() const {
	return imu;
}

Baro* SensorHAL::getBaro() const {
	return baro;
}

Position* SensorHAL::getPosition() const {
	return position;
}

SensorCalibration* SensorHAL::getCalibrationData() {
	return &config->SensorCalibrationData;
}

void SensorHAL::update() {
	if (gyro != NULL)
		gyro->update();
	yield();

	if (imu != NULL)
		imu->update();
	yield();

	if (baro != NULL)
		baro->update();
	yield();

	if (position != NULL)
		position->update();
	yield();
}