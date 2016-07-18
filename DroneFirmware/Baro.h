#ifndef _BARO_h
#define _BARO_h

#include "arduino.h"
#include "MathHelper.h"
#include "Config.h"
#include "Log.h"
#include "Profiler.h"

struct BaroValues {
	float Temperature;
	float Pressure;
	float Humidity;
};

class Baro
{
private:
	uint32_t lastSample;
	BaroValues last;

protected:
	Config* config;

	BaroValues values;
	float altitude;

	virtual void getValues(BaroValues* values) = 0;

public:
	explicit Baro(Config* config);

	virtual char* name() = 0;

	virtual bool init() = 0;
	virtual void reset() = 0;

	void update();

	BaroValues getValues() const;
	float getAltitude() const;
};

#endif

