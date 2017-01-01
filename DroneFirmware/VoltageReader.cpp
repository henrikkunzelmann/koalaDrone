#include "VoltageReader.h"

VoltageReader::VoltageReader(int pin, float maxVoltage, float maxInputVoltage) {
	this->inputPin = pin;
	this->maxVoltage = maxVoltage;
	this->maxInputVoltage = maxInputVoltage;
	this->voltage = maxVoltage;
}

float VoltageReader::readVoltage() {
	voltage = voltage * 0.92f + readRawVoltage() * 0.08f;
	return voltage;
}

float VoltageReader::readRawVoltage() {
	return ((analogRead(inputPin) / 1024.0f) / maxInputVoltage) * maxVoltage;
}
