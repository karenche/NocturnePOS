using Microsoft.Practices.ServiceLocation;
using Nocturne.App.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Nocturne.App.Pages.Client
{
    public sealed partial class ClientListPage : Page
    {
        public ClientInfoListViewModel CurrentViewModel { get; private set; }

        public ClientListPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = CurrentViewModel = ServiceLocator.Current.GetInstance<ClientInfoListViewModel>();
            base.OnNavigatedTo(e);
        }
    }
}
