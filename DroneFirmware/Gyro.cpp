#include "Gyro.h"

Gyro::Gyro(Config* config) {
	this->config = config;
	this->calibration = &config->SensorCalibrationData;

	memset(&last, 0, sizeof(GyroValues));
	memset(&rawValues, 0, sizeof(GyroValues));
	memset(&values, 0, sizeof(GyroValues));
	resetCalibration(&gyroCalibration);
	resetCalibration(&accCalibration);
	resetCalibration(&orientationCalibration);

	this->firstSample = true;
	this->lastSample = micros();
	this->lastMagnetData = millis();

	this->validGyroData = false;
	this->validAccData = false;
	this->validMagData = false;
	this->validImu = false;
}

Gyro::~Gyro() {

}

void Gyro::resetCalibration(CalibrationData* data) {
	for (int i = 0; i < 3; i++) {
		data->Min[i] = MAXFLOAT;
		data->Max[i] = -MAXFLOAT;
		data->Average[i] = 0;
	}

	data->Length = 0;
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

	float ax = data->Average[0];
	float ay = data->Average[1];
	float az = data->Average[2];
	data->Length = sqrtf(ax*ax + ay*ay + az*az);
	yield();
}

void Gyro::logCalibration(CalibrationData* data) {
	Log::debug("Gyro", "min: %f, %f, %f", data->Min[0], data->Min[1], data->Min[2]);
	Log::debug("Gyro", "max: %f, %f, %f", data->Max[0], data->Max[1], data->Max[2]);
	Log::debug("Gyro", "average: %f, %f, %f", data->Average[0], data->Average[1], data->Average[2]);
	Log::debug("Gyro", "length: %f", data->Length);
}

void Gyro::beginCalibration(CalibrationState state) {
	if (state == CalibrationMagnet && !hasMagnetometer()) {
		Log::info("Gyro", "Magnetic calibration started but no magnetometer found");
		return;
	}

	calibrationState = state;

	firstSample = true;
	calibrationCount = 1;
	for (int i = 0; i < 3; i++)
		calibrationSum[i] = 0;


	if (!inCalibration())
		return;

	Log::info("Gyro", "Starting calibration %d", state);
	switch (state) {
	case CalibrationGyro:
		resetCalibration(&gyroCalibration);
		break;
	case CalibrationAcc:
		resetCalibration(&accCalibration);
		break;
	case CalibrationOrientation:
		resetCalibration(&orientationCalibration);
		break;
	case CalibrationMagnet:
		resetCalibration(&calibration->MagnetCalibration);
		break;
	default:
		Log::error("Gyro", "Unknown calibration state: %d", calibrationState);
		calibrationState = CalibrationNone;
		return;
	}
}

void Gyro::runCalibration() {
	if (!inCalibration())
		return;

	switch (calibrationState) {
	case CalibrationGyro:
		if (!validGyroData)
			return;

		updateCalibrationData(&gyroCalibration, rawValues.GyroX, rawValues.GyroY, rawValues.GyroZ, false);
		break;
	case CalibrationAcc:
		if (!validAccData)
			return;

		updateCalibrationData(&accCalibration, rawValues.AccX, rawValues.AccY, rawValues.AccZ, false);
		break;
	case CalibrationOrientation:
		if (!validImu)
			return;

		updateCalibrationData(&orientationCalibration, roll, pitch, yaw, false);
		break;
	case CalibrationMagnet:
		if (!canUseMagneticData())
			return;

		updateCalibrationData(&calibration->MagnetCalibration, rawValues.MagnetX, rawValues.MagnetY, rawValues.MagnetZ, true);
		break;
	default:
		Log::error("Gyro", "Unknown calibration state: %d", calibrationState);
		calibrationState = CalibrationNone;
		return;
	}
	int32_t time[] = { 0, 5000, 5000, 2000, 20000 };

	if (++calibrationCount > time[calibrationState] / CYCLE_GYRO) {
		Log::info("Gyro", "Done with calibration.");
		
		switch (calibrationState) {
		case CalibrationGyro:
			Log::debug("Gyro", "Calibration data for gyro:");
			logCalibration(&gyroCalibration);

			beginCalibration(CalibrationAcc);
			break;
		case CalibrationAcc:
			Log::debug("Gyro", "Calibration data for acc:");
			logCalibration(&accCalibration);

			beginCalibration(CalibrationOrientation);
			break;
		case CalibrationOrientation:
			Log::debug("Gyro", "Calibration data for orientation:");
			logCalibration(&orientationCalibration);
			beginCalibration(CalibrationNone);
			break;

		default:
			beginCalibration(CalibrationNone);
			break;
		}
		firstSample = true;
	}
	yield();
}

void Gyro::processData() {
	values = rawValues;

	// Werte überprüfen
	const float gyroRange = 500.0f;
	const float accRange = 5.0f;

	validGyroData = true;
	validAccData = true;
	if (values.GyroX < -gyroRange || values.GyroX > gyroRange ||
		values.GyroY < -gyroRange || values.GyroY > gyroRange ||
		values.GyroZ < -gyroRange || values.GyroZ > gyroRange) {
		Log::debug("Gyro", "invalid gyrox: %f, gyroy: %f, gyroz: %f", values.GyroX, values.GyroY, values.GyroZ);
		FaultManager::fault(FaultInvalidSensorData, "Gyro", "filterData()");
		validGyroData = false;
	}
	if (values.AccX < -accRange || values.AccX > accRange ||
		values.AccY < -accRange || values.AccY > accRange ||
		values.AccZ < -accRange || values.AccZ > accRange) {
		Log::debug("Gyro", "invalid accx: %f, accy: %f, accz: %f", values.AccX, values.AccY, values.AccZ);
		FaultManager::fault(FaultInvalidSensorData, "Acc", "filterData()");
		validAccData = false;
	}

	// Calibration
	values.GyroX -= gyroCalibration.Average[0];
	values.GyroY -= gyroCalibration.Average[1];
	values.GyroZ -= gyroCalibration.Average[2];

	values.MagnetX -= calibration->MagnetCalibration.Average[0];
	values.MagnetY -= calibration->MagnetCalibration.Average[1];
	values.MagnetZ -= calibration->MagnetCalibration.Average[2];

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
	
	validMagData = canUseMagneticData();
	yield();
}

void Gyro::update() {
	uint32_t interval = micros() - lastSample;
	if (interval < CYCLE_GYRO * 1000)
		return;
	Profiler::pushData("Gyro::jitter()", interval - CYCLE_GYRO * 1000);
	lastSample = micros();

	Profiler::begin("Gyro::update()");

	validGyroData = false;
	validAccData = false;
	validMagData = false;
	validImu = false;

	if (getValues(&rawValues))
		processData();

	if (inCalibration() && calibrationState != CalibrationOrientation)
		runCalibration();
	else {
		// Runtime magnetic calibration
		if (canUseMagneticData()) {
			updateCalibrationData(&calibration->MagnetCalibration, rawValues.MagnetX, rawValues.MagnetY, rawValues.MagnetZ, true);
			lastMagnetData = millis();
		}
		else if (millis() - lastMagnetData > 10000) {
			resetMagnet();
			lastMagnetData = millis();
		}

		yield();

		// IMU
		if (!hasIMU())
			calculateIMU();

		if (calibrationState == CalibrationOrientation)
			runCalibration();

		yield();
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

	if (hasValidGyroData()) {
		if (isGyroXRotating())
			roll += values.GyroX * dt;
		if (isGyroYRotating())
			pitch += values.GyroY * dt;
		if (isGyroZRotating())
			yaw += values.GyroZ * dt;
	}

	if (config->EnableImuAcc && !isAccMoving()) {
		float accRoll = -MathHelper::toDegress(atan(values.AccY / sqrtf(values.AccX*values.AccX + values.AccZ*values.AccZ)));
		float accPitch = MathHelper::toDegress(atan(values.AccX / sqrtf(values.AccY*values.AccY + values.AccZ*values.AccZ)));

		roll = FILTER(roll, accRoll, 0.05f);
		pitch = FILTER(pitch, accPitch, 0.05f);
	}
	if (config->EnableImuMag && canUseMagneticData()) {
		float magRoll = MathHelper::toDegress(atan(values.MagnetY / sqrtf(values.MagnetX*values.MagnetX + values.MagnetZ*values.MagnetZ)));
		float magPitch = -MathHelper::toDegress(atan(values.MagnetX / sqrtf(values.MagnetY*values.MagnetY + values.MagnetZ*values.MagnetZ)));

		float r = MathHelper::toRadians(roll);
		float p = MathHelper::toRadians(pitch);
		float xa = values.MagnetX*cos(p) + values.MagnetZ*sin(p);
		float ya = values.MagnetX*sin(r)*sin(p) + values.MagnetY*cos(r) - values.MagnetZ*sin(r)*cos(p);

		float magYaw = -MathHelper::toDegress(atan2(ya, xa));

		roll = FILTER(roll, magRoll, 0.01f);
		pitch = FILTER(pitch, magPitch, 0.01f);
		yaw = FILTER(yaw, magYaw, 0.025f);
	}

	validImu = true;

	Profiler::end();
}

bool Gyro::inCalibration() {
	return calibrationState != CalibrationNone;
}

boolean Gyro::hasValidGyroData() const {
	return validGyroData;
}

boolean Gyro::hasValidMagnetData() const {
	return validMagData;
}

boolean Gyro::hasValidImuData() const {
	return validImu;
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
	float len = calibration->MagnetCalibration.Length;
	if (len <= 0)
		return false;

	return abs(getMagnetStrength() - len) >= 3;
}

boolean Gyro::isAccMoving() const {
	float x = values.AccX;
	float y = values.AccY;
	float z = values.AccZ;

	float len2 = sqrtf(x*x + y*y + z*z);
	return len2 < accCalibration.Length * 0.98 || len2 > accCalibration.Length * 1.02;
}

boolean Gyro::canUseMagneticData() const {
	return hasMagnetometer() && calibration->MagnetCalibration.Length > 0 && !isMagnetInterferenced();
}

#define GYRO_ROTATING_FACTOR 5.0f
boolean Gyro::isGyroXRotating() const {
	return values.GyroX < gyroCalibration.Min[0] * GYRO_ROTATING_FACTOR || values.GyroX > gyroCalibration.Max[0] * GYRO_ROTATING_FACTOR;
}

boolean Gyro::isGyroYRotating() const {
	return values.GyroY < gyroCalibration.Min[1] * GYRO_ROTATING_FACTOR || values.GyroY > gyroCalibration.Max[1] * GYRO_ROTATING_FACTOR;
}

boolean Gyro::isGyroZRotating() const {
	return values.GyroZ < gyroCalibration.Min[2] * GYRO_ROTATING_FACTOR || values.GyroZ > gyroCalibration.Max[2] * GYRO_ROTATING_FACTOR;
}

boolean Gyro::isGyroRotating() const {
	return isGyroXRotating() || isGyroYRotating() || isGyroZRotating();
}

boolean Gyro::isMoving() const {
	return isAccMoving() || isGyroRotating();
}

#define GYRO_FLAT(x) (abs(x) < 1.0f)

boolean Gyro::isFlat() const {
	return GYRO_FLAT(getRoll()) && GYRO_FLAT(getPitch());
}