#include "Gyro.h"

Gyro::Gyro(Config* config) {
	this->config = config;

	memset(&last, 0, sizeof(GyroValues));
	memset(&values, 0, sizeof(GyroValues));
	this->gyroOffset[0] = 0;
	this->gyroOffset[1] = 0; 
	this->gyroOffset[2] = 0;
	this->lastSample = micros();
}

void Gyro::update() {
	// nur alle 10 Millisekunden Daten einlesen
	uint32_t interval = micros() - lastSample;
	if (interval < CYCLE_GYRO * 1000)
		return;
	Profiler::pushData("Gyro::jitter()", interval - CYCLE_GYRO * 1000);
	lastSample = micros();

	Profiler::begin("Gyro::update()");

	if (calibration) {
		GyroValues calibrationValues;
		getValues(&calibrationValues);

		gyroOffset[0] += calibrationValues.RawGyroX;
		gyroOffset[1] += calibrationValues.RawGyroY;
		gyroOffset[2] += calibrationValues.RawGyroZ;

		if (++calibrationCount > 5000 / CYCLE_GYRO) {
			for (int i = 0; i < 3; i++)
				gyroOffset[i] /= calibrationCount;

			calibration = false;
		}
	}
	else {
		getValues(&values);
		values.RawGyroX -= gyroOffset[0];
		values.RawGyroY -= gyroOffset[1];
		values.RawGyroZ -= gyroOffset[2];

		float gyroRes = 2000.0f / 8196.0f;
		values.GyroX = FILTER(last.RawGyroX, values.RawGyroX, 0.05f) * gyroRes;
		values.GyroY = FILTER(last.RawGyroY, values.RawGyroY, 0.05f) * gyroRes;
		values.GyroZ = FILTER(last.RawGyroZ, values.RawGyroZ, 0.05f) * gyroRes;

		values.AccX = FILTER(last.AccX, values.AccX, 0.05f);
		values.AccY = FILTER(last.AccY, values.AccY, 0.05f);
		values.AccZ = FILTER(last.AccZ, values.AccZ, 0.05f);

		// IMU
		if (!hasIMU())
			calculateIMU();

		// Data Interval messen
		if (memcmp(&last, &values, sizeof(GyroValues)) != 0) {
			Profiler::restart("Gyro::data()");

			last = values;
		}
	}
	Profiler::end();
}

void Gyro::calculateIMU() {
	Profiler::begin("Gyro::calculateIMU()");
	float dt = CYCLE_GYRO / 1000.f;

	pitch -= values.GyroX * dt;
	roll += values.GyroY * dt;

	if (values.GyroZ > 0.5f) 
		yaw += values.GyroZ * dt;

	float fastAccLen = abs(values.AccX) + abs(values.AccY) + abs(values.AccZ);

	if (fastAccLen > 0.5f && fastAccLen < 2.0f)
	{
		float accPitch = atan(values.AccX / sqrt(values.AccY*values.AccY + values.AccZ*values.AccZ)) * 180 / PI;
		float accRoll = atan(values.AccY / sqrt(values.AccX*values.AccX + values.AccZ*values.AccZ)) * 180 / PI;

		pitch = FILTER(pitch, accPitch, 0.05f);
		roll = FILTER(roll, accRoll, 0.05f);
	}
	Profiler::end();
}

void Gyro::calibrate() {
	calibration = true;
	calibrationCount = 0;

	gyroOffset[0] = 0;
	gyroOffset[1] = 0;
	gyroOffset[2] = 0;
}

bool Gyro::inCalibration() {
	return calibration;
}

GyroValues Gyro::getValues() const {
	return values;
}

float Gyro::getRoll() const {
	return MathHelper::fixValue(roll, -90, 90);
}

float Gyro::getPitch() const {
	return MathHelper::fixValue(pitch, -90, 90);
}

float Gyro::getYaw() const {
	return MathHelper::fixValue(yaw, 0, 360);
}

#define GYRO_MOVING(x) (abs(x) > 4.0f)

boolean Gyro::isMoving() const {
	boolean accMoving = GYRO_MOVING(values.AccX) || GYRO_MOVING(values.AccY) || GYRO_MOVING(values.AccZ);
	boolean gyroMoving = GYRO_MOVING(values.GyroX) || GYRO_MOVING(values.GyroY) || GYRO_MOVING(values.GyroZ);
	return accMoving || gyroMoving;
}

#define GYRO_FLAT(x) (abs(x) < 1.0f)

boolean Gyro::isFlat() const {
	return GYRO_FLAT(getRoll()) && GYRO_FLAT(getPitch());
}