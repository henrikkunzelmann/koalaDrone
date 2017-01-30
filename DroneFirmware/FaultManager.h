#ifndef _FAULTMANAGER_h
#define _FAULTMANAGER_h

#include <Arduino.h>
#include "Log.h"

#include "Fault.h"
#include "FaultType.h"

#define FAULTS_SIZE 20
#define NEW_FAULT_TIME 10000 // ms

class FaultManager
{
private:
	static bool inited;
	static uint16_t lastID;
	static Fault faults[FAULTS_SIZE];

	static const char* getFaultName(FaultType type);

	static void init();
	static uint16_t findFault(FaultType type, const char* source, const char* function);
	static bool addFault(Fault fault);
	static void createNewFault(FaultType type, const char* source, const char* function);

public:
	static void fault(FaultType type, const char* source, const char* function);
	
};


#endif