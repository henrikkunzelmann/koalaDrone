#include "Baro280.h"

Baro280::Baro280(SensorCalibration* calibration) : Baro(calibration) {
}

char* Baro280::name() {
	if (ok)
		return "Bosch BME280";
	return "";
}

bool Baro280::init() {
	Log::info("Baro280", "init()");

	if (!bme.checkConnection()) {
		Log::error("Baro280", "Connection failed");
		ok = false;
		return ok;
	}

	Log::debug("Baro280", "bme.reset()");
	bme.reset();

	Log::debug("Baro280", "bme.init()");
	bme.init();

	Log::info("Baro280", "done with init");

	ok = true;
	return ok;
}

void Baro280::getValues(BaroValues* values) {
	if (!ok)
		return;

	Profiler::begin("Baro280::getValues()");

	bme.getValues(&values->Temperature, &values->Pressure, &values->Humidity);

	Profiler::end();
}

void Baro280::reset() {
	if (ok)
		bme.reset();
}