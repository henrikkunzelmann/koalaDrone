#ifndef _NETWORKMANAGER_h
#define _NETWORKMANAGER_h

#include <Arduino.h>

#include "Hardware.h"
#include "WiFi.h"
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
#include "VoltageReader.h"
#include "Updater.h"


#define VERBOSE_PACKET_LOG false

class NetworkManager
{
protected:
	SensorHAL* sensor;
	ServoManager* servos;
	DroneEngine* engine;
	Config* config;
	VoltageReader* voltageReader;

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

	WiFiUDP* helloUDP;
	WiFiUDP* controlUDP;
	WiFiUDP* dataUDP;

	PacketBuffer* readBuffer;
	PacketBuffer* writeBuffer;

	bool checkRevision(int a, int b);

	void checkSaveConfig();

	void handlePackets(uint16_t num);

	bool beginParse(WiFiUDP* udp);
	void handleHello(WiFiUDP* udp);
	void handleControl(WiFiUDP* udp);
	void handleData(WiFiUDP* udp);

	void writeHeader(WiFiUDP* udp, int32_t revision, ControlPacketType packetType);
	void writeDataHeader(WiFiUDP* udp, int32_t revision, DataPacketType packetType);

	void sendPacket(WiFiUDP* udp);
	void sendPacket(WiFiUDP* udp, IPAddress remote, uint16_t remotePort);

	void sendAck(WiFiUDP* udp, int32_t revision);
	void sendData(WiFiUDP* udp);
	void echoPacket(WiFiUDP* udp);

	void sendDroneData(WiFiUDP* udp);
	void sendLog(WiFiUDP* udp);
	void sendDebugData(WiFiUDP* udp);
public:
	explicit NetworkManager(SensorHAL* sensor, ServoManager* servos, DroneEngine* engine, Config* config, VoltageReader* voltageReader);
	
	void beginSaveConfig();

	void handlePackets();
	void handleData();
};

#endif

