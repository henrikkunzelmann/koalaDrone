using System;

namespace DroneControl.Input
{
    public interface IInputDevice : IDisposable, IEquatable<IInputDevice>
    {
        /// <summary>
        /// Gibt zurück, ob das Eingabegerät noch verbunden ist.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Gibt den Namen des Eingabegeräts zurück.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gibt die Informationen über die Batterie des Eingabegeräts zurück.
        /// </summary>
        BatteryInfo Battery { get; }

        /// <summary>
        /// Gibt zurück, ob das Eingabegerät kalibriert werden kann.
        /// </summary>
        bool CanCalibrate { get; }

        bool HasError { get; }

        void Calibrate();
        void Update(InputManager manager);
    }
}
