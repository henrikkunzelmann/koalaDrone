#pragma once

#include <Arduino.h>

enum HelloPacketType : uint8_t {
	HelloQuestion = 1,
	HelloAnswer = 2
};

enum ControlPacketType : uint8_t {
	MovementPacket = 1,
	StopPacket = 2,
	ArmPacket = 3,
	BlinkPacket = 4,
	RawSetPacket = 5,

	AckPacket = 6,
	PingPacket = 7,
	ResetRevisionPacket = 8,

	GetInfoPacket = 9,
	SubscribeDataFeed = 10,
	UnsubscribeDataFeed = 11,

	CalibrateGyro = 12,
	Reset = 13,

	SetConfig = 14,
	ClearStatus = 15,

	BeginOTA = 16,
	DataOTA = 17,
	EndOTA = 18
};

enum DataPacketType : uint8_t {
	DataDrone = 1,
	DataLog = 2,
	DataDebugOutput = 3,
	DataDebugProfiler = 4
};

const char* getHelloPacketName(HelloPacketType type);
const char* getControlPacketName(ControlPacketType type);
const char* getDataPacketName(DataPacketType type);