using Nocturne.App.Helpers;
using Nocturne.BL.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using Windows.UI.Popups;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// Used in displaying a list of all current discounts and provides discount management.
    /// </summary>
    public class DiscountListViewModel : PageViewModelBase
    {
        private readonly IDiscountService _discountService;

        #region Properties
        public override string PageName { get { return "Discounts"; } }
        
        private readonly ObservableCollection<DiscountViewModel> _discountList = new ObservableCollection<DiscountViewModel>();
        public ObservableCollection<DiscountViewModel> DiscountList
        {
            get { return _discountList; }
        }

        private DiscountViewModel _selectedDiscount;
        public DiscountViewModel SelectedDiscount
        {
            get { return _selectedDiscount; }
            set { if (_selectedDiscount != value) { _selectedDiscount = value; NotifyPropertyChanged(); } }
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

        public DiscountListViewModel(IDiscountService discountService, INavigationService navigationService) : base(navigationService)
        {
            _discountService = discountService;

            foreach (var discount in _discountService.GetAllDiscounts())
            {
                DiscountList.Add(new DiscountViewModel(_discountService, Navigation, discount));
            }
        }

        private void Add()
        {
            Navigation.Navigate(typeof(DiscountViewModel));
        }

        private void Edit()
        {
            if (SelectedDiscount == null) return;

            Navigation.Navigate(typeof(DiscountViewModel), SelectedDiscount.Id);
        }

        private async void Delete()
        {
            if (SelectedDiscount == null) return;

            var dialog = new MessageDialog("Are you sure you want to delete discount " + SelectedDiscount.DiscountTypeName + " on product " + SelectedDiscount.ProductName + "?");
            dialog.Title = "Confirm";
            dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });

            var res = await dialog.ShowAsync();

            if ((int)res.Id == 0)
            {
                Logger.GetLogger().LogMessage("Deleted discount " + SelectedDiscount.DiscountTypeName + " on product " + SelectedDiscount.ProductName);
                _discountService.DeleteDiscount(SelectedDiscount.Id);
                DiscountList.Remove(SelectedDiscount);
            }
        }
    }
}