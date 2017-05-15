using Nocturne.BL.Interfaces;
using Nocturne.App.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// Used to display a list of clients and searching.
    /// </summary>
    public class ClientInfoListViewModel : PageViewModelBase
    {
        private readonly IClientService _clientService;

        #region Properties

        public override string PageName { get { return "Clients"; } }

        private readonly ObservableCollection<ClientInfoViewModel> _searchResult = new ObservableCollection<ClientInfoViewModel>();
        public ObservableCollection<ClientInfoViewModel> SearchResult
        {
            get { return _searchResult; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { if (_name != value) { _name = value; NotifyPropertyChanged(); } }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set { if (_surname != value) { _surname = value; NotifyPropertyChanged(); } }
        }

        private string _idCode;
        public string IdCode
        {
            get { return _idCode; }
            set { if (_idCode != value) { _idCode = value; NotifyPropertyChanged(); } }
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

        public ClientInfoListViewModel(IClientService clientService, INavigationService navigationService) : base(navigationService)
        {
            _clientService = clientService;
        }

        private void Search()
        {

            var clients = _clientService.Find(u =>
                (string.IsNullOrEmpty(Name) || u.Name.Contains(Name)) &&
                (string.IsNullOrEmpty(Surname) || u.Surname.Contains(Surname))&&
                (string.IsNullOrEmpty(IdCode) || u.IdCode.Contains(IdCode)));
            SearchResult.Clear();
            foreach (var client in clients)
            {
                SearchResult.Add(new ClientInfoViewModel(client, _clientService, Navigation));
            }
        }

        private void Add()
        {
            Navigation.Navigate(typeof(ClientInfoViewModel));
        }

        private void OpenDetails(TappedRoutedEventArgs eventArgs)
        {
            var clientViewModel = ((FrameworkElement)eventArgs.OriginalSource).DataContext as ClientInfoViewModel;
            if (clientViewModel != null)
            {
                Navigation.Navigate(clientViewModel.GetType(), clientViewModel.Id);
            }
        }
    }
}
