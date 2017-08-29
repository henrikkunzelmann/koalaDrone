#include "WiFiCompat.h"

#if HARDWARE_ESP32
void WiFiCompat::initSettings() {
}

void WiFiCompat::setHostname(const char* hostname) {
	//WiFi.setHostname(hostname);
	//WiFi.softAPsetHostname(hostname);
}
#endif