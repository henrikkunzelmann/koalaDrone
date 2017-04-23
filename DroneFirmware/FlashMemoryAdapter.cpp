#include "FlashMemoryAdapter.h"

#ifdef HARDWARE_ESP8266
FlashMemoryAdapter::FlashMemoryAdapter(size_t size, uint16_t offset) {
	this->size = size;
	this->offset = offset;
}

bool FlashMemoryAdapter::assertAddress(uint32_t address, size_t length) {
	if (address >= size) {
		Log::error("FlashMemory", "Invalid value for address");
		return false;
	}
	if (address + length > size) {
		Log::error("FlashMemory", "Invalid value for length");
		return false;
	}
	return true;
}

bool FlashMemoryAdapter::begin() {
	EEPROM.begin(size);
	return true;
}

bool FlashMemoryAdapter::end() {
	EEPROM.end();
	return true;
}

void FlashMemoryAdapter::writeByte(uint32_t address, uint8_t val) {
	if (assertAddress(address, sizeof(uint8_t)))
		EEPROM.write(offset + address, val);
}

uint8_t FlashMemoryAdapter::readByte(uint32_t address) {
	if (assertAddress(address, sizeof(uint8_t)))
		return EEPROM.read(offset + address); 
	return 0;
}

void FlashMemoryAdapter::read(uint32_t address, uint8_t* data, size_t length) {
	if (assertAddress(address, length))
		memcpy(data, EEPROM.getDataPtr() + offset + address, length);
	else
		memset(data, 0, length); // Daten mit 0 befüllen, damit der Leser bei Fehlern keine zufällige Werte bekommt
}

void FlashMemoryAdapter::write(uint32_t address, uint8_t* data, size_t length) {
	if (assertAddress(address, length))
		memcpy(EEPROM.getDataPtr() + offset + address, data, length);
}
#endif