#ifndef _HARDWARE_h
#define _HARDWARE_h

#include "arduino.h"

#ifdef ESP8266
#define HARDWARE_NAME "ESP8266"
#define HARDWARE_ESP8266 1

#include <Esp.h>
#include <user_interface.h>

#endif

#ifndef HARDWARE_NAME
#error "Unknown hardware type"
#endif

#ifdef ARDUINO
#define ARDUINO_VERSION ARDUINO
#else
#define ARDUINO_VERSION 0
#endif

class Hardware {
public:
	static const char* getName();

	static void halt();
	static void restart();

	static uint32_t getChipID();

	static const char* getArduinoCoreVersion();
	static const char* getSDKVersion();
 
	static uint32_t getCPUFrequency();
	static uint64_t getFlashSize();
	
	static uint64_t getFreeHeap();

	static void enableWatchDog();
	static void enableWatchDog(uint32_t time);
	static void disableWatchDog();
};

#endif