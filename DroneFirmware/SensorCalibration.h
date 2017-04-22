#pragma once

#include <Arduino.h>

struct CalibrationData {
	float Min[3];
	float Max[3];
	float Average[3];
	float Length;
};

struct SensorCalibration {
	CalibrationData MagnetCalibration;
};