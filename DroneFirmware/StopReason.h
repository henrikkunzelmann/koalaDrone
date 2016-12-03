#ifndef _STOPREASON_h
#define _STOPREASON_h

enum StopReason : uint8_t {
	Unknown,
	None, 
	User,
	NoData,
	NoPing,
	InvalidGyro,
	WifiDisconnect
};

#endif