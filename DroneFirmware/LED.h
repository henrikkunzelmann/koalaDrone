#pragma once

#include <Arduino.h>

#include "Config.h"
#include "Model.h"

extern bool shouldBlink;
extern bool blinkCooldown;

extern int blinkCount;
extern uint32_t blinkCooldownTime;
extern uint32_t blinkTimer;

void setupLED();
void handleBlink();
void blinkLED(int count, uint32_t time);
void turnLedOn();
void turnLedOff();