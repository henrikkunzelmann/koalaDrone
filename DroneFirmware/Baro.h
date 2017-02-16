#ifndef _BARO_h
#define _BARO_h

#include <Arduino.h>

#include "MathHelper.h"
#include "SensorCalibration.h"
#include "Log.h"
#include "Profiler.h"
#include "Timer.h"

struct BaroValues {
	float Temperature;
	float Pressure;
	float Humidity;
};

class Baro
{
private:
	bool firstSample;
	Timer sampleTimer = Timer(CYCLE_BARO);
	BaroValues last;

	BaroValues values;
	float altitude;

protected:
	Config* config;
	SensorCalibration* calibration;

	virtual bool getValues(BaroValues* values) = 0;

	explicit Baro(Config* config);
public:
	virtual ~Baro();

	virtual const char* name() const = 0;

	virtual bool init() = 0;
	virtual void reset() = 0;

	virtual bool isOK() const = 0;

	void update();
	BaroValues getValues() const;
	float getAltitude() const;
};

#endif

