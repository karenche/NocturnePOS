using System.ComponentModel;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// Used to display products in shopping views.
    /// </summary>
    public class ShoppingItemViewModel : ViewModelBase
    {
        #region Properties  

        public ProductViewModel Product { get; private set; }
        public string ProductName { get { return Product.Name; } }

        private int _amount = 1;
        public int Amount
        {
            get { return _amount; }
            set { if (_amount != value) { _amount = value; NotifyPropertyChanged(); } }
        }

        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            private set { if (_totalPrice != value) { _totalPrice = value; NotifyPropertyChanged(); } }
        }

        #endregion Properties 

        public ShoppingItemViewModel(ProductViewModel product)
        {
            Product = product;
            RecalculateTotalPrice();
            PropertyChanged += ShoppingItemViewModel_PropertyChanged;
        }

        private void ShoppingItemViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Amount))
            {
                RecalculateTotalPrice();
            }
        }

        private void RecalculateTotalPrice()
        {
            TotalPrice = Product == null ? 0 : (Product.DiscountPrice ?? Product.Price) * Amount;
        }
    }
}
