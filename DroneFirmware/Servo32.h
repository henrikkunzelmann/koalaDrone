#pragma once

#include <Arduino.h>
#include "Hardware.h"

#if HARDWARE_ESP32

#define SERVO_FREQUENCY 50 // hz
#define SERVO_REFRESH_INTERVAL (1000 / SERVO_FREQUENCY)

#define SERVO_DUTY_PRECISION 16 // bits

class Servo 
{
private:
	uint8_t channel;
	int pin;

	bool isAttached;
	uint32_t microseconds;

public:
	Servo();
	uint8_t attach(int pin); 
	void detach();
	void writeMicroseconds(int value); 
	int readMicroseconds();            
	bool attached(); 
};

#endif