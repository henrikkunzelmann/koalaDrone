#include "PositionNMEA.h"

PositionNMEA::PositionNMEA(Config* config) : Position(config) {
	ok = false;
}

bool PositionNMEA::isOK() const {
	return ok;
}

const char* PositionNMEA::name() const {
	return "GPS via NMEA";
}

bool PositionNMEA::init() {
	Log::info("PositionNMEA", "init()");

	serial = new HardwareSerial(2);
	serial->begin(9600);

	ok = true;
	return true;
}

bool PositionNMEA::getValue(PositionValue* value) {
	if (!ok)
		return false;

	Profiler::begin("PositionNMEA::getValues()");

	NMEA* nmea = new NMEA();
	while (serial->available() > 0) {
		String message = serial->readStringUntil('\n');
		message.trim(); // remove white space

		if (message.length() > 0) {
			if (config->PassThroughSerialNMEA)
				Log::debug("PositionNMEA", "Data: %s", message.c_str());

			nmea->parse(message.c_str());
		}
	}

	// get data
	bool valid = nmea->isValid();
	if (valid) {
		value->latitude = nmea->getLatitude();
		value->longitude = nmea->getLongitude();
		value->altitude = nmea->getAltitude();
		value->velocity = nmea->getSpeed();
	}

	delete nmea;
	Profiler::end();
	return valid;
}

void PositionNMEA::reset() {
}