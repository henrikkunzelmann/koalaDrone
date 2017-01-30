#include "WiFiCompat.h"

#if HARDWARE_ESP8266
void WiFiCompat::initSettings() {
	WiFi.setOutputPower(20.5f);
	WiFi.setPhyMode(WIFI_PHY_MODE_11N);
	WiFi.setAutoReconnect(true);
}

void WiFiCompat::setHostname(const char* hostname) {
	WiFi.hostname(hostname);
}
#endif