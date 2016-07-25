#ifndef _SENSORCALIBRATION_h
#define _SENSORCALIBRATION_h


struct CalibrationData {
	float Min[3];
	float Max[3];
	float Average[3];
};

struct SensorCalibration {
	CalibrationData MagnetData;
	float MagneticFieldStrength;
};

#endif