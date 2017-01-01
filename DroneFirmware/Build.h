#ifndef _BUILD_h
#define _BUILD_h

#include "Arduino.h"
#include <stdio.h>
#include <Esp.h>

#define BUILD_VERSION 1
#define MODEL_NAME "koalaDrone r1"
#define BUILD_NAME "build_" __DATE__ "_" __TIME__

inline void getBuildSerialCode(char* buffer, int length) {
	snprintf(buffer, length, "%X", ESP.getChipId());
}

#endif