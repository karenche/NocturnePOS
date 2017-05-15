using Nocturne.App.Helpers;
using Nocturne.BL.DTO;
using Nocturne.BL.Interfaces;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// Used for displaying discount types.
    /// </summary>
    public class DiscountTypeViewModel : ViewModelBase
    {
        private readonly IDiscountTypeService _discountTypeService;

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
            private set { if (_name != value) { _name = value; NotifyPropertyChanged(); } }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            private set { if (_isActive != value) { _isActive = value; NotifyPropertyChanged(); } }
        }

        public DiscountTypeViewModel(DiscountTypeDto discountType, IDiscountTypeService discountTypeService, INavigationService navigation)
        {
            _discountTypeService = discountTypeService;
            Id = discountType.Id;
            Name = discountType.Name;
            IsActive = discountType.IsActive;
        }
    }
}
