#pragma once

#include <Arduino.h>

#include "MathHelper.h"
#include "SensorCalibration.h"
#include "Log.h"
#include "Profiler.h"
#include "Timer.h"

struct PositionValue {
	float latitude;
	float longitude;
	float altitude;
	float velocity;
};

class Position
{
private:
	bool firstSample;
	Timer sampleTimer = Timer(CYCLE_POSITION);
	PositionValue last;

	PositionValue value;

protected:
	Config* config;
	SensorCalibration* calibration;

	virtual bool getValue(PositionValue* value) = 0;

	explicit Position(Config* config);
public:
	virtual ~Position();

	virtual const char* name() const = 0;

	virtual bool init() = 0;
	virtual void reset() = 0;

	virtual bool isOK() const = 0;

	void update();
	PositionValue getValue() const;
};