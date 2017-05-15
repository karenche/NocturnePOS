using Nocturne.BL.HelperServiceReference;
using Nocturne.BL.Interfaces;
using System;
using System.ServiceModel;
using System.Threading;

namespace Nocturne.BL.Monitors
{
    public class CertificateMonitor : ICertificateMonitor
    {
        private const string baseAddress = "net.tcp://localhost:8523/CardHelper";
        private HelperServiceClient _client;
        private IdCardData _lastResult;
        private Timer _timer;

        public event EventHandler<CertificateEventArgs> Selected;

        private void TimerCallback(object state)
        {
            try
            {
                var task = _client.GetIdCardFromReaderAsync();
                task.Wait(1000);
                var result = task.Result;
                if (result != null)
                {
                    if (_lastResult == null || _lastResult.IdCode != result.IdCode)
                        _lastResult = result;
                    SendEvent(result);
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

        private void SendEvent(IdCardData result)
        {
            Selected?.Invoke(this, new CertificateEventArgs(result.IdCode, result.FirstName, result.LastName));
        }

        public void StartMonitoring()
        {
            StopMonitoring();
            var endpoint = new EndpointAddress(baseAddress);
            var binding = new NetTcpBinding(SecurityMode.None);
            _client = new HelperServiceClient(binding, endpoint);
            _timer = new Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
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
