using Microsoft.Practices.ServiceLocation;
using Nocturne.App.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Nocturne.App.Pages.Discount
{
    public sealed partial class DiscountListPage : Page
    {
        public DiscountListViewModel CurrentViewModel { get; private set; }

        public DiscountListPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = CurrentViewModel = ServiceLocator.Current.GetInstance<DiscountListViewModel>();
            base.OnNavigatedTo(e);
        }

        private void gridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridView grid = sender as GridView;
            CurrentViewModel.SelectedDiscount = grid.SelectedItem as DiscountViewModel;
        }
    }
}
