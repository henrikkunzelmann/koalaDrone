#pragma once

#include <Arduino.h>

#include "Hardware.h"
#include "Model.h"
#include "MemoryAdapter.h"
#include "Config.h"
#include "Log.h"
#include "FlashMemoryAdapter.h"
#include "PacketBuffer.h"
#include "Profiler.h"

#define CONFIG_MAGIC_VALUE 124
#define CONFIG_VERSION 4

class ConfigManager
{
 public:
	 static Config loadConfig();
	 static Config loadConfig(MemoryAdapter* memory);

	 static void saveConfig(const Config config);
	 static void saveConfig(MemoryAdapter* memory, const Config config);

	 static Config getDefault();
};