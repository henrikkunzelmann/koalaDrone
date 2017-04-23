#include "NMEA.h"

NMEA::NMEA()
{
	memset(&lastData, 0, sizeof(lastData));
}

boolean NMEA::parse(const char* sentence)
{
	if (!fillBuffer(sentence))
		return false;
	return parseBuffer();
}

boolean NMEA::isValid()
{
	return lastData.valid;
}

time_t NMEA::getTime()
{
	tm time = lastData.time;
	return mktime(&time);
}

float NMEA::getLatitude()
{
	return lastData.latitude;
}

float NMEA::getLongitude()
{
	return lastData.longitude;
}

float NMEA::getAltitude()
{
	return lastData.altitude;
}

float NMEA::getSpeed()
{
	// Speed is in knots... convert to km/h
	return lastData.speed * 1.852;
}

uint8_t NMEA::oneDigit(char a)
{
	if (a >= '0' && a <= '9')
		return (a - '0');
	if (a >= 'a' && a <= 'f')
		return (a - 'a') + 10;
	if (a >= 'A' && a <= 'F')
		return (a - 'A') + 10;
	return 0;
}

uint8_t NMEA::twoDigitDec(char a, char b)
{
	return oneDigit(a) * 10 + oneDigit(b);
}

uint8_t NMEA::twoDigitHex(char a, char b)
{
	return oneDigit(a) * 16 + oneDigit(b);
}

boolean NMEA::fillBuffer(const char* sentence)
{
	if (sentence == NULL)
		return false;
	if (strlen(sentence) >= sizeof(buffer))
		return false;

	int length = strlen(sentence);
	int start = -1;
	int end = -1;

	for (int i = 0; i < length; i++) {
		if (sentence[i] == '$') {
			if (start != -1) // parsed already a $ char
				return false;
			start = i + 1;
		}
		else if (sentence[i] == '*') {
			if (end != -1) // parsed already a * char
				return false;
			end = i;
		}
	}

	// $ is always before *, otherwise indicate error
	if (start >= end)
		return false;

	// Read checksum after message
	int checksumLength = (length - end - 1);

	if (checksumLength > 0) {
		// Checksum should be two digits
		if (checksumLength != 2)
			return false;

		// Last two digits are the checksum in hex
		uint8_t messageChecksum = twoDigitHex(sentence[end + 1], sentence[end + 2]);
		uint8_t calculatedChecksum = 0;

		// Calculate the checksum of the message to compare it
		for (int i = start; i < end; i++)
			calculatedChecksum ^= sentence[i];

		// Checksum mismatch
		if (messageChecksum != calculatedChecksum)
			return false;
	}

	int msgLength = end - start;

	// Copy to internal buffer
	memset(buffer, 0, sizeof(buffer));
	memcpy(buffer, sentence + start, msgLength);

}

char* NMEA::firstValue()
{
	return strtok(buffer, ",");
}

char* NMEA::nextValue()
{
	return strtok(NULL, ",");
}

boolean NMEA::skipValue()
{
	return nextValue() != NULL;
}

boolean NMEA::parseInt(int* value)
{
	char* str = nextValue();
	if (str == NULL)
		return false;

	if (sscanf(str, "%d", value) == 0)
		return false;
	return true;
}

boolean NMEA::parseFloat(float* value)
{
	char* str = nextValue();
	if (str == NULL)
		return false;

	if (sscanf(str, "%f", value) == 0)
		return false;
	return true;
}

boolean NMEA::parseChar(char* value)
{
	char* str = nextValue();
	if (str == NULL)
		return false;

	if (strlen(str) != 1)
		return false;
	*value = str[0];
	return true;
}

boolean NMEA::parseTimeStamp(tm* tm)
{
	char* str = nextValue();
	if (str == NULL)
		return false;

	if (strlen(str) < 6)
		return false;

	tm->tm_hour = twoDigitDec(str[0], str[1]);
	tm->tm_min = twoDigitDec(str[2], str[3]);
	tm->tm_sec = twoDigitDec(str[4], str[5]);
	return true;
}

boolean NMEA::parseDateStamp(tm* tm)
{
	char* str = nextValue();
	if (str == NULL)
		return false;

	if (strlen(str) < 6)
		return false;

	tm->tm_mday = twoDigitDec(str[0], str[1]);
	tm->tm_mon = twoDigitDec(str[2], str[3]) - 1;

	// Calculate years since 1900
	int year = twoDigitDec(str[4], str[5]);
	if (year < 60)
		year = 100 + year;

	tm->tm_year = year;
	tm->tm_isdst = -1;
	return true;
}

boolean NMEA::parseCoordinate(float* value)
{
	float v;
	if (!parseFloat(&v))
		return false;

	char dir;
	if (!parseChar(&dir))
		return false;

	int sign = 0;

	if (dir == 'n' || dir == 'e' || dir == 'N' || dir == 'E')
		sign = 1;
	else if (dir == 's' || dir == 'w' || dir == 'S' || dir == 'W')
		sign = -1;
	else
		return false;

	// Convert to decimal format
	v = fabs(v);
	float degress = ((int)v) / 100;
	float minutes = fmod(v, 100);

	*value = (degress + minutes / 60) * sign;
	return true; 
}

boolean NMEA::parseBuffer()
{
	char* type = firstValue();
	if (type == NULL)
		return false;

	NmeaData data = lastData;
	if (strcmp(type, "GPRMC") == 0) {
		char validStatus;
		if (!parseTimeStamp(&data.time))
			return false;
		if (!parseChar(&validStatus))
			return false;
		if (!parseCoordinate(&data.latitude))
			return false;
		if (!parseCoordinate(&data.longitude))
			return false;
		if (!parseFloat(&data.speed))
			return false;
		if (!skipValue()) // direction of movement
			return false;
		if (!parseDateStamp(&data.time))
			return false;
		// Other values ignored

		data.valid = validStatus == 'A';
	}
	else if (strcmp(type, "GPGGA") == 0) {
		int quality;
		if (!parseTimeStamp(&data.time))
			return false;
		if (!parseCoordinate(&data.latitude))
			return false;
		if (!parseCoordinate(&data.longitude))
			return false;
		if (!parseInt(&quality))
			return false;
		if (!skipValue()) // numbers of satellites
			return false;
		if (!skipValue()) // HDOP
			return false;
		if (!parseFloat(&data.altitude))
			return false;
		// Other values ignored

		data.valid = quality > 0;
	}
	else {
		// Unknown message type
		// Return true, because the message is probably valid, but it is not supported
		return true;
	}

	// Data correctly parsed
	lastData = data;
	return true;
}

