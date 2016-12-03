#include "BinaryHelper.h"
#include "string.h"

#ifdef  __IEEE_LITTLE_ENDIAN
	#define NEED_SWAPPING 1
#else
	#define NEED_SWAPPING 0
#endif

void BinaryHelper::changeByteOrder(uint8_t* buffer, int32_t offset, size_t length) {
#if NEED_SWAPPING
	int32_t end = offset + length;
	int32_t swapsLeft = length / 2;

	uint8_t temp;
	while (swapsLeft > 0) {
		uint8_t* a = buffer + (offset + swapsLeft - 1);
		uint8_t* b = buffer + (end - swapsLeft);

		temp = *a;
		*a = *b;
		*b = temp;
		swapsLeft--;
	}
#endif
}

void BinaryHelper::writeInt16(uint8_t* buf, int32_t offset, int16_t val) {
	memcpy(buf + offset, &val, sizeof(int16_t));
	changeByteOrder(buf, offset, sizeof(int16_t));
}

int16_t BinaryHelper::readInt16(uint8_t* buf, int32_t offset) {
	uint8_t valArr[sizeof(int16_t)];
	memcpy(valArr, buf + offset, sizeof(int16_t));
	changeByteOrder(valArr, 0,sizeof(int16_t));
	return *reinterpret_cast<int16_t*>(valArr);
}

void BinaryHelper::writeUint16(uint8_t* buf, int32_t offset, uint16_t val) {
	memcpy(buf + offset, &val, sizeof(uint16_t));
	changeByteOrder(buf, offset, sizeof(uint16_t));
}

uint16_t BinaryHelper::readUint16(uint8_t* buf, int32_t offset) {
	uint8_t valArr[sizeof(uint16_t)];
	memcpy(valArr, buf + offset, sizeof(uint16_t));
	changeByteOrder(valArr, 0, sizeof(uint16_t));
	return *reinterpret_cast<uint16_t*>(valArr);
}

void BinaryHelper::writeInt32(uint8_t* buf, int32_t offset, int32_t val) {
	memcpy(buf + offset, &val, sizeof(int32_t));
	changeByteOrder(buf, offset, sizeof(int32_t));
}

int32_t BinaryHelper::readInt32(uint8_t* buf, int32_t offset) {
	uint8_t valArr[sizeof(int32_t)];
	memcpy(valArr, buf + offset, sizeof(int32_t));
	changeByteOrder(valArr, 0, sizeof(int32_t));
	return *reinterpret_cast<int32_t*>(valArr);
}

void BinaryHelper::writeUint32(uint8_t* buf, int32_t offset, uint32_t val) {
	memcpy(buf + offset, &val, sizeof(uint32_t));
	changeByteOrder(buf, offset, sizeof(uint32_t));
}

uint32_t BinaryHelper::readUint32(uint8_t* buf, int32_t offset) {
	uint8_t valArr[sizeof(uint32_t)];
	memcpy(valArr, buf + offset, sizeof(uint32_t));
	changeByteOrder(valArr, 0, sizeof(uint32_t));
	return *reinterpret_cast<uint32_t*>(valArr);
}

void BinaryHelper::writeInt64(uint8_t* buf, int32_t offset, int64_t val) {
	memcpy(buf + offset, &val, sizeof(int64_t));
	changeByteOrder(buf, offset, sizeof(int64_t));
}


int64_t BinaryHelper::readInt64(uint8_t* buf, int32_t offset) {
	uint8_t valArr[sizeof(int64_t)];
	memcpy(valArr, buf + offset, sizeof(int64_t));
	changeByteOrder(valArr, 0, sizeof(int64_t));
	return *reinterpret_cast<int64_t*>(valArr);
}


void BinaryHelper::writeUint64(uint8_t* buf, int32_t offset, uint64_t val) {
	memcpy(buf + offset, &val, sizeof(uint64_t));
	changeByteOrder(buf, offset, sizeof(uint64_t));
}


uint64_t BinaryHelper::readUint64(uint8_t* buf, int32_t offset) {
	uint8_t valArr[sizeof(uint64_t)];
	memcpy(valArr, buf + offset, sizeof(uint64_t));
	changeByteOrder(valArr, 0, sizeof(uint64_t));
	return *reinterpret_cast<uint64_t*>(valArr);
}

void BinaryHelper::writeFloat(uint8_t* buf, int32_t offset, float val) {
	memcpy(&buf[offset], &val, sizeof(float));
}

float BinaryHelper::readFloat(uint8_t* buf, int32_t offset) {
	uint8_t valArr[sizeof(float)];
	memcpy(valArr, buf + offset, sizeof(float));
	return *reinterpret_cast<float*>(valArr);
}

void BinaryHelper::writeDouble(uint8_t* buf, int32_t offset, double val) {
	memcpy(buf + offset, &val, sizeof(double));
}

double BinaryHelper::readDouble(uint8_t* buf, int32_t offset) {
	uint8_t valArr[sizeof(double)];
	memcpy(valArr, buf + offset, sizeof(double));
	return *reinterpret_cast<double*>(valArr);
}
