#include "Hardware.h"

#if HARDWARE_ESP8266
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
	return ESP.getChipId();
}

const char* Hardware::getArduinoCoreVersion() {
	return "Unknown";
}

const char* Hardware::getSDKVersion() {
	return ESP.getSdkVersion();
}

uint32_t Hardware::getCPUFrequency() {
	return ESP.getCpuFreqMHz();
}

uint64_t Hardware::getFlashSize() {
	return ESP.getFlashChipRealSize();
}

uint64_t Hardware::getFreeHeap() {
	return ESP.getFreeHeap();
}

uint64_t Hardware::getFreeSketchSpace() {
	return ESP.getFreeSketchSpace();
}

void Hardware::enableWatchDog() {
	ESP.wdtEnable(WDTO_0MS);
}

void Hardware::enableWatchDog(uint32_t time) {
	ESP.wdtEnable(time);
}

void Hardware::disableWatchDog() {
	ESP.wdtDisable();
}

bool Hardware::isNormalBoot() {
	uint8_t reason = Hardware::getResetInfo().reason;
	return reason == REASON_DEFAULT_RST || reason == REASON_EXT_SYS_RST;
}

ResetInfo Hardware::getResetInfo() {
	ResetInfo info;

	rst_info* espInfo = ESP.getResetInfoPtr();
	info.reason = espInfo->reason;
	info.exception = espInfo->exccause;
	info.epc = espInfo->epc1;
	info.excvaddr = espInfo->exccause;
	return info;
}
#endif