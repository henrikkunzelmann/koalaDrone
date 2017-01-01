#ifndef _VOLTAGEREADER_h
#define _VOLTAGEREADER_h

#include "arduino.h"

class VoltageReader {
private:
	int inputPin;
	float maxVoltage;
	float maxInputVoltage;
	float voltage;

public:
	VoltageReader(int pin, float maxVoltage, float maxInputVoltage);

	float readVoltage();
	float readRawVoltage();
};

#endif

