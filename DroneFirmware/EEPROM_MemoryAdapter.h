#ifndef _EEPROM_MEMORYADAPTER_h
#define _EEPROM_MEMORYADAPTER_h

#include "arduino.h"
#include <EEPROM.h>

#include "MemoryAdapter.h"
#include "Log.h"

class EEPROM_MemoryAdapter : public MemoryAdapter {
protected:
	size_t size;
	uint16_t offset;

	bool assertAddress(uint32_t address, size_t length);

public:
	EEPROM_MemoryAdapter(size_t size, uint16_t offset);

	bool begin() override;
	bool end() override;
	void writeByte(uint32_t address, uint8_t val) override;
	void write(uint32_t address, uint8_t* data, size_t length) override;
	uint8_t readByte(uint32_t address) override;
	void read(uint32_t address, uint8_t* data, size_t length) override;

};

#endif

