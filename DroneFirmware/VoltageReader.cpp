#include "VoltageReader.h"

VoltageReader::VoltageReader(int pin, float maxVoltage) {
	this->inputPin = pin;
	this->maxVoltage = maxVoltage;

	this->voltage = 0;
}

float VoltageReader::readVoltage() {
	if (firstSample) {
		voltage = readRawVoltage();
		firstSample = false;
	}
	else
		voltage = voltage * 0.92f + readRawVoltage() * 0.08f;
	return voltage;
}

float VoltageReader::readRawVoltage() {
	return (analogRead(inputPin) / 1024.0f) * maxVoltage;
}
