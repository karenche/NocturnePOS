using Microsoft.Practices.ServiceLocation;
using Nocturne.App.Helpers;
using Nocturne.App.ViewModels;
using Nocturne.BL.Interfaces;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Nocturne.App.Pages.Discount
{
    public sealed partial class DiscountViewPage: Page
    {
        public DiscountViewModel CurrentViewModel { get; private set; }

        public DiscountViewPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var discountService = ServiceLocator.Current.GetInstance<IDiscountService>();
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();

            int? id = e.Parameter as int?;

            DataContext = CurrentViewModel = new DiscountViewModel(discountService, navigationService,
                    id.HasValue ? discountService.GetDiscountById(id.Value) : null);
            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// For some reason we can't set initial values for comboboxes with simple twoway binding,
        /// hence this method is needed for initialization.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var selectedDiscountTypeValue = CurrentViewModel.DiscountTypeId;

            if (selectedDiscountTypeValue != 0) { // DiscountViewPage is used for adding and editing, adding = no ID values
                cbDiscountType.SelectedIndex = 0;
                CurrentViewModel.DiscountTypeId = selectedDiscountTypeValue;
            }

            var selectedProductValue = CurrentViewModel.ProductId;

            if (selectedProductValue != 0) { // DiscountViewPage is used for adding and editing, adding = no ID values
                cbProduct.SelectedIndex = 0;
                CurrentViewModel.ProductId = selectedProductValue;
            }
        }
    }
}
