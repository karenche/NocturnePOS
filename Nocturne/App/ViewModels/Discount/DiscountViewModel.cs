using Nocturne.BL.DTO;
using Nocturne.App.Helpers;
using Nocturne.BL.Interfaces;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Input;
using System;
using Nocturne.BL.Helpers;
using System.Collections.ObjectModel;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// Used for viewing, editing and adding discounts.
    /// </summary>
    public class DiscountViewModel: PageViewModelBase
    {
        private readonly IDiscountService _discountService;
        private readonly IProductService _productService;
        private readonly IDiscountTypeService _discountTypeService;

        #region Properties

        public override string PageName { get { return "Discount"; } }

        private readonly ObservableCollection<ProductViewModel> _products = new ObservableCollection<ProductViewModel>();
        public ObservableCollection<ProductViewModel> Products
        {
            get { return _products; }
        }

        private readonly ObservableCollection<DiscountTypeViewModel> _discountTypes = new ObservableCollection<DiscountTypeViewModel>();
        public ObservableCollection<DiscountTypeViewModel> DiscountTypes
        {
            get { return _discountTypes; }
        }

        private PageMode _pageMode;
        public PageMode PageMode
        {
            get { return _pageMode; }
            set { if (_pageMode != value) { _pageMode = value; NotifyPropertyChanged(); } }
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            private set { if (_id != value) { _id = value; NotifyPropertyChanged(); } }
        }

        private int _discountTypeId;
        public int DiscountTypeId
        {
            get { return _discountTypeId; }
            set { if (_discountTypeId != value) { _discountTypeId = value; NotifyPropertyChanged(); } }
        }

        private string _discountTypeName;
        public string DiscountTypeName
        {
            get { return _discountTypeName; }
            private set { if (_discountTypeName != value) { _discountTypeName = value;  NotifyPropertyChanged();  } }
        }

        private int _productId;
        public int ProductId
        {
            get { return _productId; }
            set { if (_productId != value) { _productId = value; NotifyPropertyChanged(); } }
        }

        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            private set { if (_productName != value) { _productName = value; NotifyPropertyChanged(); } }
        }

        private int _amountPercent;

        public int AmountPercent
        {
            get { return _amountPercent; }
            set { if (_amountPercent != value) { _amountPercent = value; NotifyPropertyChanged(); } }
        }

        #endregion Properties

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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="discountService"></param>
        /// <param name="navigation"></param>
        /// <param name="discount">If set then we're in editing or viewing mode, otherwise we're in adding mode</param>
        public DiscountViewModel(IDiscountService discountService, INavigationService navigation, DiscountDto discount = null) : base(navigation)
        {
            _discountService = discountService;
            _productService = ServiceLocator.Current.GetInstance<IProductService>();
            _discountTypeService = ServiceLocator.Current.GetInstance<IDiscountTypeService>();

            // needed for combobox
            foreach (var product in _productService.GetAllProducts())
            {
                _products.Add(new ProductViewModel(product, _productService, Navigation));
            }

            // needed for combobox
            foreach (var discountType in _discountTypeService.GetAllDiscountTypes())
            {
                _discountTypes.Add(new DiscountTypeViewModel(discountType, _discountTypeService, Navigation));
            }

            if (discount != null)
            {
                ProductId = discount.ProductId;
                ProductName = _productService.GetProductById(ProductId).Name;

                DiscountTypeId = discount.DiscountTypeId;
                DiscountTypeName = _discountTypeService.GetDiscountTypeById(DiscountTypeId).Name;

                Id = discount.Id;
                AmountPercent = discount.AmountPercent;
            }
        }

        private void Save()
        {
            var discountSaveResult = _discountService.SaveDiscount(new DiscountDto
            {
                DiscountTypeId = DiscountTypeId,
                ProductId = ProductId,
                Id = Id,
                AmountPercent = AmountPercent
            });
            SetValidationMessages(discountSaveResult.Messages);

            if (!HasValidationMessageType<ValidationErrorMessage>())
            {
                Id = discountSaveResult.Result;
                Logger.GetLogger().LogMessage("Changed discount with id=" + Id);
                Navigation.Navigate(typeof(DiscountListViewModel));
            }
        }
    }
}