#include "LED.h"

bool shouldBlink = false;
bool blinkCooldown = false;

int32_t blinkCount = 0;
uint32_t blinkCooldownTime = 0;
uint32_t blinkTimer = 0;

void setupLED() {
	pinMode(PIN_LED0, OUTPUT);
	turnLedOff();
}

void handleBlink() {
	if (shouldBlink) {
		if (millis() - blinkTimer > 300) {
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

	digitalWrite(PIN_LED0, HIGH);
}

void turnLedOff() {
	shouldBlink = false;
	blinkCooldown = false;

	digitalWrite(PIN_LED0, LOW);
}


