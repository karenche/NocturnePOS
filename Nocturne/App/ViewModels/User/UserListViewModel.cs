using Nocturne.BL.Interfaces;
using Nocturne.App.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// Used to display a list of users and searching.
    /// </summary>
    public class UserListViewModel : PageViewModelBase
    {
        private readonly IUserService _userService;

        #region Properties

        public override string PageName { get { return "Users"; } }

        private readonly ObservableCollection<UserViewModel> _searchResult = new ObservableCollection<UserViewModel>();
        public ObservableCollection<UserViewModel> SearchResult
        {
            get { return _searchResult; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { if (_name != value) { _name = value; NotifyPropertyChanged(); } }
        }

        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { if (_displayName != value) { _displayName = value; NotifyPropertyChanged(); } }
        }

        #endregion Properties

        #region Commands

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null) { _searchCommand = new DelegateCommand<object>(p => Search()); }
                return _searchCommand;
            }
        }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null) { _addCommand = new DelegateCommand<object>(p => Add()); }
                return _addCommand;
            }
        }

        private ICommand _openDetailsCommand;
        public ICommand OpenDetailsCommand
        {
            get
            {
                if (_openDetailsCommand == null) { _openDetailsCommand = new DelegateCommand<TappedRoutedEventArgs>(p => OpenDetails(p)); }
                return _openDetailsCommand;
            }
        }

        #endregion Commands

        //public UserListViewModel() : this(
        //    ServiceLocator.Current.GetInstance<IUserService>(),
        //    ServiceLocator.Current.GetInstance<INavigationService>())
        //{
        //}

        public UserListViewModel(IUserService userService, INavigationService navigationService) : base(navigationService)
        {
            _userService = userService;
        }

        private void Search()
        {

            var users = _userService.Find(u =>
                (string.IsNullOrEmpty(Name) || u.Name.Contains(Name)) &&
                (string.IsNullOrEmpty(DisplayName) || u.DisplayName.Contains(DisplayName)));
            SearchResult.Clear();
            var avaliableRoles = _userService.GetAvaliableRoles();
            foreach (var user in users)
            {
                SearchResult.Add(new UserViewModel(user, avaliableRoles, _userService, Navigation));
            }
        }

        private void Add()
        {
            Navigation.Navigate(typeof(UserViewModel));
        }

        private void OpenDetails(TappedRoutedEventArgs eventArgs)
        {
            var userViewModel = ((FrameworkElement)eventArgs.OriginalSource).DataContext as UserViewModel;
            if (userViewModel != null)
            {
                Navigation.Navigate(userViewModel.GetType(), userViewModel.Id);
            }
        }
    }
}
