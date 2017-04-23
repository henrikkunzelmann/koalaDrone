#pragma once

#include <Arduino.h>
#include <ctime>

// Henrik Kunzelmann 2017

struct NmeaData {
	boolean valid;
	tm time;
	float latitude;
	float longitude;
	float altitude;
	float speed;
};

// Implements an NMEA protocol parser for reading GPS data
class NMEA
{
private:
	NmeaData lastData;

	char buffer[300];

	uint8_t oneDigit(char a);
	uint8_t twoDigitDec(char a, char b);
	uint8_t twoDigitHex(char a, char b);

	boolean fillBuffer(const char* sentence);
	boolean parseBuffer();

	char* firstValue();
	char* nextValue();

	boolean skipValue();

	boolean parseInt(int* value);
	boolean parseFloat(float* value);
	boolean parseChar(char* value);

	boolean parseTimeStamp(tm* value);
	boolean parseDateStamp(tm* value);
	boolean parseCoordinate(float* value);

public:
	explicit NMEA();

	boolean parse(const char* sentence);

	boolean isValid();
	time_t getTime();
	float getLatitude();
	float getLongitude();
	float getAltitude();
	float getSpeed();
};