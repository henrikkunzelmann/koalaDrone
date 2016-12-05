#include "Baro280.h"

Baro280::Baro280(Config* config) : Baro(config), bme(BME280_ADDR_SDO_HIGH) {
	ok = false;
}

bool Baro280::isOK() const {
	return ok;
}

const char* Baro280::name() const {
	return "Bosch BME280";
}

bool Baro280::init() {
	Log::info("Baro280", "init()");

	if (!bme.checkConnection()) {
		Log::error("Baro280", "Connection failed");
		ok = false;
		return ok;
	}

	Log::debug("Baro280", "bme.reset()");
	if (!bme.reset()) {
		Log::error("Baro280", "Reset failed");
		ok = false;
		return ok;
	}

	Log::debug("Baro280", "bme.init()");
	if (!bme.init()) {
		Log::error("Baro280", "Init failed");
		ok = false;
		return ok;
	}

	Log::info("Baro280", "done with init");

	ok = true;
	return ok;
}

bool Baro280::getValues(BaroValues* values) {
	if (!ok)
		return false;

	Profiler::begin("Baro280::getValues()");

	bool success = bme.getValues(&values->Temperature, &values->Pressure, &values->Humidity);
	yield();

	Profiler::end();
	return success;
}

void Baro280::reset() {
	if (ok)
		bme.reset();
}