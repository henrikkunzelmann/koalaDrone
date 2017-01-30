#pragma once

#include <Arduino.h>

#include "Hardware8266.h"
#include "Hardware32.h"
#include "ResetInfo.h"

#ifndef HARDWARE_NAME
#error "Unknown hardware type"
#endif

#ifdef ARDUINO
#define ARDUINO_VERSION ARDUINO
#else
#define ARDUINO_VERSION 0
#endif

// Implements common methods to query informations about the hardware and interface with it.
class Hardware {
public:
	// Returns the name of the hardware the firmware is currently running.
	static const char* getName();

	// Halts the CPU.
	static void halt();

	// Restarts the hardware.
	static void restart();

	// Returns an unique chip id.
	static uint32_t getChipID();

	// Returns the arduino core version of the hardware.
	static const char* getArduinoCoreVersion();

	// Returns the SDK version of the hardware if a SDK is used.
	static const char* getSDKVersion();
 
	// Returns the frequency of the cpu in mhz.
	static uint32_t getCPUFrequency();

	// Returns the flash size of the chip or 0 if the flash size is unknown.
	static uint64_t getFlashSize();

	// Returns the remaining flash space on the flash size.
	static uint64_t getFreeSketchSpace();
	
	// Returns the free heap of the cpu or 0 if the free heap is unknown.
	static uint64_t getFreeHeap();

	// Enables the Watchdog if there is any.
	static void enableWatchDog();

	// Enables the Watchdog if there is any with the specified time.
	static void enableWatchDog(uint32_t time);

	// Disables the Watchdog.
	static void disableWatchDog();

	// Returns true when the hardware was booted normal and no exception or reset occured.
	static boolean isNormalBoot();

	// Returns the reset info.
	static ResetInfo getResetInfo();
};