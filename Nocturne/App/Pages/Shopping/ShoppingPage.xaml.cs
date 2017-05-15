using Microsoft.Practices.ServiceLocation;
using Nocturne.App.Helpers;
using Nocturne.App.ViewModels;
using Nocturne.BL.DTO;
using Nocturne.BL.Helpers;
using Nocturne.BL.Interfaces;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Nocturne.App.Pages.Shopping
{
    public sealed partial class ShoppingPage : Page
    {
        public ShoppingViewModel CurrentViewModel { get; private set; }

        public ShoppingPage()
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
            var productService = ServiceLocator.Current.GetInstance<IProductService>();
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            var products = ServiceLocator.Current.GetInstance<IProductService>().GetAllProducts().Select(product => new ProductViewModel(product, productService, navigationService)).ToArray();
            var viewModel = new ShoppingViewModel(products, sessionService, navigationService);
            DataContext = CurrentViewModel = viewModel;
        }


        private void RfidMonitor_Selected(object sender, BL.Monitors.RfidEventArgs e)
        {
            var cardService = ServiceLocator.Current.GetInstance<ICardService>();
            var card = cardService.Find(q => q.CardType == CardTypeEnum.RfidCard && q.Uid == e.Uid).SingleOrDefault();
            var asyncAction = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { SelectCard(card); });    
        }

        private void CertificateMonitor_Selected(object sender, BL.Monitors.CertificateEventArgs e)
        {
            var displayName = string.Concat(e.IdCode, ";", e.FirstName, ";", e.LastName);
            var cardService = ServiceLocator.Current.GetInstance<ICardService>();
            var card = cardService.Find(q => q.CardType == CardTypeEnum.IdCard && q.DisplayName == displayName).SingleOrDefault();
            var asyncAction = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { SelectCard(card); });
        }

        private void SelectCard(CardDto card)
        {
            CurrentViewModel.Card = card == null ? null : new CardViewModel(card);
        }
    }
}
