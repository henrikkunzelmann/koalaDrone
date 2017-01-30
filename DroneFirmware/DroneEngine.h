#ifndef _DRONEENGINE_h
#define _DRONEENGINE_h

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

	float maxTilt;
	float maxRotationSpeed;

	int lastValues[4];

 protected:
	 Config* config;

	 DroneState _state;
	 StopReason _stopReason;

	 SensorHAL* sensor;
	 ServoManager* servos;

	 int16_t targetRoll;
	 int16_t targetPitch;
	 int16_t targetYaw;
	 int16_t thrust;

	 double pidInput;
	 double pidSetpoint;
	 
	 double pitchOutput;
	 double rollOutput;
	 double yawOutput;

	 double anglePitchOutput;
	 double angleRollOutput;
	 
	 PID* pitchPID;
	 PID* rollPID;
	 PID* yawPID;

	 PID* anglePitchPID;
	 PID* angleRollPID;

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

	int getTargetRoll() const;
	int getTargetPitch() const;
	int getTargetYaw() const;
	int getThrust() const;

	float getPitchOutput() const;
	float getRollOutput() const;
	float getYawOutput() const;
};

#endif

