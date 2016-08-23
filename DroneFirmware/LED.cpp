#include "LED.h"

int ledPin = 0;

bool shouldBlink = false;
bool blinkCooldown = false;

int32_t blinkCount = 0;
uint32_t blinkCooldownTime = 0;
uint32_t blinkTimer = 0;

void setupLED(Config* config) {
	ledPin = config->PinLed;
	pinMode(ledPin, OUTPUT);
}

void handleBlink() {
	if (shouldBlink) {
		if (millis() - blinkTimer > 250) {
			turnLedOff();

			shouldBlink = false;
			blinkCooldown = true;
			blinkTimer = millis();
		}
	}
	else {
		if (millis() - blinkTimer > blinkCooldownTime) {
			blinkCooldown = false;
			if (--blinkCount > 0)
				blinkLED(blinkCount, blinkCooldownTime);
		}
	}
}

void blinkLED(int count, uint32_t time) {
	if (time < blinkCooldownTime) {
		blinkCooldownTime = time;
		blinkCount = count;
	}

	if (blinkCooldown || shouldBlink)
		return;

	turnLedOn();

	shouldBlink = true;
	blinkCount = count;
	blinkCooldownTime = time;
	blinkTimer = millis();
}

void turnLedOn() {
	shouldBlink = false;
	blinkCooldown = false;

	digitalWrite(ledPin, HIGH);
}

void turnLedOff() {
	shouldBlink = false;
	blinkCooldown = false;

	digitalWrite(ledPin, LOW);
}


