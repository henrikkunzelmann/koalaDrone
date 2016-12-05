#ifndef _VOLTAGEINPUTREADER_h
#define _VOLTAGEINPUTREADER_h

#include "arduino.h"

class VoltageInputReader {
private:
	int inputPin;
	float maxVoltage;
	float maxInputVoltage;
	float voltage;

public:
	VoltageInputReader(int pin, float maxVoltage, float maxInputVoltage);

	float readVoltage();
	float readRawVoltage();
};

#endif

