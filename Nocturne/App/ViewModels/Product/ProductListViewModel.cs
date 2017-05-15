using Nocturne.App.Helpers;
using Nocturne.BL.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using Windows.UI.Popups;
using Microsoft.Practices.ServiceLocation;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    ///  Used for displaying a list of all products and product management.
    /// </summary>
    public class ProductListViewModel : PageViewModelBase
    {
        private readonly IProductService _productService;

        #region Properties
        public override string PageName { get { return "Products"; } }

        private readonly ObservableCollection<ProductViewModel> _productList = new ObservableCollection<ProductViewModel>();
        public ObservableCollection<ProductViewModel> ProductList
        {
            get { return _productList; }
        }

        private ProductViewModel _selectedProduct;
        public ProductViewModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { if (_selectedProduct != value) { _selectedProduct = value; NotifyPropertyChanged(); } }
        }

        #endregion Properties

        #region Commands
        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null) { _addCommand = new DelegateCommand<object>(p => Add()); }
                return _addCommand;
            }
        }

        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null) { _editCommand = new DelegateCommand<object>(p => Edit()); }
                return _editCommand;
            }
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null) { _deleteCommand = new DelegateCommand<object>(p => Delete()); }
                return _deleteCommand;
            }
        }

        #endregion Commands


        public ProductListViewModel(IProductService productService, INavigationService navigationService) : base(navigationService)
        {
            _productService = productService;

            foreach (var product in _productService.GetAllProducts())
            {
                ProductList.Add(new ProductViewModel(product, _productService, Navigation));
            }
        }


        private void Add()
        {
            Navigation.Navigate(typeof(ProductViewModel));
        }

        private void Edit()
        {
            if (SelectedProduct == null) return;

            Navigation.Navigate(typeof(ProductViewModel), SelectedProduct.Id);
        }

        private async void Delete()
        {
            if (SelectedProduct == null) return;

            var dialog = new MessageDialog("Are you sure you want to delete product " + SelectedProduct.Name + "?");
            dialog.Title = "Confirm";
            dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });

            var res = await dialog.ShowAsync();

            if ((int)res.Id == 0)
            {
                Logger.GetLogger().LogMessage("Deleted product " + SelectedProduct.Name);
                _productService.DeleteProduct(SelectedProduct.Id);
                ProductList.Remove(SelectedProduct);
            }
        }
    }
}
