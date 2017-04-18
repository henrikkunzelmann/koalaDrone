#pragma once

#include <Arduino.h>
#include <stdio.h>

#include "Hardware.h"

#define BUILD_VERSION 1
#define BUILD_NAME "build_" __DATE__ "_" __TIME__

inline void getBuildSerialCode(char* buffer, int length) {
	snprintf(buffer, length, "%X", Hardware::getChipID());
}