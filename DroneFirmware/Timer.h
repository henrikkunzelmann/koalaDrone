#pragma once

#include <Arduino.h>

// Provides functions to run code in specified intervals.
class Timer 
{
private:
	uint32_t interval;
	uint32_t last;
	uint32_t jitter;

public:
	// Initializes the timer with the specified interval in milliseconds.
	explicit Timer(uint32_t interval);

	// Returns the jitter (extra time betweens ticks) in milliseconds. Smaller jitter means more accurate ticking behaviour.
	uint32_t getJitter();

	// Returns true when the timer has elapsed and some action should be performed.
	bool shouldTick();
};