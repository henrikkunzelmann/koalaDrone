#include "Hardware.h"

#if HARDWARE_ESP32
const char* Hardware::getName() {
	return HARDWARE_NAME;
}

void Hardware::halt() {
	while (1) yield();
}

void Hardware::restart() {
	ESP.restart();
}

uint32_t Hardware::getChipID() {
	static uint32_t chipID = UINT32_MAX;

	if (chipID == UINT32_MAX) {
		uint8_t mac[6];
		if (esp_efuse_read_mac(mac) == ESP_OK)
			chipID = mac[2] << 24 | mac[3] << 16 | mac[4] << 8 | mac[5];
		else
			chipID = 0x12345678u;
	}
	return chipID;
}

const char* Hardware::getArduinoCoreVersion() {
	return "Unknown";
}

const char* Hardware::getSDKVersion() {
	return ESP.getSdkVersion();
}

uint32_t Hardware::getCPUFrequency() {
	return F_CPU / (1000 * 1000);
}

uint64_t Hardware::getFlashSize() {
	return 0;
}

uint64_t Hardware::getFreeSketchSpace() {
	return 0;
}

uint64_t Hardware::getFreeHeap() {
	return ESP.getFreeHeap();
}

void Hardware::enableWatchDog() {
}

void Hardware::enableWatchDog(uint32_t time) {
}

void Hardware::disableWatchDog() {
}

boolean Hardware::isNormalBoot() {
	return true;
}

ResetInfo Hardware::getResetInfo() {
	ResetInfo info;
	memset(&info, 0, sizeof(info));
	return info;
}
#endif