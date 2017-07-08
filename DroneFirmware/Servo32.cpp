#include "Servo32.h"

#if HARDWARE_ESP32

Servo::Servo() {
	static uint8_t servoChannel = 0;

	this->channel = servoChannel++;
	ledcSetup(this->channel, SERVO_FREQUENCY, SERVO_DUTY_PRECISION);
}

uint8_t Servo::attach(int pin) {
	pinMode(pin, OUTPUT);
	digitalWrite(pin, LOW);

	ledcAttachPin(pin, channel);

	this->pin = pin;
	this->isAttached = true;
	return channel;
}

void Servo::detach() {
	ledcDetachPin(pin);
	digitalWrite(pin, LOW);
	isAttached = false;
}

void Servo::writeMicroseconds(int value) {
	microseconds = value;

	if (isAttached) {
		// convert from value in microseconds to the duty cycle
		float v = (float)microseconds / ((float)SERVO_REFRESH_INTERVAL * 1000);
		const uint32_t fullDuty = (1 << SERVO_DUTY_PRECISION) - 1;

		ledcWrite(channel, (uint32_t)roundf(v * fullDuty));
	}
}

int Servo::readMicroseconds() {
	return microseconds;
}

bool Servo::attached() {
	return isAttached;
}

#endif