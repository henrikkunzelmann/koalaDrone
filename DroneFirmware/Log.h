#ifndef _LOG_h
#define _LOG_h

#include "arduino.h"
#include <stdarg.h>

enum LogLevel {
	Error, // Nachrichten die als Fehler eingestuft werden, können Sicherheit von Drohne gefährden
	Info,  // Nachrichten die nicht als Error oder Debug eingestuft werden
	Debug  // Nachrichten die für Programmierer interessant sein können
};

#define LOG_BUFFER_LINES 128
#define LOG_TAG_BUFFER_SIZE 30
#define LOG_FORMAT_BUFFER_SIZE 200

class Log {
private:
	static bool printToSerial;
	static uint32_t bufferLines;
	static char** _buffer;

	static char tagBuffer[];
	static char formatBuffer[];

	static void addMessage(char* str);

	static const char* getLevelString(LogLevel level);

	static void print_P(LogLevel level, const __FlashStringHelper* tag, const __FlashStringHelper* format, va_list args);
	static void print(LogLevel level, const char* tag, const char* format, va_list args);
public:
	static void emptyLine();

	static void error_P(const __FlashStringHelper* tag, const __FlashStringHelper* format, ...);
	static void info_P(const __FlashStringHelper* tag, const __FlashStringHelper* format, ...);
	static void debug_P(const __FlashStringHelper* tag, const __FlashStringHelper* format, ...);

	static void error(const char* tag, const char* format, ...);
	static void info(const char* tag, const char* format, ...);
	static void debug(const char* tag, const char* format, ...);

	static uint32_t getBufferLines();
	static char** getBuffer();
	static void clearBuffer();
	static char* popMessage();

	static void setPrintToSerial(bool value);
};


#endif

