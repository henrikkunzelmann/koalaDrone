#pragma once

#include "Hardware.h"

#include <Arduino.h>
#include <MemoryI2C.h>

#include "MemoryAdapter.h"
#include "Model.h"
#include "Log.h"

class EEPROMMemoryAdapter : public MemoryAdapter {
protected:
	MemoryI2C memory;

	bool assertAddress(uint32_t address, size_t length);
	bool assertError(uint8_t error);

public:
	EEPROMMemoryAdapter();

	bool begin() override;
	bool end() override;
	void writeByte(uint32_t address, uint8_t val) override;
	void write(uint32_t address, uint8_t* data, size_t length) override;
	uint8_t readByte(uint32_t address) override;
	void read(uint32_t address, uint8_t* data, size_t length) override;
};
