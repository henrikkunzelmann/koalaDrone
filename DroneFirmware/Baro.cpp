#include "Baro.h"

Baro::Baro(Config* config) {
	this->config = config;
}

void Baro::update() {
	uint32_t interval = micros() - lastSample;
	if (interval < CYCLE_BARO * 1000)
		return;
	Profiler::pushData("Baro::jitter()", interval - CYCLE_BARO * 1000);
	lastSample = micros();

	Profiler::begin("Baro::update()");

	getValues(&values);

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