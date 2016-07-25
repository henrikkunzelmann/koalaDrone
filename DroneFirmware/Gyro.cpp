#include "Gyro.h"

Gyro::Gyro(SensorCalibration* calibration) {
	this->calibration = calibration;

	memset(&last, 0, sizeof(GyroValues));
	memset(&rawValues, 0, sizeof(GyroValues));
	memset(&values, 0, sizeof(GyroValues));
	memset(&gyroCalibration, 0, sizeof(CalibrationData));
	memset(&orientationCalibration, 0, sizeof(CalibrationData));

	this->lastSample = micros();
	this->lastMagnetData = millis();
}

void Gyro::resetCalibration(CalibrationData* data) {
	calibrationCount = 0;
	for (int i = 0; i < 3; i++)
		calibrationSum[i] = 0;

	for (int i = 0; i < 3; i++) {
		data->Min[i] = MAXFLOAT;
		data->Max[i] = -MAXFLOAT;
	}
}

void Gyro::calibrateGyro() {
	updateCalibrationData(&gyroCalibration, rawValues.RawGyroX, rawValues.RawGyroY, rawValues.RawGyroZ, false);

	if (++calibrationCount > 5000 / CYCLE_GYRO) {
		for (int i = 0; i < 3; i++) {
			gyroCalibration.Min[i] *= 1.25f;
			gyroCalibration.Max[i] *= 1.25f;
		}

		Log::info("Gyro", "Done with calibration. Starting orientation calibration...");
		calibrationRunning = false;

		resetCalibration(&orientationCalibration);
		calibrationOrientation = true;
		firstSample = true;
	}
}

void Gyro::calibrateMagnet() {
	updateCalibrationData(&calibration->MagnetData, rawValues.MagnetX, rawValues.MagnetY, rawValues.MagnetZ, true);

	if (++calibrationCount > 20000 / CYCLE_GYRO) {
		Log::info("Gyro", "Done with magnetic calibration");
		calibration->MagneticFieldStrength = getMagnetStrength();

		calibrationMagnet = false;
		firstSample = true;
	}
}

void Gyro::updateCalibrationData(CalibrationData* data, float x, float y, float z, boolean averageByBounds) {
	data->Min[0] = min(data->Min[0], x);
	data->Min[1] = min(data->Min[1], y);
	data->Min[2] = min(data->Min[2], z);

	data->Max[0] = max(data->Max[0], x);
	data->Max[1] = max(data->Max[1], y);
	data->Max[2] = max(data->Max[2], z);

	if (averageByBounds) {
		for (int i = 0; i < 3; i++)
			data->Average[i] = (data->Min[i] + data->Max[i]) * 0.5f;
	}
	else {
		calibrationSum[0] += x;
		calibrationSum[1] += y;
		calibrationSum[2] += z;

		for (int i = 0; i < 3; i++)
			data->Average[i] = calibrationSum[i] / calibrationCount;
	}
}

void Gyro::filterData() {
	values = rawValues;

	values.RawGyroX -= gyroCalibration.Average[0];
	values.RawGyroY -= gyroCalibration.Average[1];
	values.RawGyroZ -= gyroCalibration.Average[2];

	values.MagnetX -= calibration->MagnetData.Average[0];
	values.MagnetY -= calibration->MagnetData.Average[1];
	values.MagnetZ -= calibration->MagnetData.Average[2];

	if (firstSample) {
		last = values;
		firstSample = false;
	}

	float gyroRes = 2000.0f / 8196.0f;
	values.GyroX = FILTER(last.RawGyroX, values.RawGyroX, 0.025f) * gyroRes;
	values.GyroY = FILTER(last.RawGyroY, values.RawGyroY, 0.025f) * gyroRes;
	values.GyroZ = FILTER(last.RawGyroZ, values.RawGyroZ, 0.025f) * gyroRes;

	values.AccX = FILTER(last.AccX, values.AccX, 0.025f);
	values.AccY = FILTER(last.AccY, values.AccY, 0.025f);
	values.AccZ = FILTER(last.AccZ, values.AccZ, 0.025f);

	values.MagnetX = FILTER(last.MagnetX, values.MagnetX, 0.005f);
	values.MagnetY = FILTER(last.MagnetY, values.MagnetY, 0.005f);
	values.MagnetZ = FILTER(last.MagnetZ, values.MagnetZ, 0.005f);

	values.Temperature = FILTER(last.Temperature, values.Temperature, 0.025f);
}

void Gyro::calibrateOrientation() {
	updateCalibrationData(&orientationCalibration, roll, pitch, yaw, false);

	if (++calibrationCount > 2000 / CYCLE_GYRO) {
		Log::debug("Gyro", "Done with orientation calibration");

		calibrationOrientation = false;
		firstSample = true;
	}
}

void Gyro::update() {
	uint32_t interval = micros() - lastSample;
	if (interval < CYCLE_GYRO * 1000)
		return;
	Profiler::pushData("Gyro::jitter()", interval - CYCLE_GYRO * 1000);
	lastSample = micros();

	Profiler::begin("Gyro::update()");

	getValues(&rawValues);
	filterData();

	if (calibrationRunning)
		calibrateGyro();
	else if (calibrationMagnet)
		calibrateMagnet();
	else {
		// Runtime magnetic calibration
		if (canUseMagneticData()) {
			updateCalibrationData(&calibration->MagnetData, rawValues.MagnetX, rawValues.MagnetY, rawValues.MagnetZ, true);

			calibration->MagneticFieldStrength = FILTER(calibration->MagneticFieldStrength, getMagnetStrength(), 0.05f);
			lastMagnetData = millis();
		}
		else if (millis() - lastMagnetData > 10000) {
			resetMagnet();
			lastMagnetData = millis();
		}

		// IMU
		if (!hasIMU())
			calculateIMU();

		if (calibrationOrientation)
			calibrateOrientation();
	}


	// Data Interval messen
	if (memcmp(&last, &values, sizeof(GyroValues)) != 0) {
		Profiler::restart("Gyro::data()");
		last = values;
	}

	Profiler::end();
}

void Gyro::calculateIMU() {
	Profiler::begin("Gyro::calculateIMU()");
	float dt = CYCLE_GYRO / 1000.f;

	if (values.RawGyroX < gyroCalibration.Min[0] || values.RawGyroX > gyroCalibration.Max[0])
		roll += values.GyroX * dt;
	if (values.RawGyroY < gyroCalibration.Min[1] || values.RawGyroY > gyroCalibration.Max[1])
		pitch += values.GyroY * dt;
	if (values.RawGyroZ < gyroCalibration.Min[2] || values.RawGyroZ > gyroCalibration.Max[2])
		yaw += values.GyroZ * dt;

	if (!isAccMoving()) {
		float accRoll = -MathHelper::toDegress(atan(values.AccY / sqrt(values.AccX*values.AccX + values.AccZ*values.AccZ)));
		float accPitch = MathHelper::toDegress(atan(values.AccX / sqrt(values.AccY*values.AccY + values.AccZ*values.AccZ)));

		roll = FILTER(roll, accRoll, 0.05f);
		pitch = FILTER(pitch, accPitch, 0.05f);
	}
	if (canUseMagneticData()) {
		float magRoll = MathHelper::toDegress(atan(values.MagnetY / sqrt(values.MagnetX*values.MagnetX + values.MagnetZ*values.MagnetZ)));
		float magPitch = -MathHelper::toDegress(atan(values.MagnetX / sqrt(values.MagnetY*values.MagnetY + values.MagnetZ*values.MagnetZ)));

		float r = MathHelper::toRadians(roll);
		float p = MathHelper::toRadians(pitch);
		float xa = values.MagnetX*cos(p) + values.MagnetZ*sin(p);
		float ya = values.MagnetX*sin(r)*sin(p) + values.MagnetY*cos(r) - values.MagnetZ*sin(r)*cos(p);

		float magYaw = -MathHelper::toDegress(atan2(ya, xa));

		roll = FILTER(roll, magRoll, 0.01f);
		pitch = FILTER(pitch, magPitch, 0.01f);
		yaw = FILTER(yaw, magYaw, 0.025f);
	}

	Profiler::end();
}

void Gyro::beginCalibration() {
	if (inCalibration())
		return;

	calibrationRunning = true;
	calibrationOrientation = false;
	calibrationCount = 0;

	resetCalibration(&gyroCalibration);
}

void Gyro::beginMagnetCalibration() {
	if (inCalibration())
		return;
	if (!hasMagnetometer())
		return;

	calibrationMagnet = true;
	calibration->MagneticFieldStrength = 0;
	resetCalibration(&calibration->MagnetData);
}

bool Gyro::inCalibration() {
	return calibrationRunning || calibrationMagnet || calibrationOrientation;
}

GyroValues Gyro::getValues() const {
	return values;
}

float Gyro::getRoll() const {
	return MathHelper::fixValue(roll - orientationCalibration.Average[0], -180, 180);
}

float Gyro::getPitch() const {
	return MathHelper::fixValue(pitch - orientationCalibration.Average[1], -180, 180);
}

float Gyro::getYaw() const {
	float offset = 0;
	if (!hasMagnetometer())
		offset = orientationCalibration.Average[2];

	return MathHelper::fixValue(yaw - offset, 0, 360);
}

float Gyro::getMagnetStrength() const {
	float x = values.MagnetX;
	float y = values.MagnetY;
	float z = values.MagnetZ;

	return sqrtf(x*x + y*y + z*z);
}

boolean Gyro::isMagnetInterferenced() const {
	if (calibration->MagneticFieldStrength <= 0)
		return false;

	return abs(getMagnetStrength() - calibration->MagneticFieldStrength) >= 3;
}

boolean Gyro::isAccMoving() const {
	float x = values.AccX;
	float y = values.AccY;
	float z = values.AccZ;

	float len = sqrtf(x*x + y*y + z*z);
	return len < 0.98f || len > 1.02f;
}

boolean Gyro::canUseMagneticData() const {
	return hasMagnetometer() && calibration->MagneticFieldStrength > 0 && !isMagnetInterferenced();
}


#define GYRO_MOVING(x) (abs(x) > (10 * (2000.0f / 8196.0f)))
boolean Gyro::isGyroMoving() const {
	return GYRO_MOVING(values.RawGyroX) || GYRO_MOVING(values.RawGyroY) || GYRO_MOVING(values.RawGyroZ);
}

boolean Gyro::isMoving() const {
	return isAccMoving() || isGyroMoving();
}

#define GYRO_FLAT(x) (abs(x) < 1.0f)

boolean Gyro::isFlat() const {
	return GYRO_FLAT(getRoll()) && GYRO_FLAT(getPitch());
}