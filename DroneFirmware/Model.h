#pragma once

#include "Hardware.h"

#if HARDWARE_ESP8266
	#include "ModelOne.h"
#elif HARDWARE_ESP32
	#include "ModelTwo.h"
#else
	#error "Unknown model"
#endif