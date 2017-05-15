using Nocturne.App.Helpers;
using Nocturne.BL.Helpers;
using Nocturne.BL.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// View model used in shopping that handles adding and removing products to/from client sessions.
    /// </summary>
    public class ShoppingViewModel : PageViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ISessionService _sessionService;

        #region Properties 

        public override string PageName { get { return "Shopping"; } }

        private readonly ObservableCollection<ProductViewModel> _products = new ObservableCollection<ProductViewModel>();
        public ObservableCollection<ProductViewModel> Products
        {
            get { return _products; }
        }

        private readonly ObservableCollection<ShoppingItemViewModel> _selectedProducts = new ObservableCollection<ShoppingItemViewModel>();
        public ObservableCollection<ShoppingItemViewModel> SelectedProducts
        {
            get { return _selectedProducts; }
        }

        private CardViewModel _card;
        public CardViewModel Card
        {
            get { return _card; }
            set { if (_card != value) { _card = value; NotifyPropertyChanged(); } }
        }

        public string CardName { get { return Card != null ? Card.Name : string.Empty; } }

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

        private ICommand _addProductCommand;
        public ICommand AddProductCommand
        {
            get
            {
                if (_addProductCommand == null) { _addProductCommand = new DelegateCommand<TappedRoutedEventArgs>(p => AddProduct(p)); }
                return _addProductCommand;
            }
        }

        private ICommand _removeProductCommand;
        public ICommand RemoveProductCommand
        {
            get
            {
                if (_removeProductCommand == null) { _removeProductCommand = new DelegateCommand<TappedRoutedEventArgs>(p => RemoveProduct(p)); }
                return _removeProductCommand;
            }
        }

        #endregion Commands

        public ShoppingViewModel(ProductViewModel[] products, ISessionService sessionService, INavigationService navigationService) : base(navigationService)
        {
            _sessionService = sessionService;
            _navigationService = navigationService;
            foreach (var product in products)
            {
                Products.Add(product);
            }
            PropertyChanged += ShoppingViewModel_PropertyChanged;
            SelectedProducts.CollectionChanged += SelectedProducts_CollectionChanged;
        }

        private void SelectedProducts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RemoveValidationMessages(nameof(SelectedProducts));
        }

        private void ShoppingViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Card))
            {
                NotifyPropertyChanged(nameof(CardName));
            }   
        }

        private void AddProduct(TappedRoutedEventArgs eventArgs)               //as ProductViewModel
        {
            var productViewModel = ((FrameworkElement)eventArgs.OriginalSource).DataContext as ProductViewModel;
            if (productViewModel != null)
            {
                var existItem = SelectedProducts.Where(q => q.Product.Id == productViewModel.Id).SingleOrDefault();
                if (existItem == null)
                {
                    SelectedProducts.Add(new ShoppingItemViewModel(productViewModel));
                }
                else
                {
                    existItem.Amount++;
                }
            }
        }

        private void RemoveProduct(TappedRoutedEventArgs eventArgs)
        {
            var shoppingItemViewModel = ((FrameworkElement)eventArgs.OriginalSource).DataContext as ShoppingItemViewModel;
            if (shoppingItemViewModel != null)
            {
                if (shoppingItemViewModel.Amount == 1)
                {
                    SelectedProducts.Remove(shoppingItemViewModel);
                }
                else
                {
                    shoppingItemViewModel.Amount--;
                }
            }
        }

        private void Save()
        {
            int sessionId = 0;
            if (Card != null)
            {
                sessionId = _sessionService.Find(s => s.CardId == Card.Id && !s.To.HasValue).Select(q => q.Id).SingleOrDefault();
            }

            const string emptyErrorTemplate = "{0} can not be blank.";
            ValidateProperty((msg) => { return Card == null ? new ValidationErrorMessage(msg) : null; }, string.Format(emptyErrorTemplate, "Card"), nameof(Card));
            ValidateProperty((msg) => { return Card != null && Card.Id == 0 ? new ValidationErrorMessage(msg) : null; }, "Card not found", nameof(Card));
            ValidateProperty((msg) => { return sessionId == 0 ? new ValidationErrorMessage(msg) : null; }, "Session not found", nameof(Card));
            ValidateProperty((msg) => { return SelectedProducts.Count == 0 ? new ValidationErrorMessage(msg) : null; }, "At least one product must be selected", nameof(SelectedProducts));

            if (!HasValidationMessageType<ValidationErrorMessage>())
            {
                foreach (var selectedProduct in SelectedProducts)
                {
                    _sessionService.SaveUsedProduct(new BL.DTO.UsedProductDto
                    {
                        Amount = (int)selectedProduct.Amount,
                        Date = DateTime.UtcNow,
                        ProductId = selectedProduct.Product.Id,
                        RegisteredBy = App.CurrentUserId,
                        SessionId = sessionId
                    });
                }
                Logger.GetLogger().LogMessage("Added products for session " + sessionId);
                OpenHomePageCommand.Execute(null);
            }
        }
    }
}
