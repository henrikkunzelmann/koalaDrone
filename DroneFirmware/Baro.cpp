#include "Baro.h"

Baro::Baro(SensorCalibration* calibration) {
	this->calibration = calibration;

	memset(&last, 0, sizeof(BaroValues));
	memset(&values, 0, sizeof(BaroValues));

	this->altitude = 0;

	this->firstSample = true;
	this->lastSample = micros();
}

Baro::~Baro() {

}

void Baro::update() {
	uint32_t interval = micros() - lastSample;
	if (interval < CYCLE_BARO * 1000)
		return;
	Profiler::pushData("Baro::jitter()", interval - CYCLE_BARO * 1000);
	lastSample = micros();

	Profiler::begin("Baro::update()");

	getValues(&values);

	if (firstSample) {
		last = values;
		firstSample = false;
		return Profiler::end();
	}

	values.Pressure = FILTER(last.Pressure, values.Pressure, 0.025f);
	values.Humidity = FILTER(last.Humidity, values.Humidity, 0.025f);
	values.Temperature = FILTER(last.Temperature, values.Temperature, 0.025f);
	altitude = 44330 * (1.0 - pow(values.Pressure / 1013.25, 0.1903));

	// Data Interval messen
	if (memcmp(&last, &values, sizeof(BaroValues)) != 0) {
		Profiler::restart("Baro::data()");

		last = values;
	}
	Profiler::end();
}

BaroValues Baro::getValues() const {
	return values;
}

float Baro::getAltitude() const {
	return altitude;
}