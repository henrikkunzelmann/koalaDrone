#include "SoftwareIMU.h"
#include "SensorHAL.h"

SoftwareIMU::SoftwareIMU(SensorHAL* hal, Config* config) : SensorIMU(hal, config)
{
}

const char* SoftwareIMU::getName() const
{
	return "SoftwareIMU";
}

const char* SoftwareIMU::getShortName() const
{
	return getName();
}

boolean SoftwareIMU::isHardwareBased() const
{
	return false;
}

boolean SoftwareIMU::init()
{
	this->roll = 0;
	this->pitch = 0;
	this->yaw = 0;
	this->firstAccSample = true;
	return true;
}

boolean SoftwareIMU::disable()
{
	return true;
}

bool SoftwareIMU::getValues(IMUValues* imuValues)
{
	const float dt = CYCLE_GYRO / 1000.f;

	Gyro* gyro = hal->getGyro();
	if (!gyro->isDataOK())
		return false;

	Profiler::begin("SoftwareIMU::getValues()");
	GyroValues values = gyro->getValues();

	if (gyro->isGyroXRotating())
		roll += values.GyroX * dt;
	if (gyro->isGyroYRotating())
		pitch += values.GyroY * dt;
	if (gyro->isGyroZRotating())
		yaw += values.GyroZ * dt;

	if (config->EnableImuAcc && !gyro->isAccMoving()) {
		float accRoll = -MathHelper::toDegress(atan(values.AccY / sqrtf(values.AccX*values.AccX + values.AccZ*values.AccZ)));
		float accPitch = MathHelper::toDegress(atan(values.AccX / sqrtf(values.AccY*values.AccY + values.AccZ*values.AccZ)));

		if (firstAccSample) {
			roll = accRoll;
			pitch = accPitch;
			firstAccSample = false;
		}
		else {
			roll = FILTER(roll, accRoll, 0.05f);
			pitch = FILTER(pitch, accPitch, 0.05f);
		}
	}
	if (config->EnableImuMag && gyro->canUseMagneticData()) {
		float magRoll = MathHelper::toDegress(atan(values.MagnetY / sqrtf(values.MagnetX*values.MagnetX + values.MagnetZ*values.MagnetZ)));
		float magPitch = -MathHelper::toDegress(atan(values.MagnetX / sqrtf(values.MagnetY*values.MagnetY + values.MagnetZ*values.MagnetZ)));

		float r = MathHelper::toRadians(roll);
		float p = MathHelper::toRadians(pitch);
		float xa = values.MagnetX*cos(p) + values.MagnetZ*sin(p);
		float ya = values.MagnetX*sin(r)*sin(p) + values.MagnetY*cos(r) - values.MagnetZ*sin(r)*cos(p);

		float magYaw = -MathHelper::toDegress(atan2(ya, xa));

		roll = FILTER(roll, magRoll, 0.01f);
		pitch = FILTER(pitch, magPitch, 0.01f);
		yaw = FILTER(yaw, magYaw, 0.025f);
	}

	imuValues->roll = MathHelper::fixValue(roll, -180, 180);
	imuValues->pitch = MathHelper::fixValue(pitch, -180, 180);
	imuValues->yaw = MathHelper::fixValue(yaw, 0, 360);

	Profiler::end();
	return true;
}
