using Microsoft.Practices.ServiceLocation;
using Nocturne.App.Helpers;
using Nocturne.App.ViewModels;
using Nocturne.BL.Interfaces;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Nocturne.App.Pages.Session
{
    public sealed partial class SessionStartPage : Page
    {
        public SessionStartViewModel CurrentViewModel { get; private set; }

        public SessionStartPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            var certificateMonitor = ServiceLocator.Current.GetInstance<ICertificateMonitor>();
            certificateMonitor.StopMonitoring();
            certificateMonitor.Selected -= CertificateMonitor_Selected;
            var rfidMonitor = ServiceLocator.Current.GetInstance<IRfidMonitor>();
            rfidMonitor.StopMonitoring();
            rfidMonitor.Selected -= RfidMonitor_Selected;
            base.OnNavigatingFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var certificateMonitor = ServiceLocator.Current.GetInstance<ICertificateMonitor>();
            certificateMonitor.StartMonitoring();
            certificateMonitor.Selected += CertificateMonitor_Selected;
            var rfidMonitor = ServiceLocator.Current.GetInstance<IRfidMonitor>();
            rfidMonitor.StartMonitoring();
            rfidMonitor.Selected += RfidMonitor_Selected;

            var sessionService = ServiceLocator.Current.GetInstance<ISessionService>();
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            var cardService = ServiceLocator.Current.GetInstance<ICardService>();
            var customerService = ServiceLocator.Current.GetInstance<IClientService>();
            var viewModel = new SessionStartViewModel(sessionService, cardService, customerService, navigationService);
            DataContext = CurrentViewModel = viewModel;
            base.OnNavigatedTo(e);
        }

        private void RfidMonitor_Selected(object sender, BL.Monitors.RfidEventArgs e)
        {    
            var asyncAction = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => CurrentViewModel.CardSelected(e.Uid));  
        }

        private void CertificateMonitor_Selected(object sender, BL.Monitors.CertificateEventArgs e)
        {
            var asyncAction = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => CurrentViewModel.CardSelected(e.IdCode, e.FirstName, e.LastName));
        } 
    }
}
