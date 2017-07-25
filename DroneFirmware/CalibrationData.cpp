#include "CalibrationData.h"

void clearCalibrationData(CalibrationData* data)
{
	data->count = 0;
	for (int i = 0; i < 3; i++) {
		data->min[i] = MAXFLOAT;
		data->max[i] = -MAXFLOAT;
		data->sum[i] = 0;
		data->average[i] = 0;
	}
	data->length = 0;
}

void updateCalibrationData(CalibrationData* data, float a, float b, float c, boolean averageByBounds)
{
	data->count++;

	data->min[0] = _min(data->min[0], a);
	data->min[1] = _min(data->min[1], b);
	data->min[2] = _min(data->min[2], c);

	data->max[0] = _max(data->max[0], a);
	data->max[1] = _max(data->max[1], b);
	data->max[2] = _max(data->max[2], c);

	if (averageByBounds) {
		for (int i = 0; i < 3; i++)
			data->average[i] = (data->min[i] + data->max[i]) * 0.5f;
	}
	else {
		data->sum[0] += a;
		data->sum[1] += b;
		data->sum[2] += c;

		for (int i = 0; i < 3; i++)
			data->average[i] = data->sum[i] / data->count;
	}

	float ax = data->average[0];
	float ay = data->average[1];
	float az = data->average[2];
	data->length = sqrtf(ax*ax + ay*ay + az*az);
	yield();
}

void logCalibration(CalibrationData* data) {
	Log::debug("Gyro", "min: %f, %f, %f", data->min[0], data->min[1], data->min[2]);
	Log::debug("Gyro", "max: %f, %f, %f", data->max[0], data->max[1], data->max[2]);
	Log::debug("Gyro", "average: %f, %f, %f", data->average[0], data->average[1], data->average[2]);
	Log::debug("Gyro", "length: %f", data->length);
	Log::emptyLine();
}