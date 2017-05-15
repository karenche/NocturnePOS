using Nocturne.App.Helpers;
using System.Windows.Input;
using System;
using Microsoft.Practices.ServiceLocation;

namespace Nocturne.App.ViewModels
{
    public abstract class PageViewModelBase : ViewModelBase, IPageViewModel
    {
        private readonly INavigationService _navigationService;

        public INavigationService Navigation
        {
            get { return _navigationService; }
        }

        public bool IsAdministrator { get { return App.IsAdministrator; } }
        public bool IsWorker { get { return App.IsWorker; } }

        public abstract string PageName { get; }

        #region Commands

        private ICommand _openHomePageCommand;
        public ICommand OpenHomePageCommand
        {
            get
            {
                if (_openHomePageCommand == null) { _openHomePageCommand = new DelegateCommand(p => OpenPage(typeof(MainViewModel))); }
                return _openHomePageCommand;
            }
        }

        private ICommand _openProductListPageCommand;
        public ICommand OpenProductListPageCommand
        {
            get
            {
                if (_openProductListPageCommand == null) { _openProductListPageCommand = new DelegateCommand(p => OpenPage(typeof(ProductListViewModel))); }
                return _openProductListPageCommand;
            }
        }

        private ICommand _openDiscountListPageCommand;
        public ICommand OpenDiscountListPageCommand
        {
            get
            {
                if (_openDiscountListPageCommand == null) { _openDiscountListPageCommand = new DelegateCommand(p => OpenPage(typeof(DiscountListViewModel))); }
                return _openDiscountListPageCommand;
            }
        }

        private ICommand _openUserListPageCommand;
        public ICommand OpenUserListPageCommand
        {
            get
            {
                if (_openUserListPageCommand == null) { _openUserListPageCommand = new DelegateCommand(p => OpenPage(typeof(UserListViewModel))); }
                return _openUserListPageCommand;
            }
        }

        private ICommand _openSessionStartPageCommand;
        public ICommand OpenSessionStartPageCommand
        {
            get
            {
                if (_openSessionStartPageCommand == null) { _openSessionStartPageCommand = new DelegateCommand(p => OpenPage(typeof(SessionStartViewModel))); }
                return _openSessionStartPageCommand;
            }
        }

        private ICommand _openShoppingPageCommand;
        public ICommand OpenShoppingPageCommand
        {
            get
            {
                if (_openShoppingPageCommand == null) { _openShoppingPageCommand = new DelegateCommand(p => OpenPage(typeof(ShoppingViewModel))); }
                return _openShoppingPageCommand;
            }
        }

        private ICommand _openSessionStopPageCommand;
        public ICommand OpenSessionStopPageCommand
        {
            get
            {
                if (_openSessionStopPageCommand == null) { _openSessionStopPageCommand = new DelegateCommand(p => OpenPage(typeof(SessionStopViewModel))); }
                return _openSessionStopPageCommand;
            }
        }

        private ICommand _openClientListPageCommand;
        public ICommand OpenClientListPageCommand
        {
            get
            {
                if (_openClientListPageCommand == null) { _openClientListPageCommand = new DelegateCommand(p => OpenPage(typeof(ClientInfoListViewModel))); }
                return _openClientListPageCommand;
            }
        }

        private ICommand _openActiveClientListPageCommand;
        public ICommand OpenActiveClientListPageCommand
        {
            get
            {
                if (_openActiveClientListPageCommand == null) { _openActiveClientListPageCommand = new DelegateCommand(p => OpenPage(typeof(ActiveClientListViewModel))); }
                return _openActiveClientListPageCommand;
            }
        }

        private ICommand _logoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                if (_logoutCommand == null) { _logoutCommand = new DelegateCommand(p => Logout()); }
                return _logoutCommand;
            }
        }

        #endregion Commands

        protected PageViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        protected void OpenPage(Type modelType)
        {
            _navigationService.Navigate(modelType);
        }    

        protected void Logout()
        {
            App.CurrentUserId = -1;
            App.IsAdministrator = false;
            App.IsWorker = false;
            OpenPage(typeof(LognPage));
        }
    }
}
