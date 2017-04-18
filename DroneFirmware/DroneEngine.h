#pragma once

#include <Arduino.h>

#include "CycleTimes.h"
#include "ServoManager.h"
#include "SensorHAL.h"
#include "MathHelper.h"
#include "Quadcopter.h"
#include "Log.h"
#include "LED.h"
#include "StopReason.h"
#include "Profiler.h"
#include "PID.h"

enum DroneState {
	StateUnknown,
	StateReset,
	StateOTA,
	StateStopped,
	StateIdle,
	StateArmed,
	StateFlying
};

class DroneEngine
{
 private:
	long lastMovementUpdate;
	long lastHeartbeat;

	int lastValues[4];

 protected:
	 Config* config;

	 DroneState _state;
	 StopReason _stopReason;

	 SensorHAL* sensor;
	 ServoManager* servos;

	 int16_t targetGyroX;
	 int16_t targetGyroY;
	 int16_t targetGyroZ;
	 int16_t thrust;

	 double pidInput;
	 double pidSetpoint;
	 
	 double rollOutput;
	 double pitchOutput;
	 double yawOutput;

	 double angleRollOutput;
	 double anglePitchOutput;
	 double angleYawOutput;
	 
	 PID* rollPID;
	 PID* pitchPID;
	 PID* yawPID;

	 PID* angleRollPID;
	 PID* anglePitchPID;
	 PID* angleYawPID;

	 void createPID();
	 PID* createPID(PIDSettings settings, double limit, double* output);

	 void calculatePID(PID* pid, float input, float setpoint);

	 bool isGyroSafe();

 public:
	explicit DroneEngine(SensorHAL* sensor, ServoManager* servos, Config* config);

	void arm();
	void disarm();
	void fly();
	void stop(StopReason reason);
	void clearStatus();
	
	bool beginOTA();
	void endOTA();

	DroneState state() const;
	StopReason getStopReason() const;

	bool isStateIdle() const;
	bool areMotorsRunning() const;
	
	void handle();
	void handleInternal();

	void updateTunings();
	void updateTuning(PID* pid, PIDSettings settings);

	void heartbeat();

	void setRawServoValues(int fl, int fr, int bl, int br) const;
	void setRawServoValues(int all) const;

	void setTargetMovement(int16_t roll, int16_t pitch, int16_t yaw, int16_t thrust);

	int getTargetGyroX() const;
	int getTargetGyroY() const;
	int getTargetGyroZ() const;
	int getThrust() const;

	float getRollOutput() const;
	float getPitchOutput() const;
	float getYawOutput() const;

	float getAngleRollOutput() const;
	float getAnglePitchOutput() const;
	float getAngleYawOutput() const;
};
