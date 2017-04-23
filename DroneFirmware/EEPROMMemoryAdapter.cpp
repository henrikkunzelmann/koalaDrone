#include "EEPROMMemoryAdapter.h"

EEPROMMemoryAdapter::EEPROMMemoryAdapter() {
}

bool EEPROMMemoryAdapter::assertAddress(uint32_t address, size_t length) {
	if (address >= MEMORY_I2C_SIZE) {
		Log::error("EEPROMMemory", "Invalid value for address");
		return false;
	}
	if (address + length > MEMORY_I2C_SIZE) {
		Log::error("EEPROMMemory", "Invalid value for length");
		return false;
	}
	return true;
}

bool EEPROMMemoryAdapter::assertError(uint8_t error) {
	if (error == 0)
		return true;
	Log::error("EEPROMMemory", "I2C error: %d", error);
	return false;
}

bool EEPROMMemoryAdapter::begin() {
	if (!memory.begin(MEMORY_I2C_SIZE, MEMORY_I2C_PAGE_SIZE)) {
		Log::error("EEPROMMemory", "Could not connect to chip on I2C");
		return false;
	}
	return true;
}

bool EEPROMMemoryAdapter::end() {
	return true;
}

void EEPROMMemoryAdapter::writeByte(uint32_t address, uint8_t val) {
	if (assertAddress(address, sizeof(uint8_t)))
		assertError(memory.writeByte(address, val));
}

uint8_t EEPROMMemoryAdapter::readByte(uint32_t address) {
	if (assertAddress(address, sizeof(uint8_t)))
		return memory.readByte(address);
	return 0;
}

void EEPROMMemoryAdapter::read(uint32_t address, uint8_t* data, size_t length) {
	if (assertAddress(address, length))
		assertError(memory.readBytes(address, data, length));
	else
		memset(data, 0, length); // Daten mit 0 befüllen, damit der Leser bei Fehlern keine zufällige Werte bekommt
}

void EEPROMMemoryAdapter::write(uint32_t address, uint8_t* data, size_t length) {
	if (assertAddress(address, length))
		assertError(memory.writeBytes(address, data, length));
}