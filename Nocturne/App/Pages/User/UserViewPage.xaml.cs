using Microsoft.Practices.ServiceLocation;
using Nocturne.App.Helpers;
using Nocturne.App.ViewModels;
using Nocturne.BL.Interfaces;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Nocturne.App.Pages.User
{
    public sealed partial class UserViewPage : Page
    {
        public UserViewModel CurrentViewModel { get; private set; }

        public UserViewPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            var avaliableRoles = userService.GetAvaliableRoles();

            var id = e.Parameter as int?;
            var user = id.HasValue
                ? new UserViewModel(userService.GetUser(id.Value), avaliableRoles, userService, navigationService)
                : new UserViewModel(avaliableRoles, userService, navigationService);

            DataContext = CurrentViewModel = user;
            base.OnNavigatedTo(e);
        }
    }
}
