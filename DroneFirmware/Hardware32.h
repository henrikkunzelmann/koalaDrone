#pragma once

#include <Arduino.h>

#ifdef ESP32
#define HARDWARE_NAME "ESP32"
#define HARDWARE_ESP32 1

#include <Esp.h>
#include <esp_system.h>
#endif