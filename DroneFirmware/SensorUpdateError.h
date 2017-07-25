#pragma once

#include <Arduino.h>

enum SensorUpdateError {
	UpdateOK,
	UpdateNotChanged,
	UpdateError
};