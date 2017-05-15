using Nocturne.BL.Monitors;
using System;

namespace Nocturne.BL.Interfaces
{
    /// <summary>
    /// Service to manage ID-card events.
    /// </summary>
    public interface ICertificateMonitor
    {
        event EventHandler<CertificateEventArgs> Selected;

        void StartMonitoring();
        void StopMonitoring();
    }
}