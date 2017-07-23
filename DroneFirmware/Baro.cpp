#include "Baro.h"

Baro::Baro(Config* config) {
	this->config = config;
	this->calibration = &config->SensorCalibrationData;

	memset(&last, 0, sizeof(last));
	memset(&values, 0, sizeof(values));

	this->altitude = 0;

	this->firstSample = true;
}

Baro::~Baro() {

}

void Baro::update() {
	if (!sampleTimer.shouldTick())
		return;

	Profiler::pushData("Baro::jitter()", sampleTimer.getJitter());
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
	altitude = 44330 * (1.0 - pow(values.Pressure / 1013.25, 0.19029495718));

	// Data Interval messen
	if (memcmp(&last, &values, sizeof(values)) != 0) {
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