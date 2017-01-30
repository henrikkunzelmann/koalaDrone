#pragma once

#include <Arduino.h>

#ifdef ESP8266

#define HARDWARE_NAME "ESP8266"
#define HARDWARE_ESP8266 1

#include <Esp.h>
#include <user_interface.h>

#endif