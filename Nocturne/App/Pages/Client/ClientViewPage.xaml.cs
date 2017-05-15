using Microsoft.Practices.ServiceLocation;
using Nocturne.App.Helpers;
using Nocturne.App.ViewModels;
using Nocturne.BL.Interfaces;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Nocturne.App.Pages.Client
{
    public sealed partial class ClientViewPage : Page
    {
        public ClientInfoViewModel CurrentViewModel { get; private set; }

        public ClientViewPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var clientService = ServiceLocator.Current.GetInstance<IClientService>();
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();

            var id = e.Parameter as int?;
            var user = id.HasValue
                ? new ClientInfoViewModel(clientService.GetClient(id.Value), clientService, navigationService)
                : new ClientInfoViewModel(clientService, navigationService);

            DataContext = CurrentViewModel = user;
            base.OnNavigatedTo(e);
        }
    }
}
