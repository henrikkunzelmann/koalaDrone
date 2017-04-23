#include "VoltageReader.h"

VoltageReader::VoltageReader() {
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
	return (analogRead(PIN_BATTERY) / BATTERY_MAX_VALUE) * BATTERY_MAX_VOLTAGE;
}
