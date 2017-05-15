using Nocturne.App.Helpers;

namespace Nocturne.App.ViewModels
{
    public class MainViewModel : PageViewModelBase
    {
        public override string PageName { get { return "Home"; } }

        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
