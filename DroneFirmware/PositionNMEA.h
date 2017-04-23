#pragma once

#include <Arduino.h>

#include "Build.h"
#include "Position.h"
#include "Model.h"
#include "NMEA.h"

class PositionNMEA : public Position
{
protected:
	bool ok;
	HardwareSerial* serial;

	bool getValue(PositionValue* value);
public:
	explicit PositionNMEA(Config* config);

	bool isOK() const;
	const char* name() const;

	bool init();
	void reset();
};