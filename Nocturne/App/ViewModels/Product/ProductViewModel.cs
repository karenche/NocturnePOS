using System;
using Microsoft.Practices.ServiceLocation;
using Nocturne.App.Helpers;
using Nocturne.BL.DTO;
using Nocturne.BL.Interfaces;
using System.Windows.Input;
using Nocturne.BL.Helpers;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// Used to view, edit or add a product.
    /// </summary>
    public class ProductViewModel : PageViewModelBase
    {
        private readonly IProductService _productService;

        public override string PageName { get { return "Product"; } }

        private int _id;
        public int Id
        {
            get { return _id; }
            private set { if (_id != value) { _id = value; NotifyPropertyChanged(); } }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { if (_name != value) { _name = value; NotifyPropertyChanged(); } }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { if (_description != value) { _description = value; NotifyPropertyChanged(); } }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { if (_price != value) { _price = value; NotifyPropertyChanged(); } }
        }

        private decimal? _discountPrice;

        public decimal? DiscountPrice
        {
            get { return _discountPrice; }
            set { if (_discountPrice != value) { _discountPrice = value; NotifyPropertyChanged(); } }
        }

        #region Commands

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null) { _saveCommand = new DelegateCommand(p => Save()); }
                return _saveCommand;
            }
        }

        #endregion Commands

        public ProductViewModel(ProductDto product, IProductService productService, INavigationService navigation) : base(navigation)
        {
            _productService = productService;
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            DiscountPrice = ServiceLocator.Current.GetInstance<IDiscountService>().CalculateDiscountPriceForProduct(product);
        }

        public ProductViewModel(IProductService productService, INavigationService navigationService) : base(navigationService)
        {
            this._productService = productService;
        }

        private void Save()
        {
            var productSaveResult = _productService.SaveProduct(new ProductDto
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Price = Price
            });
            SetValidationMessages(productSaveResult.Messages);

            if (!HasValidationMessageType<ValidationErrorMessage>())
            {
                Id = productSaveResult.Result;
                Logger.GetLogger().LogMessage("Changed product " + Name);
                Navigation.Navigate(typeof(ProductListViewModel));
            }
        }
    }
}
