#pragma once

#include <Arduino.h>

enum StopReason : uint8_t {
	Unknown,
	None, 
	User,
	NoData,
	NoPing,
	InvalidGyro,
	WifiDisconnect
};