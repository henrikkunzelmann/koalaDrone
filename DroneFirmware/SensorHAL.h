#ifndef _SENSORHAL_h
#define _SENSORHAL_h

#include "arduino.h"
#include "Config.h"
#include "Gyro.h"
#include "Gyro6050.h"
#include "Gyro9250.h"
#include "Baro.h"
#include "Baro280.h"
#include "SensorCalibration.h"

class SensorHAL
{
private:
	Config* config;
	Gyro* gyro;
	Baro* baro;

	void initSensors();
	boolean initGyro(Gyro* gyro);
	boolean initBaro(Baro* baro);

public:
	explicit SensorHAL(Config* config);
	
	char* getGyroName() const;
	char* getMagnetometerName() const;
	char* getBaroName() const;

	Gyro* getGyro() const;
	Baro* getBaro() const;
	SensorCalibration* getCalibrationData();

	void update();
};
#endif
