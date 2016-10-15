#include "DroneEngine.h"

DroneEngine::DroneEngine(SensorHAL* sensor, ServoManager* servos, Config* config) {
	this->sensor = sensor;
	this->servos = servos;
	this->config = config;

	this->lastHeartbeat = 0;
	this->lastMovementUpdate = 0;

	this->pitchPID = NULL;
	this->rollPID = NULL;
	this->yawPID = NULL;

	this->anglePitchPID = NULL;
	this->angleRollPID = NULL;

	createPID();

	_state = StateReset;
	_stopReason = None;
	servos->setAllServos(config->ServoMin);
}

void DroneEngine::createPID() {
	if (pitchPID)
		delete pitchPID;
	if (rollPID)
		delete rollPID;
	if (yawPID)
		delete yawPID;
	if (anglePitchPID)
		delete anglePitchPID;
	if (angleRollPID)
		delete angleRollPID;

	pitchPID = createPID(config->PitchPid, 35, &pitchOutput);
	rollPID = createPID(config->RollPid, 35, &rollOutput);
	yawPID = createPID(config->YawPid, 35, &yawOutput);

	anglePitchPID = createPID(config->AngleStabilization, 200, &anglePitchOutput);
	angleRollPID = createPID(config->AngleStabilization, 200, &angleRollOutput);
}

PID* DroneEngine::createPID(PIDSettings settings, double limit, double* output) {
	PID* pid = new PID(&pidInput, output, &pidSetpoint, settings.Kp, settings.Ki, settings.Kd, DIRECT);
	pid->SetMode(AUTOMATIC);
	pid->SetSampleTime(CYCLE_PID);
	pid->SetOutputLimits(-limit, limit);
	return pid;
}

bool DroneEngine::isGyroSafe() {
	return abs(sensor->getGyro()->getRoll()) <= config->SafeRoll && abs(sensor->getGyro()->getPitch()) <= config->SafePitch;
}

void DroneEngine::arm() {
	if (_state == StateIdle) {
		if (sensor->getGyro()->inCalibration()) {
			blinkLED(5, 300);
			Log::info("Engine", "Can not arm motors, gyro is in calibration");
			return;
		}
		if (!sensor->getGyro()->hasValidGyroData()) {
			blinkLED(5, 300);
			Log::info("Engine", "Can not arm motors, gyro data is not valid");
			return;
		}
		if (!isGyroSafe()) {
			blinkLED(5, 300);
			Log::info("Engine", "Can not arm motors, gyro is not safe");
			return;
		}
		if (config->OnlyArmWhenStill && (sensor->getGyro()->isMoving() || !sensor->getGyro()->isFlat())) {
			blinkLED(5, 300);
			Log::info("Engine", "Can not arm motors, not still");
			return;
		}

		servos->setAllServos(config->ServoMin);
		_state = StateArmed;
		blinkLED(3, 600);

		Log::info("Engine", "Armed motors");
	}
}

void DroneEngine::disarm() {
	if (_state == StateArmed || _state == StateFlying) {
		servos->setAllServos(config->ServoMin);

		_state = StateIdle;
		Log::info("Engine", "Disarmed motors");
	}
}

void DroneEngine::fly() {
	// entweder schon im Flying Zustand
	// oder nicht Armed
	if (_state != StateArmed)
		return;

	if (thrust > config->MaxThrustForFlying)
		return;

	pitchOutput = 0;
	rollOutput = 0;
	yawOutput = 0;

	anglePitchOutput = 0;
	angleRollOutput = 0;

	createPID();

	_state = StateFlying;
	blinkLED(3, 550);
	Log::info("Engine", "Flying");
}

void DroneEngine::stop(StopReason reason) {
	if (_state == StateOTA)
		return;

	disarm();
	blinkLED(5, 250);

	_stopReason = reason;
	_state = StateStopped;
	Log::info("Engine", "Stopped! reason: %d", reason);
}

void DroneEngine::clearStatus() {
	if (_state == StateStopped) {
		Log::info("Engine", "Resetting gyro, because clearStatus()");
		sensor->getGyro()->reset();
	}

	if (_state == StateReset || _state == StateStopped) {
		_state = StateIdle;
		Log::info("Engine", "Status cleared");
	}
}

bool DroneEngine::beginOTA() {
	if (_state != StateFlying) {
		disarm();

		_state = StateOTA;
		Log::info("Engine", "Now in OTA state");
		return true;
	}
	return false;
}

void DroneEngine::endOTA() {
	if (_state == StateOTA) {
		_state = StateIdle;
		Log::info("Engine", "Stopped OTA state");
	}
}

void DroneEngine::handle() {
	Profiler::begin("DroneEngine::handle()");

	if (_state == StateArmed || _state == StateFlying) {
		blinkLED(1, 800);

		if (millis() - lastHeartbeat >= config->MaximumNetworkTimeout) {
			stop(NoPing);
			Profiler::end();
			return;
		}

		if (!isGyroSafe()) {
			stop(InvalidGyro);
			Profiler::end();
			return;
		}

		if (_state == StateFlying) {
			if (millis() - lastMovementUpdate >= config->MaximumNetworkTimeout) {
				stop(NoData);
				Profiler::end();
				return;
			}

			handleInternal();
		}
	}
	Profiler::end();
}

void DroneEngine::handleInternal() {
	int values[4];

	GyroValues gyroValues = sensor->getGyro()->getValues();

    float pitchCmd = targetPitch << 1;
	float rollCmd = targetRoll << 1;

	if (config->EnableStabilization) {
		if (sensor->getGyro()->hasValidImuData()) {
			calculatePID(anglePitchPID, sensor->getGyro()->getPitch(), targetPitch / 50.0f);
			calculatePID(angleRollPID, sensor->getGyro()->getRoll(), targetRoll / 50.0f);

			pitchCmd = -anglePitchOutput;
			rollCmd = -angleRollOutput;
		}
		else
			FaultManager::fault(FaultInvalidSensorData, "DroneEngine", "hasValidImuData()");
	}

	if (sensor->getGyro()->hasValidGyroData()) {
		if (config->UseFilteredGyroEngine) {
			float res = 8196.0f / 2000.0f;
			calculatePID(rollPID, gyroValues.GyroX * res, rollCmd);
			calculatePID(pitchPID, gyroValues.GyroY* res, pitchCmd);
			calculatePID(yawPID, gyroValues.GyroZ * res, targetYaw << 1);
		}
		else {
			calculatePID(rollPID, gyroValues.RawGyroX, rollCmd);
			calculatePID(pitchPID, gyroValues.RawGyroY, pitchCmd);
			calculatePID(yawPID, gyroValues.RawGyroZ, targetYaw << 1);
		}
	}
	else {
		FaultManager::fault(FaultInvalidSensorData, "DroneEngine", "hasValidGyroData()");

		rollPID = 0;
		pitchPID = 0;
		yawPID = 0;
	}

	uint16_t minServo = config->ServoMin;
	if (config->KeepMotorsOn)
		minServo = config->ServoIdle;

	for (int i = 0; i < 4; i++)
		values[i] = (int)MathHelper::clampValue(minServo + thrust + MathHelper::mixMotor(config, i, pitchOutput, rollOutput, yawOutput), config->ServoMin, config->ServoMax);

	servos->setServos(values[0], values[1], values[2], values[3]);

	if (memcmp(values, lastValues, sizeof(lastValues)) != 0)
	{
		Profiler::restart("DroneEngine::data()");
		memcpy(lastValues, values, sizeof(lastValues));
	}
}

void DroneEngine::updateTunings() {
	pitchPID->SetTunings(config->PitchPid.Kp, config->PitchPid.Ki, config->PitchPid.Kd);
	rollPID->SetTunings(config->RollPid.Kp, config->RollPid.Ki, config->RollPid.Kd);
	yawPID->SetTunings(config->YawPid.Kp, config->YawPid.Ki, config->YawPid.Kd);

	anglePitchPID->SetTunings(config->AngleStabilization.Kp, config->AngleStabilization.Ki, config->AngleStabilization.Kd);
	angleRollPID->SetTunings(config->AngleStabilization.Kp, config->AngleStabilization.Ki, config->AngleStabilization.Kd);
}

void DroneEngine::calculatePID(PID* pid, float input, float setpoint) {
	this->pidInput = input;
	this->pidSetpoint = setpoint;
	pid->Compute();
}


void DroneEngine::setRawServoValues(int fl, int fr, int bl, int br) const {
	if(_state == StateArmed)
		servos->setServos(fl, fr, bl, br);
}

void DroneEngine::setRawServoValues(int all) const {
	setRawServoValues(all, all, all, all);
}

void DroneEngine::heartbeat() {
	lastHeartbeat = millis();
}

DroneState DroneEngine::state() const {
	return _state;
}

StopReason DroneEngine::getStopReason() const {
	return _stopReason;
}


void DroneEngine::setTargetMovement(int16_t roll, int16_t pitch, int16_t yaw, int16_t thrust) {
	if (_state != StateArmed && _state != StateFlying)
		return;

	// Werte in richtigen Bereich bringen und setzen
	this->targetPitch = MathHelper::clampValue(pitch, -500, 500);
	this->targetRoll = MathHelper::clampValue(roll, -500, 500);
	this->targetYaw = MathHelper::clampValue(yaw, -500, 500);
	this->thrust = MathHelper::clampValue(thrust, 0, config->ServoMax - config->ServoMin);

	// in den Fliegen Modus gehen
	fly();
	lastMovementUpdate = millis();

	Profiler::restart("DroneEngine::input()");
}

int DroneEngine::getTargetRoll() const {
	return targetRoll;
}

int DroneEngine::getTargetPitch() const {
	return targetPitch;
}

int DroneEngine::getTargetYaw() const {
	return targetYaw;
}

int DroneEngine::getThrust() const {
	return thrust;
}

float DroneEngine::getPitchOutput() const {
	return pitchOutput;
}

float DroneEngine::getRollOutput() const {
	return rollOutput;
}

float DroneEngine::getYawOutput() const {
	return yawOutput;
}