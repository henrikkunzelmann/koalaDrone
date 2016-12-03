#include "FaultManager.h"

boolean FaultManager::inited = false;
uint16_t FaultManager::lastID = 1;
Fault FaultManager::faults[FAULTS_SIZE];

const char* FaultManager::getFaultName(FaultType type) {
	switch (type) {
	case FaultInternal:
		return "Internal";
	case FaultException:
		return "Exception";
	case FaultBug:
		return "Bug";
	case FaultProtocol:
		return "Protocol";
	case FaultInvalidSensorData:
		return "InvalidSensorData";
	case FaultHardware:
		return "Hardware";

	default:
		return "Unknown";
	}
}

void FaultManager::init() {
	if (inited)
		return;

	memset(faults, 0, sizeof(faults));
	inited = true;
}

uint16_t FaultManager::findFault(FaultType type, const char* source, const char* function) {
	uint16_t faultIndex = UINT16_MAX;
	uint16_t highestID = 0;
	
	for (int i = 0; i < FAULTS_SIZE; i++) {
		Fault fault = faults[i];

		if (fault.type == type
			&& strncmp(fault.source, source, sizeof(fault.source)) == 0
			&& strncmp(fault.function, function, sizeof(fault.function)) == 0
			&& fault.id > highestID) {
			faultIndex = i;
			highestID = fault.id;
		}
	}

	return faultIndex;
}

boolean FaultManager::addFault(Fault fault) {
	for (int i = 0; i < FAULTS_SIZE; i++) {
		if (faults[i].id == 0) {
			faults[i] = fault;
			return true;
		}
	}
	return false;
}

void FaultManager::createNewFault(FaultType type, const char* source, const char* function) {
	Fault fault;
	fault.id = lastID++;
	fault.type = type;
	strncpy(fault.source, source, sizeof(fault.source));
	strncpy(fault.function, function, sizeof(fault.function));

	uint32_t now = millis();
	fault.timeFirst = now;
	fault.timeLast = now;

	Log::error("Fault", "[%u] [%s] %s at %s", fault.id, getFaultName(type), source, function);

	if (!addFault(fault)) {
		// Fault mit kleinster ID löschen
		uint16_t lowestID = UINT16_MAX;
		int index = -1;
		
		for (int i = 0; i < FAULTS_SIZE; i++) {
			Fault f = faults[i];
			if (f.id < lowestID) {
				lowestID = f.id;
				index = i;
			}
		}

		if (index >= 0) {
			faults[index].id = 0;
			Log::info("Fault", "Removed fault with id %u", lowestID);

			// erneut versuchen
			if (!addFault(fault))
				Log::error("Fault", "Could not add fault!");
		}
		else
			Log::error("Fault", "Could not remove old fault");
	}

	yield();
}

void FaultManager::fault(FaultType type, const char* source, const char* function) {
	init();

	uint16_t index = findFault(type, source, function);

	if (index != UINT16_MAX) {
		Fault* fault = &faults[index];

		// gespeicherter Fault ist zu alt
		uint32_t now = millis();
		if (fault->timeLast < now && now - fault->timeLast > NEW_FAULT_TIME)
		{
			Log::error("Fault", "New fault for old fault... (first time: %ums, last time: %ums)", fault->timeFirst, fault->timeLast);
			return createNewFault(type, source, function);
		}

		fault->timeLast = now;
		return;
	}

	createNewFault(type, source, function);
}