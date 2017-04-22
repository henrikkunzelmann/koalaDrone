#include "Timer.h"

Timer::Timer(uint32_t interval)
{
	this->interval = interval;
	this->last = 0;
	this->jitter = 0;
}

uint32_t Timer::getJitter()
{
	return jitter;
}

bool Timer::shouldTick()
{
	uint32_t time = millis();
	uint32_t diff = time - last;

	if (diff >= interval) {
		last = time;

		// if time went not backwards (because millis() overflowed)
		if (time >= last) 
			jitter = diff - interval; // calculate the jitter
		return true;
	}
	return false;
}
