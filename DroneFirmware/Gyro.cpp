#include "Gyro.h"
#include "SensorHAL.h"

Gyro::Gyro(SensorHAL* hal, Config* config) : Sensor(hal, config, CYCLE_GYRO, 1000) {
	this->calibration = &config->SensorCalibrationData;

	memset(&last, 0, sizeof(last));
	memset(&rawValues, 0, sizeof(rawValues));
	memset(&values, 0, sizeof(values));
	clearCalibrationData(&gyroCalibration);
	clearCalibrationData(&accCalibration);

	firstSample = true;
	lastMagnetData = millis();
}

Gyro::~Gyro() {

}

boolean Gyro::startCalibration(uint8_t* savedData, size_t length)
{
	firstSample = true;
	clearCalibrationData(&gyroCalibration);
	clearCalibrationData(&accCalibration);
	return false;
}

boolean Gyro::runCalibration(uint32_t ticks) {
	if (ticks > convertTimeToTicks(5000)) {
		Log::debug("Gyro", "GyroCalibration:");
		logCalibration(&gyroCalibration);
		Log::debug("Gyro", "AccCalibration:");
		logCalibration(&accCalibration);
		// Calibrate IMU too, because gyro values have changed
		if (hal->getIMU() != NULL)
			hal->getIMU()->calibrate(NULL, 0);
		return true;
	}

	updateCalibrationData(&gyroCalibration, rawValues.GyroX, rawValues.GyroY, rawValues.GyroZ, false);
	updateCalibrationData(&accCalibration, rawValues.AccX, rawValues.AccY, rawValues.AccZ, false);
	return false;
}

boolean Gyro::processData() {
	// Apply config
#if SWAP_GYRO_XY
	MathHelper::swap(&rawValues.GyroX, &rawValues.GyroY);
	MathHelper::swap(&rawValues.AccX, &rawValues.AccY);
	MathHelper::swap(&rawValues.MagnetX, &rawValues.MagnetY);
#endif
#if NEGATE_GYRO_X
	rawValues.GyroX *= -1.0f;
	rawValues.AccX *= -1.0f;
	rawValues.MagnetX *= -1.0f;
#endif
#if NEGATE_GYRO_Y
	rawValues.GyroY *= -1.0f;
	rawValues.AccY *= -1.0f;
	rawValues.MagnetY *= -1.0f;
#endif
#if NEGATE_GYRO_Z
	rawValues.GyroZ *= -1.0f;
	rawValues.AccZ *= -1.0f;
	rawValues.MagnetZ *= -1.0f;
#endif

	values = rawValues;

	// Werte überprüfen
	const float gyroRange = 300.0f;
	const float accRange = 10.0f;

	boolean ok = true;
	if (values.GyroX < -gyroRange || values.GyroX > gyroRange ||
		values.GyroY < -gyroRange || values.GyroY > gyroRange ||
		values.GyroZ < -gyroRange || values.GyroZ > gyroRange) {
		Log::debug("Gyro", "invalid gyrox: %f, gyroy: %f, gyroz: %f", values.GyroX, values.GyroY, values.GyroZ);
		FaultManager::fault(FaultInvalidSensorData, "Gyro", "filterData()");
		ok = false;
	}
	if (values.AccX < -accRange || values.AccX > accRange ||
		values.AccY < -accRange || values.AccY > accRange ||
		values.AccZ < -accRange || values.AccZ > accRange) {
		Log::debug("Gyro", "invalid accx: %f, accy: %f, accz: %f", values.AccX, values.AccY, values.AccZ);
		FaultManager::fault(FaultInvalidSensorData, "Acc", "filterData()");
		ok = false;
	}

	// Calibration
	if (isCalibrated()) {
		values.GyroX -= gyroCalibration.average[0];
		values.GyroY -= gyroCalibration.average[1];
		values.GyroZ -= gyroCalibration.average[2];

		values.MagnetX -= calibration->MagnetCalibration.average[0];
		values.MagnetY -= calibration->MagnetCalibration.average[1];
		values.MagnetZ -= calibration->MagnetCalibration.average[2];
	}

	if (firstSample) {
		last = values;
		firstSample = false;
	}

	// Filtering
	values.GyroX = FILTER(last.GyroX, values.GyroX, config->GyroFilter);
	values.GyroY = FILTER(last.GyroY, values.GyroY, config->GyroFilter);
	values.GyroZ = FILTER(last.GyroZ, values.GyroZ, config->GyroFilter);

	values.AccX = FILTER(last.AccX, values.AccX, config->AccFilter);
	values.AccY = FILTER(last.AccY, values.AccY, config->AccFilter);
	values.AccZ = FILTER(last.AccZ, values.AccZ, config->AccFilter);

	values.MagnetX = FILTER(last.MagnetX, values.MagnetX, 0.005f);
	values.MagnetY = FILTER(last.MagnetY, values.MagnetY, 0.005f);
	values.MagnetZ = FILTER(last.MagnetZ, values.MagnetZ, 0.005f);

	values.Temperature = FILTER(last.Temperature, values.Temperature, 0.025f);

	yield();
	return ok;
}

SensorUpdateError Gyro::collectData() {
	Profiler::pushData("Gyro::jitter()", updateTimer->getJitter());
	Profiler::begin("Gyro::update()");

	if (!getValues(&rawValues)) {
		Profiler::end();
		return UpdateError;
	}

	if (!processData()) {
		Profiler::end();
		return UpdateError;
	}

	if (isCalibrated()) {
		if (hasMagnetometer()) {
			// Runtime magnetic calibration
			if (canUseMagneticData()) {
				updateCalibrationData(&calibration->MagnetCalibration, rawValues.MagnetX, rawValues.MagnetY, rawValues.MagnetZ, true);
				lastMagnetData = millis();
			}
			else if (millis() - lastMagnetData > 10000) {
				Log::error("Gyro", "Magnetic data is not useable");
				lastMagnetData = millis();
			}
		}

		yield();
	}


	// Data Interval messen
	boolean changed = (memcmp(&last, &values, sizeof(values)) != 0);
	if (changed) {
		Profiler::restart("Gyro::data()");
		last = values;
	}

	Profiler::end();
	
	return changed ? UpdateOK : UpdateNotChanged;
}


GyroValues Gyro::getValues() const {
	return values;
}

float Gyro::getMagnetStrength() const {
	float x = values.MagnetX;
	float y = values.MagnetY;
	float z = values.MagnetZ;

	return sqrtf(x*x + y*y + z*z);
}

bool Gyro::isMagnetInterferenced() const {
	float len = calibration->MagnetCalibration.length;
	if (len <= 0 || !isCalibrated()) // keine Kalibrierung vorhanden
		return getMagnetStrength() > 200;

	return abs(getMagnetStrength() - len) >= 5;
}

bool Gyro::isAccMoving() const {
	float x = values.AccX;
	float y = values.AccY;
	float z = values.AccZ;

	float len2 = sqrtf(x*x + y*y + z*z);
	return len2 < accCalibration.length * 0.98 || len2 > accCalibration.length * 1.02;
}

bool Gyro::canUseMagneticData() const {
	return hasMagnetometer() && calibration->MagnetCalibration.length > 0 && !isMagnetInterferenced();
}

#define GYRO_ROTATING_FACTOR 5.0f
bool Gyro::isGyroXRotating() const {
	return values.GyroX < gyroCalibration.min[0] * GYRO_ROTATING_FACTOR || values.GyroX > gyroCalibration.max[0] * GYRO_ROTATING_FACTOR;
}

bool Gyro::isGyroYRotating() const {
	return values.GyroY < gyroCalibration.min[1] * GYRO_ROTATING_FACTOR || values.GyroY > gyroCalibration.max[1] * GYRO_ROTATING_FACTOR;
}

bool Gyro::isGyroZRotating() const {
	return values.GyroZ < gyroCalibration.min[2] * GYRO_ROTATING_FACTOR || values.GyroZ > gyroCalibration.max[2] * GYRO_ROTATING_FACTOR;
}

bool Gyro::isGyroRotating() const {
	return isGyroXRotating() || isGyroYRotating() || isGyroZRotating();
}

bool Gyro::isMoving() const {
	return isAccMoving() || isGyroRotating();
}