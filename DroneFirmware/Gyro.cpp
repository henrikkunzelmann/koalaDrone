#include "Gyro.h"

Gyro::Gyro(Config* config) {
	this->config = config;

	memset(&last, 0, sizeof(GyroValues));
	memset(&values, 0, sizeof(GyroValues));
	for (int i = 0; i < 3; i++) {
		gyroOffset[i] = 0;
		minGyro[i] = 0;
		maxGyro[i] = 0;
	}
	this->lastSample = micros();
}

void Gyro::update() {
	uint32_t interval = micros() - lastSample;
	if (interval < CYCLE_GYRO * 1000)
		return;
	Profiler::pushData("Gyro::jitter()", interval - CYCLE_GYRO * 1000);
	lastSample = micros();

	Profiler::begin("Gyro::update()");

	if (calibration) {
		getValues(&values);

		gyroOffset[0] += values.RawGyroX;
		gyroOffset[1] += values.RawGyroY;
		gyroOffset[2] += values.RawGyroZ;

		minGyro[0] = min(minGyro[0], values.RawGyroX);
		minGyro[1] = min(minGyro[1], values.RawGyroY);
		minGyro[2] = min(minGyro[2], values.RawGyroZ);

		maxGyro[0] = max(maxGyro[0], values.RawGyroX);
		maxGyro[1] = max(maxGyro[1], values.RawGyroY);
		maxGyro[2] = max(maxGyro[2], values.RawGyroZ);

		if (++calibrationCount > 5000 / CYCLE_GYRO) {
			for (int i = 0; i < 3; i++) {
				gyroOffset[i] /= calibrationCount;
				minGyro[i] *= 1.25f;
				maxGyro[i] *= 1.25f;
			}

			calibration = false;
			calibrationOrientation = true;
			calibrationCount = 0;
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

		if (calibrationOrientation) {
			rollOffset += roll;
			pitchOffset += pitch;
			yawOffset += yaw;

			if (++calibrationCount > 2000 / CYCLE_GYRO) {
				rollOffset /= calibrationCount;
				pitchOffset /= calibrationCount;
				yawOffset /= calibrationCount;

				calibrationOrientation = false;
			}
		}

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

	if (values.RawGyroX < minGyro[0] || values.RawGyroX > maxGyro[0])
		pitch -= values.GyroX * dt;
	if (values.RawGyroY < minGyro[1] || values.RawGyroY > maxGyro[1])
		roll += values.GyroY * dt;
	if (values.RawGyroZ < minGyro[2] || values.RawGyroZ > maxGyro[2])
		yaw += values.GyroZ * dt;

	if (!isAccMoving()) {
		float accPitch = atan(values.AccX / sqrt(values.AccY*values.AccY + values.AccZ*values.AccZ)) * 180 / PI;
		float accRoll = atan(values.AccY / sqrt(values.AccX*values.AccX + values.AccZ*values.AccZ)) * 180 / PI;

		pitch = FILTER(pitch, accPitch, 0.05f);
		roll = FILTER(roll, accRoll, 0.05f);
	}
	if (hasMagnetometer()) {
		float magPitch = MathHelper::toDegress(atan(values.MagnetX / sqrt(values.MagnetY*values.MagnetY + values.MagnetZ*values.MagnetZ)));
		float magRoll = MathHelper::toDegress(atan(values.MagnetY / sqrt(values.MagnetX*values.MagnetX + values.MagnetZ*values.MagnetZ)));
		float magYaw = MathHelper::toDegress(atan2(values.MagnetY, values.MagnetX));

		pitch = FILTER(pitch, magPitch, 0.025f);
		roll = FILTER(roll, magRoll, 0.025f);
		yaw = FILTER(roll, magYaw, 0.025f);
	}
	Profiler::end();
}

void Gyro::calibrate() {
	calibration = true;
	calibrationOrientation = false;
	calibrationCount = 0;

	for (int i = 0; i < 3; i++) {
		gyroOffset[i] = 0;
		minGyro[i] = 0;
		maxGyro[i] = 0;
	}
}

bool Gyro::inCalibration() {
	return calibration || calibrationOrientation;
}

GyroValues Gyro::getValues() const {
	return values;
}

float Gyro::getRoll() const {
	return MathHelper::fixValue(roll - (calibrationOrientation ? 0 : rollOffset), -90, 90);
}

float Gyro::getPitch() const {
	return MathHelper::fixValue(pitch - (calibrationOrientation ? 0 : pitchOffset), -90, 90);
}

float Gyro::getYaw() const {
	float offset = 0;
	if (!calibrationOrientation && !hasMagnetometer())
		offset = yawOffset;
	return MathHelper::fixValue(yaw - offset, 0, 360);
}

boolean Gyro::isAccMoving() const {
	float x = values.AccX;
	float y = values.AccY;
	float z = values.AccZ;

	float len = sqrtf(x*x + y*y + z*z);
	return len < 0.75f || len > 1.25f;
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