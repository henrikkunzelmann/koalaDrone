#ifndef _BARO280_h
#define _BARO280_h

#include "Build.h"
#include "Baro.h"
#include "CycleTimes.h"

#include <Wire.h>
#include <I2Cdev.h>
#include <BME280.h>

class Baro280 : public Baro
{
protected:
	bool ok;

	BME280 bme;

	bool getValues(BaroValues* values);
public:
	explicit Baro280(SensorCalibration* calibration);

	const char* name();

	bool init();
	void reset();
};

#endif