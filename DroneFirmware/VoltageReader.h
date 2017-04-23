#pragma once

#include <Arduino.h>

#include "Model.h"

class VoltageReader {
private:
	bool firstSample = true;
	float voltage;

	float readRawVoltage();

public:
	VoltageReader();

	float readVoltage();
};