using Microsoft.Practices.ServiceLocation;
using Nocturne.App.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Nocturne.App.Pages.Client
{
    public sealed partial class ActiveClientListPage : Page
    {
        public ActiveClientListViewModel CurrentViewModel { get; private set; }

        public ActiveClientListPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = CurrentViewModel = ServiceLocator.Current.GetInstance<ActiveClientListViewModel>();
            base.OnNavigatedTo(e);
        }
    }
}
