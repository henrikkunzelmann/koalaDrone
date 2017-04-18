#pragma once

#include <Arduino.h>

class VoltageReader {
private:
	int inputPin;

	bool firstSample = true;

	float maxVoltage;
	float voltage;

	float readRawVoltage();

public:
	VoltageReader(int pin, float maxVoltage);

	float readVoltage();
};