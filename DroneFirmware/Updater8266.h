#pragma once

#include <Arduino.h>
#include "Hardware.h"

#if HARDWARE_ESP8266
// on ESP8266 we use the Updater library
#include <Updater.h>

#define HARDWARE_SUPPORTS_OTA 1
#endif