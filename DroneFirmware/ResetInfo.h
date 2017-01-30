#pragma once

#include <Arduino.h>

struct ResetInfo {
	// The reason for the reset. The value depends on the hardware used.
	uint8_t reason;

	// The exception when an exception caused the reset. The value depends on the hardware used.
	uint8_t exception;

	// The program counter the reset was caused on.
	uint32_t epc;

	// The address the exception was caused by.
	uint32_t excvaddr;
};