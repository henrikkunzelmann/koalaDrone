#pragma once

#include <Arduino.h>
#include "Hardware.h"

#if HARDWARE_ESP8266
// on ESP8266 we use the servo library
#include <Servo.h>

#define SERVO_FREQUENCY (1000 / REFRESH_INTERVAL)
#define SERVO_REFRESH_INTERVAL REFRESH_INTERVAL
#endif