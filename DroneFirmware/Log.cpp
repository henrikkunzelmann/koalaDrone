#include "Log.h"

uint32_t Log::bufferLines = 0;
char** Log::_buffer;
char Log::tagBuffer[LOG_TAG_BUFFER_SIZE];
char Log::formatBuffer[LOG_FORMAT_BUFFER_SIZE];
bool Log::printToSerial = true;

const char* Log::getLevelString(LogLevel level) {
	switch (level) {
	case Error:
		return "[Error]";
	case Info:
		return " [Info]"; // extra Leerzeichen damit alle Strings gleich lang sind
	case Debug:
		return "[Debug]";
	}
	return     "[Unkwn]";
}

uint32_t Log::getBufferLines() {
	return bufferLines;
}

char** Log::getBuffer() {
	if (_buffer == NULL)
		_buffer = (char**)malloc(LOG_BUFFER_LINES * sizeof(char*));
	return _buffer;
}

void Log::clearBuffer() {
	char** buffer = getBuffer();
	for (uint32_t i = 0; i < bufferLines; i++)
	{
		free(buffer[i]);
		buffer[i] = NULL;
	}
	bufferLines = 0;
}

char* Log::popMessage() {
	if (bufferLines == 0)
		return NULL;

	char** buffer = getBuffer();
	char* msg = buffer[0];

	for (uint32_t i = 1; i < bufferLines; i++)
		buffer[i - 1] = buffer[i];

	bufferLines--;

	return msg;
}

void Log::addMessage(char* message) {
	char** buffer = getBuffer();

	// alte Log Einträge verschieben
	if (bufferLines >= LOG_BUFFER_LINES) {
		free(buffer[0]);
		for (int i = 1; i < LOG_BUFFER_LINES; i++)
			buffer[i - 1] = buffer[i];
		bufferLines = LOG_BUFFER_LINES - 1;
	}

	buffer[bufferLines++] = message;
}

void Log::emptyLine() {
	char* message = (char*)malloc(1);
	if (message != NULL) {
		message[0] = '\0';
		addMessage(message);
	}

	Serial.println();
}

void Log::print_P(LogLevel level, const __FlashStringHelper* tag, const __FlashStringHelper* format, va_list args) {
	memset(tagBuffer, 0, sizeof(tagBuffer));
	memset(formatBuffer, 0, sizeof(formatBuffer));

	strncpy_P(tagBuffer, (PGM_P)tag, sizeof(tagBuffer));
	strncpy_P(formatBuffer, (PGM_P)format, sizeof(formatBuffer));

	print(level, tagBuffer, formatBuffer, args);
}

void Log::print(LogLevel level, const char* tag, const char* format, va_list args) {
	size_t messageSize = 128 * sizeof(char);
	char* message = (char*)malloc(messageSize);

	if (message == NULL) {
		Serial.println(F("[Log::print()] malloc() failed."));
		return;
	}

	memset(message, 0, messageSize);

	int size = snprintf(message, messageSize, "$ [%8lus] %s [%s]", millis() / 1000, getLevelString(level), tag);
	if (size < 0) {
		Serial.println(F("[Log::print()] Could not log message, because message is too long"));
		free(message);
		return;
	}

	// Padding
	size_t startLength = strlen(message);
	size_t tagLength = strlen(tag);

	if (tagLength < 14) {
		size_t padding = 14 - tagLength;
		for (size_t i = 0; i < padding; i++)
			message[startLength + i] = ' ';
		message[startLength + padding] = '\0';
	}

	size = vsnprintf(message + strlen(message), messageSize, format, args);
	if (size < 0) {
		Serial.println(F("[Log::print()] Could not log message, because message is too long"));
		free(message);
		return;
	}


	if (printToSerial)
		Serial.println(message);

	yield();

	addMessage(message);
}

void Log::error_P(const __FlashStringHelper* tag, const __FlashStringHelper* format, ...) {
	va_list args;
	va_start(args, format);
	print_P(Error, tag, format, args);
	va_end(args);
}

void Log::info_P(const __FlashStringHelper* tag, const __FlashStringHelper* format, ...) {
	va_list args;
	va_start(args, format);
	print_P(Info, tag, format, args);
	va_end(args);
}

void Log::debug_P(const __FlashStringHelper* tag, const __FlashStringHelper* format, ...) {
	va_list args;
	va_start(args, format);
	print_P(Debug, tag, format, args);
	va_end(args);
}

void Log::error(const char* tag, const char* format, ...) {
	va_list args;
	va_start(args, format);
	print(Error, tag, format, args);
	va_end(args);
}

void Log::info(const char* tag, const char* format, ...) {
	va_list args;
	va_start(args, format);
	print(Info, tag, format, args);
	va_end(args);
}

void Log::debug(const char* tag, const char* format, ...) {
	va_list args;
	va_start(args, format);
	print(Debug, tag, format, args);
	va_end(args);
}

void Log::setPrintToSerial(bool value) {
	if (value != Log::printToSerial && !value) 
		Log::info("Log", "VerboseSerialLog set to false");

	Log::printToSerial = value;
}