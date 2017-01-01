#ifndef _NETWORKMANAGER_h
#define _NETWORKMANAGER_h

#include "arduino.h"
#include <user_interface.h>
#include <ESP8266WiFi.h>
#include <WiFiUdp.h>

#include "Config.h"
#include "Gyro.h"
#include "Baro.h"
#include "ServoManager.h"
#include "DroneEngine.h"
#include "PacketBuffer.h"
#include "PacketType.h"
#include "Log.h"
#include "Build.h"
#include "ConfigManager.h"
#include "LED.h"
#include "CycleTimes.h"
#include "FaultManager.h"
#include "VoltageInputReader.h"


#define VERBOSE_PACKET_LOG false

class NetworkManager
{
protected:
	SensorHAL* sensor;
	ServoManager* servos;
	DroneEngine* engine;
	Config* config;
	VoltageInputReader* voltageReader;

	IPAddress _dataFeedSubscriptor;
	bool _dataFeedSubscribed;
	long _lastDataSend;
	long _lastLogSend;
	long _lastDebugDataSend;

	bool saveConfig;
	int lastConfigSave;

	int dataRevision;

	int lastMovementRevision;
	int lastOtaRevision;

	WiFiUDP helloUDP;
	WiFiUDP controlUDP;
	WiFiUDP dataUDP;

	PacketBuffer* readBuffer;
	PacketBuffer* writeBuffer;

	bool checkRevision(int a, int b);

	void checkSaveConfig();

	void handlePackets(uint16_t num);

	bool beginParse(WiFiUDP udp);
	void handleHello(WiFiUDP udp);
	void handleControl(WiFiUDP udp);
	void handleData(WiFiUDP upd);

	void writeHeader(WiFiUDP udp, int32_t revision, ControlPacketType packetType);
	void writeDataHeader(WiFiUDP udp, int32_t revision, DataPacketType packetType);

	void sendPacket(WiFiUDP udp);
	void sendAck(WiFiUDP udp, int32_t revision);
	void sendData(WiFiUDP udp);
	void echoPacket(WiFiUDP udp);

	void sendDroneData(WiFiUDP udp);
	void sendLog(WiFiUDP udp);
	void sendDebugData(WiFiUDP udp);
public:
	explicit NetworkManager(SensorHAL* sensor, ServoManager* servos, DroneEngine* engine, Config* config, VoltageInputReader* voltageReader);
	
	void beginSaveConfig();

	void handlePackets();
	void handleData();
};

#endif

