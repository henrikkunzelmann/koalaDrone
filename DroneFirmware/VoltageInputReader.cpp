#include "VoltageInputReader.h"

VoltageInputReader::VoltageInputReader(int pin, float maxVoltage, float maxInputVoltage) {
	this->inputPin = pin;
	this->maxVoltage = maxVoltage;
	this->maxInputVoltage = maxInputVoltage;
	this->voltage = maxVoltage;
}

float VoltageInputReader::readVoltage() {
	voltage = voltage * 0.92f + readRawVoltage() * 0.08f;
	return voltage;
}

float VoltageInputReader::readRawVoltage() {
	return ((analogRead(inputPin) / 1024.0f) / maxInputVoltage) * maxVoltage;
}
