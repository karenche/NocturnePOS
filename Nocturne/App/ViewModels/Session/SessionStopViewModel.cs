using Nocturne.App.Helpers;
using Nocturne.BL.Interfaces;
using System;
using System.Linq;
using System.Windows.Input;
using Nocturne.BL.Helpers;
using System.Collections.ObjectModel;
using Nocturne.BL.DTO;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// View model used by session stop page.
    /// </summary>
    public class SessionStopViewModel : PageViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly ICardService _cardService;
        private readonly IClientService _clientService;
        private readonly IProductService _productService;

        #region Properties 

        public override string PageName { get { return "Stop session"; } }

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

        private readonly ObservableCollection<ShoppingItemViewModel> _selectedProducts = new ObservableCollection<ShoppingItemViewModel>();
        public ObservableCollection<ShoppingItemViewModel> SelectedProducts
        {
            get { return _selectedProducts; }
        }

        private decimal _total;
        public decimal Total
        {
            get { return _total; }
            private set { if (_total != value) { _total = value; NotifyPropertyChanged(); } }
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

        public SessionStopViewModel(ISessionService sessionService, ICardService cardService, IClientService clientService, IProductService productService, INavigationService navigationService) : base(navigationService)
        {
            _sessionService = sessionService;
            _cardService = cardService;
            _clientService = clientService;
            _productService = productService;
        }

        public void CardSelected(string idCode, string firstName, string lastName)
        {
            var displayName = string.Concat(idCode, ";", firstName, ";", lastName);
            var card = _cardService.Find(q => q.CardType == CardTypeEnum.IdCard && q.RegCard == displayName).SingleOrDefault();
            SelectCard(card);
        }

        public void CardSelected(ulong uid)
        {
            var card = _cardService.Find(q => q.CardType == CardTypeEnum.RfidCard && q.Uid == uid).SingleOrDefault();
            SelectCard(card);
        }

        private void SelectCard(CardDto card)
        {
            if (card == null) return;

            CardId = card.Id;
            CardDisplayName = card.DisplayName;

            FillCustomerData(card);

            FillSessionData(card);
        }

        private void FillSessionData(CardDto card)
        {
            var session = _sessionService.Find(s => s.To == null && s.CardId == card.Id).SingleOrDefault();
            if (session != null)
            {
                Func<UsedProductDto, bool> where = up => up.SessionId == session.Id;
                var usedProducts = _sessionService.Find(where);
                SelectedProducts.Clear();

                foreach (var usedProductGroup in usedProducts.GroupBy(up => up.ProductId))
                {
                    var product = _productService.GetProductById(usedProductGroup.Key);
                    SelectedProducts.Add(new ShoppingItemViewModel(new ProductViewModel(product, _productService, null)) { Amount = usedProductGroup.Sum(up => up.Amount) });
                }
                RecalculateTotal();
            }
        }

        private void FillCustomerData(CardDto card)
        {
            var customerId = _cardService.GetClientIdAssotiatedWithCard(card.Id);
            if (customerId.HasValue)
            {
                var customer = _clientService.GetClient(customerId.Value);
                ClientDisplayName = customer.Fullname;
            }
            else
            {
                // We may have a value from previous card if 2 cards are scanned in a row
                ClientDisplayName = null;
            }
        }

        private void RecalculateTotal()
        {
            var total = decimal.Zero;
            foreach (var item in SelectedProducts)
            {
                total += item.TotalPrice;
            }
            Total = total;
        }

        private void Save()
        {
            var session = _sessionService.Find(s => s.CardId == CardId && !s.To.HasValue).SingleOrDefault();
            if (session == null)
            {
                AddValidationMessage(new ValidationErrorMessage("Session not found"), nameof(CardId));
                return;
            }
            session.To = DateTime.UtcNow;
            var sessionSaveResult = _sessionService.StopSession(session);
            SetValidationMessages(sessionSaveResult.Messages);
            if (!HasValidationMessageType<ValidationErrorMessage>())
            {
                Logger.GetLogger().LogMessage("Session stopped for client with card ID " + CardId);
                OpenHomePageCommand.Execute(null);
            }
        }
    }
}
