#pragma once

#include <Arduino.h>

#include "FaultType.h"

struct Fault {
	uint16_t id;

	uint32_t timeFirst;
	uint32_t timeLast;

	FaultType type;
	char source[25];
	char function[25];
};