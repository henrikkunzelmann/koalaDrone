#ifndef _FAULT_h
#define _FAULT_h

#include "FaultType.h"

struct Fault {
	uint16_t id;

	uint32_t timeFirst;
	uint32_t timeLast;

	FaultType type;
	char source[25];
	char function[25];
};

#endif