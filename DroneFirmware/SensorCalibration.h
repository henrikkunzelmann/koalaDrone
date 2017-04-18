#pragma once

#include <Arduino.h>

struct CalibrationData {
	float Min[3];
	float Max[3];
	float Average[3];
	float Length;
} __attribute__((__packed__));

struct SensorCalibration {
	CalibrationData MagnetCalibration;
} __attribute__((__packed__));