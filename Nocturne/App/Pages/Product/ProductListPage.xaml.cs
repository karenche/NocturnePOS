using Microsoft.Practices.ServiceLocation;
using Nocturne.App.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Nocturne.App.Pages.Product
{
    public sealed partial class ProductListPage : Page
    {
        public ProductListViewModel CurrentViewModel { get; private set; }

        public ProductListPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = CurrentViewModel = ServiceLocator.Current.GetInstance<ProductListViewModel>();
            base.OnNavigatedTo(e);
        }

        private void gridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridView grid = sender as GridView;
            CurrentViewModel.SelectedProduct = grid.SelectedItem as ProductViewModel;

        }
    }
}
