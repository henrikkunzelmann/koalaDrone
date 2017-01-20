#pragma once

#include <Arduino.h>

#include "Config.h"

class Quadcopter
{
public:
	static float mixMotor(Config* config, int motorIndex, float roll, float pitch, float yaw);
};