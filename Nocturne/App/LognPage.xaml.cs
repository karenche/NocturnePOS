using Microsoft.Practices.ServiceLocation;
using Nocturne.App.Helpers;
using Nocturne.App.ViewModels;
using Nocturne.BL.DTO;
using Nocturne.BL.Helpers;
using Nocturne.BL.Interfaces;
using System;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Nocturne.App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LognPage : Page
    {
        public LognPage()
        {
            this.InitializeComponent();
        }

        private void btnLogn_Click(object sender, RoutedEventArgs e)
        {
            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            var user = userService.GetUser(tbUsername.Text, pbPasword.Password);
            Login(user);
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
            base.OnNavigatedTo(e);
        }

        private void RfidMonitor_Selected(object sender, BL.Monitors.RfidEventArgs e)
        {
            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            var cardService = ServiceLocator.Current.GetInstance<ICardService>();
            var card = cardService.Find(c => c.Uid == e.Uid).SingleOrDefault();
            if (card != null)
            {
                var userId = cardService.GetUserIdAssotiatedWithCard(card.Id);
                if (userId.HasValue)
                {
                    var user = userService.GetUser(userId.Value);
                    var asyncAction = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { Login(user); });
                }
            }
        }

        private void CertificateMonitor_Selected(object sender, BL.Monitors.CertificateEventArgs e)
        {
            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            var user = userService.GetUserByRegCode(e.IdCode);
            var asyncAction = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { Login(user); });
        }

        private void Login(UserDto user)
        {
            if (user != null)
            {
                App.CurrentUserId = user.Id;
                App.IsAdministrator = user.UserRoles.Contains(UserDto.Administrator);
                App.IsWorker = user.UserRoles.Contains(UserDto.Worker);

                var navigation = ServiceLocator.Current.GetInstance<INavigationService>();
                navigation.Navigate(typeof(MainViewModel));
            }
            else
            {
                lbError.Visibility = Visibility.Visible;
            }
        }
    }
}
