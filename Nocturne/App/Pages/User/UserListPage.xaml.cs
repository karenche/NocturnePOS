using Microsoft.Practices.ServiceLocation;
using Nocturne.App.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Nocturne.App.Pages.User
{
    public sealed partial class UserListPage : Page
    {
        public UserListViewModel CurrentViewModel { get; private set; }

        public UserListPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = CurrentViewModel = ServiceLocator.Current.GetInstance<UserListViewModel>();
            base.OnNavigatedTo(e);
        }
    }
}
