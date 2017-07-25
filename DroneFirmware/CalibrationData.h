#pragma once

#include <Arduino.h>

#include "Log.h"

struct CalibrationData {
	uint64_t count;
	float min[3];
	float max[3];
	float sum[3];
	float average[3];
	float length;
};

void clearCalibrationData(CalibrationData* data);
void updateCalibrationData(CalibrationData* data, float a, float b, float c, boolean averageByBounds);
void logCalibration(CalibrationData* data);