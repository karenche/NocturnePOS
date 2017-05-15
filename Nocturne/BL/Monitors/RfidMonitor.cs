using Nocturne.BL.HelperServiceReference;
using Nocturne.BL.Interfaces;
using System;
using System.ServiceModel;
using System.Threading;

namespace Nocturne.BL.Monitors
{
    public class RfidMonitor : IRfidMonitor
    {
        private const string baseAddress = "net.tcp://localhost:8523/CardHelper";
        private HelperServiceClient _client;
        private ulong? _lastResult;
        private Timer _timer;

        public event EventHandler<RfidEventArgs> Selected;

        private void TimerCallback(object state)
        {
            try
            {
                var task = _client.GetRfidCardFromReaderAsync();
                task.Wait(1000);
                var result = task.Result;
                if (result.HasValue)
                {
                    if (_lastResult == null || _lastResult != result)
                        _lastResult = result;
                    SendEvent(result.Value);
                }
                else
                {
                    _lastResult = null;
                }
            }
            catch (AggregateException ex) when (
                    ex.InnerException is EndpointNotFoundException ||
                    ex.InnerException is CommunicationObjectAbortedException ||
                    ex.InnerException is CommunicationObjectFaultedException ||
                    ex.InnerException is CommunicationException) // thrown when cardhelper is quit
            {
                StopMonitoring();
            }
        }

        private void SendEvent(ulong result)
        {
            Selected?.Invoke(this, new RfidEventArgs(result));
        }

        public void StartMonitoring()
        {
            StopMonitoring();
            var endpoint = new EndpointAddress(baseAddress);
            var binding = new NetTcpBinding(SecurityMode.None);
            _client = new HelperServiceClient(binding, endpoint);
            _timer = new Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
        }

        public void StopMonitoring()
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            };
            if (_client != null)
            {
                _client.Abort();
            }
        }
    }
}
