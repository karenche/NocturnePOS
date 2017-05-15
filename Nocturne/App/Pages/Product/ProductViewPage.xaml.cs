using Microsoft.Practices.ServiceLocation;
using Nocturne.App.Helpers;
using Nocturne.App.ViewModels;
using Nocturne.BL.Interfaces;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Nocturne.App.Pages.Product
{
    public sealed partial class ProductViewPage : Page
    {
        public ProductViewModel CurrentViewModel { get; private set; }

        public ProductViewPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var productService = ServiceLocator.Current.GetInstance<IProductService>();
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();

            int? id = e.Parameter as int?;

            DataContext = CurrentViewModel = id.HasValue ?
                new ProductViewModel(productService.GetProductById(id.Value), productService, navigationService) :
                new ProductViewModel(productService, navigationService);

            base.OnNavigatedTo(e);
        }

    }
}
