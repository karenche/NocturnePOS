using Nocturne.BL.DTO;
using Nocturne.App.Helpers;
using System.Windows.Input;
using Nocturne.BL.Interfaces;
using Nocturne.BL.Helpers;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// Handles viewing, editing and adding clients.
    /// </summary>
    public class ClientInfoViewModel : PageViewModelBase
    {
        private readonly IClientService _clientService;

        #region Properties 

        public override string PageName { get { return "ClientInfo"; } }

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

        private string _idCode;
        public string IdCode
        {
            get { return _idCode; }
            set { if (_idCode != value) { _idCode = value; NotifyPropertyChanged(); } }
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

        #endregion Properties 

        #region Commands

        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null) { _editCommand = new DelegateCommand(p => Edit()); }
                return _editCommand;
            }
        }

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

        public ClientInfoViewModel(IClientService clientService, INavigationService navigation) : base(navigation)
        {
            _clientService = clientService;
            PageMode = PageMode.Add;
        }

        public ClientInfoViewModel(ClientDto client,  IClientService clientService, INavigationService navigation) : base(navigation)
        {
            _clientService = clientService;
            Surname = client.Surname;
            Name = client.Name;
            IdCode = client.IdCode;
            Id = client.Id;
            PageMode = PageMode.View;
        }

        private void Edit()
        {
            PageMode = PageMode.Edit;
        }

        private void Save()
        {
            var clientSaveResult = _clientService.SaveClient(new ClientDto
            {
                Surname = Surname,
                IdCode = IdCode,
                Name = Name,
                Id = Id
            });
            SetValidationMessages(clientSaveResult.Messages);
            if (!HasValidationMessageType<ValidationErrorMessage>())
            {
                Id = clientSaveResult.Result;
                PageMode = PageMode.View;
                Logger.GetLogger().LogMessage("Changed client " + Surname + " data");
            }
        }
    }
}
