#pragma once

#include <Arduino.h>

#include "Hardware.h"
#include "WiFi.h"

class WiFiCompat {
public:
	// Sets the best settings for the WiFi.
	static void initSettings();

	// Sets the hostname of WiFi adapter.
	static void setHostname(const char* hostname);
};