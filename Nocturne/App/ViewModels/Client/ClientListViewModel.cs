using Microsoft.Practices.ServiceLocation;
using Nocturne.App.Helpers;
using Nocturne.BL.DTO;
using Nocturne.BL.Interfaces;
using System.Collections.ObjectModel;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// Used to display a list of all currently active client sessions.
    /// </summary>
    public class ActiveClientListViewModel : PageViewModelBase
    {
        public override string PageName { get { return "Clients"; } }

        private ISessionService _sessionService;

        private readonly ObservableCollection<ClientViewModel> _clientList = new ObservableCollection<ClientViewModel>();
        public ObservableCollection<ClientViewModel> ClientList
        {
            get { return _clientList; }
        }

        public ActiveClientListViewModel(INavigationService navigationService) : base(navigationService)
        {
            _sessionService = ServiceLocator.Current.GetInstance<ISessionService>();
            IClientService clientService = ServiceLocator.Current.GetInstance<IClientService>();
            ICardService cardService = ServiceLocator.Current.GetInstance<ICardService>();

            foreach (SessionDto s in _sessionService.Find(s => s.To == null))
            {
                ClientViewModel clientVM = new ClientViewModel();

                if (s.ClientId != null)
                {
                    ClientDto client = clientService.GetClient(s.ClientId ?? 0);
                    clientVM.Name = client.Name;
                    clientVM.Surname = client.Surname;
                }
                else
                {
                    CardDto card = cardService.GetCard(s.CardId);

                    if (card.Firstname != null && card.Lastname != null)
                    {
                        clientVM.Name = card.Firstname;
                        clientVM.Surname = card.Lastname;
                    }
                    else
                    {
                        clientVM.Name = card.DisplayName;
                    }
                }

                clientVM.CurrentBill = CalculateCurrentBill(s);

                ClientList.Add(clientVM);
            }
        }

        /// <summary>
        /// Calculates current running tab for client.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private decimal CalculateCurrentBill(SessionDto s)
        {
            UsedProductDto[] products = _sessionService.Find(p => p.SessionId == s.Id);
            decimal currentBill = 0;

            foreach (UsedProductDto up in products)
            {
                ProductDto product = ServiceLocator.Current.GetInstance<IProductService>().GetProductById(up.ProductId);
                decimal? discount = ServiceLocator.Current.GetInstance<IDiscountService>().CalculateDiscountPriceForProduct(product);
                currentBill += (discount ?? product.Price) * up.Amount;
            }
            return currentBill;
        }
    }
}
