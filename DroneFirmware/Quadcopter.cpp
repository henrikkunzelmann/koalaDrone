#include "Quadcopter.h"

// FL --- FR  CW  --- CCW
// |      |    |       |
// |      |    |       |
// BL --- BR  CCW --- CW

// 0--1   
// |  |
// 2--3

#define _CW -1
#define _CCW 1

float motorsRoll[] = {
	1, -1,
	1, -1
};
float motorsPitch[] = { 
	1,  1, 
	-1, -1 
};
float motorsYaw[] = { 
	_CW, _CCW,
	_CCW, _CW
};


float Quadcopter::mixMotor(Config* config, int motorIndex, float roll, float pitch, float yaw)
{
	if (motorIndex < 0 || motorIndex >= 4)
		return 0;

	float value = 0;
	if (config->NegativeMixing) {
		value += motorsRoll[motorIndex] * roll;
		value += motorsPitch[motorIndex] * pitch;
	}
	else {
		value += max(0, motorsRoll[motorIndex] * roll);
		value += max(0, motorsPitch[motorIndex] * pitch);
	}
	value += motorsYaw[motorIndex] * yaw;
	return value;
}
