#ifndef _LED_h
#define _LED_h

#include "arduino.h"
#include "Config.h"

extern int ledPin;

extern bool shouldBlink;
extern bool blinkCooldown;

extern int blinkCount;
extern uint32_t blinkCooldownTime;
extern uint32_t blinkTimer;

void setupLED(Config* config);
void handleBlink();
void blinkLED(int count, uint32_t time);
void turnLedOn();
void turnLedOff();
#endif