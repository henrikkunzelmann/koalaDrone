#pragma once

#define MODEL_ONE 1
#define MODEL_NAME "koalaDrone r1"

#define PIN_FRONT_LEFT 12
#define PIN_FRONT_RIGHT 13
#define PIN_BACK_LEFT 16
#define PIN_BACK_RIGHT 14

#define PIN_LED0 0
#define PIN_BATTERY A0

#define BATTERY_MAX_VALUE (1023.0f)
#define BATTERY_MAX_VOLTAGE (16.0f)

#define DEFAULT_SERVO_MIN 1000
#define DEFAULT_SERVO_MAX 2000
#define DEFAULT_SERVO_IDLE 1100

#define MEMORY_I2C_ENABLE false
#define MEMORY_I2C_SIZE 0
#define MEMORY_I2C_PAGE_SIZE 0

#define SWAP_GYRO_XY false
#define NEGATE_GYRO_X false
#define NEGATE_GYRO_Y false
#define NEGATE_GYRO_Z false