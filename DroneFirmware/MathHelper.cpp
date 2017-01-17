#include "MathHelper.h"

float MathHelper::clampValue(float value, float min, float max) {
	if(value <= min)
		return min;
	if(value >= max)
		return max;
	return value;
}

float MathHelper::angleDifference(float a, float b) {
	return (float)fmod(fmod(a - b, 360) + 540, 360) - 180;
}

float MathHelper::toDegress(float radians) {
	return radians * 180 / PI;
}

float MathHelper::toRadians(float degress) {
	return degress * PI / 180;
}

float MathHelper::fixValue(float value, float begin, float end) {
	float range = end - begin;
	while (value < begin)
		value += range;
	while (value >= end)
		value -= range;
	return value;
}