using Nocturne.App.Helpers;
using Nocturne.BL.DTO;
using Nocturne.BL.Helpers;
using Nocturne.BL.Interfaces;
using System;
using System.Linq;
using System.Windows.Input;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// View model used by session start page.
    /// </summary>
    public class SessionStartViewModel : PageViewModelBase
    {
        private readonly ISessionService _sessionService;  
        private readonly ICardService _cardService;   
        private readonly IClientService _clientService;

        #region Properties 

        public override string PageName { get { return "Start session"; } }

        private int? _clientId;
        public int? ClientId
        {
            get { return _clientId; }
            set { if (_clientId != value) { _clientId = value; NotifyPropertyChanged(); } }
        }

        private int? _cardId;
        public int? CardId
        {
            get { return _cardId; }
            set { if (_cardId != value) { _cardId = value; NotifyPropertyChanged(); } }
        }

        private string _cardDisplayName;
        public string CardDisplayName
        {
            get { return _cardDisplayName; }
            set { if (_cardDisplayName != value) { _cardDisplayName = value; NotifyPropertyChanged(); } }
        }

        private string _clientDisplayName;
        public string ClientDisplayName
        {
            get { return _clientDisplayName; }
            set { if (_clientDisplayName != value) { _clientDisplayName = value; NotifyPropertyChanged(); } }
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

        public SessionStartViewModel(ISessionService sessionService, ICardService cardService, IClientService clientService, INavigationService navigationService) : base(navigationService)
        {
            _sessionService = sessionService;
            _cardService = cardService;
            _clientService = clientService;
        }

        private void Save()
        {
            var sessionSaveResult = _sessionService.StartSession(new BL.DTO.SessionDto
            {
                CardId = CardId ?? 0,
                ClientId = ClientId != 0 ? ClientId : null,
                From = DateTime.UtcNow,
                RegisteredBy = App.CurrentUserId
            });
            SetValidationMessages(sessionSaveResult.Messages);
            if (!HasValidationMessageType<ValidationErrorMessage>())
            {
                Logger.GetLogger().LogMessage("Session started for client with card ID " + CardId);
                OpenHomePageCommand.Execute(null);
            }
        }

        public void CardSelected(ulong uid)
        {
            var card = _cardService.Find(q => q.CardType == CardTypeEnum.RfidCard && q.Uid == uid).SingleOrDefault();
            if (card == null)
            {
                card = new CardDto
                {
                    CardType = CardTypeEnum.RfidCard,
                    Uid = uid
                };
                _cardService.SaveCard(card);
            }
            SelectCard(card);
        }

        public void CardSelected(string idCode, string firstName, string lastName)
        {
            var displayName = string.Concat(idCode, ";", firstName, ";", lastName);

            var card = _cardService.Find(q => q.CardType == CardTypeEnum.IdCard && q.RegCard == displayName).SingleOrDefault();
            if (card == null)
            {
                card = new CardDto
                {
                    CardType = CardTypeEnum.IdCard,
                    RegCard = displayName,
                    Firstname = firstName,
                    Lastname = lastName
                };
                _cardService.SaveCard(card);
            }
            SelectCard(card);
        }

        private void SelectCard(CardDto card)
        {
            CardId = card.Id;
            CardDisplayName = card.DisplayName;

            var customerId = _cardService.GetClientIdAssotiatedWithCard(card.Id);
            if (customerId.HasValue)
            {
                var customer = _clientService.GetClient(customerId.Value);
                ClientId = customer.Id;
                ClientDisplayName = customer.Fullname;
            }
            else
            {
                // We may have a value from previous card if 2 cards are scanned in a row
                ClientId = null;
                ClientDisplayName = string.Empty;
            }
        }
    }
}
