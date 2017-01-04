#include "Hardware.h"

const char* Hardware::getName() {
	return HARDWARE_NAME;
}

void Hardware::halt() {
	while (1);
}

void Hardware::restart() {
#if HARDWARE_ESP8266
	ESP.restart();
#endif
}

uint32_t Hardware::getChipID() {
#if HARDWARE_ESP8266
	return ESP.getChipId();
#endif
}

const char* Hardware::getArduinoCoreVersion() {
	return "unknown";
}

const char* Hardware::getSDKVersion() {
#if HARDWARE_ESP8266
	return ESP.getSdkVersion();
#endif
}

uint32_t Hardware::getCPUFrequency() {
#if HARDWARE_ESP8266
	return ESP.getCpuFreqMHz();
#endif
}

uint64_t Hardware::getFlashSize() {
#if HARDWARE_ESP8266
	return ESP.getFlashChipRealSize();
#endif
}

uint64_t Hardware::getFreeHeap() {
#if HARDWARE_ESP8266
	return ESP.getFreeHeap();
#endif
}

void Hardware::enableWatchDog() {
#if HARDWARE_ESP8266
	ESP.wdtEnable(WDTO_0MS);
#endif
}

void Hardware::enableWatchDog(uint32_t time) {
#if HARDWARE_ESP8266
	ESP.wdtEnable(time);
#endif
}

void Hardware::disableWatchDog() {
#if HARDWARE_ESP8266
	ESP.wdtDisable();
#endif
}
