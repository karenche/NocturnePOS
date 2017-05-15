using Nocturne.BL.Monitors;
using System;

namespace Nocturne.BL.Interfaces
{
    /// <summary>
    /// Service to manage RFID card events.
    /// </summary>
    public interface IRfidMonitor
    {
        event EventHandler<RfidEventArgs> Selected;

        void StartMonitoring();
        void StopMonitoring();
    }
}