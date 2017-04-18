#pragma once

#include <Arduino.h>

#include "Config.h"

#define FILTER(x, y, w) ((1.0f - w) * x + w * y)

class MathHelper
{
 public:
	 static float clampValue(float value, float min, float max);
	 static float fixValue(float value, float begin, float end);

	 static float toDegress(float radians);
	 static float toRadians(float degress);

	 static float angleDifference(float a, float b);
};