#include "Position.h"

Position::Position(Config* config) {
	this->config = config;
	this->calibration = &config->SensorCalibrationData;

	memset(&last, 0, sizeof(last));
	memset(&value, 0, sizeof(value));

	this->firstSample = true;
}

Position::~Position() {

}

void Position::update() {
	if (!sampleTimer.shouldTick())
		return;

	Profiler::pushData("Position::jitter()", sampleTimer.getJitter());
	Profiler::begin("Position::update()");

	getValue(&value);

	if (firstSample) {
		last = value;
		firstSample = false;
		return Profiler::end();
	}

	// Data Interval messen
	if (memcmp(&last, &value, sizeof(value)) != 0) {
		Profiler::restart("Position::data()");

		last = value;
	}
	Profiler::end();
}

PositionValue Position::getValue() const {
	return value;
}