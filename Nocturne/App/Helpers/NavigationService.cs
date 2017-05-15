using Nocturne.App.Pages.Client;
using Nocturne.App.Pages.Discount;
using Nocturne.App.Pages.Product;
using Nocturne.App.Pages.Session;
using Nocturne.App.Pages.Shopping;
using Nocturne.App.Pages.User;
using Nocturne.App.ViewModels;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Nocturne.App.Helpers
{
    public class NavigationService : INavigationService
    {
        private static readonly Dictionary<Type, Type> _pages = new Dictionary<Type, Type>();
        public NavigationService()
        {
            _pages.Add(typeof(MainViewModel), typeof(MainPage));
            _pages.Add(typeof(LognPage), typeof(LognPage));

            _pages.Add(typeof(UserListViewModel), typeof(UserListPage));
            _pages.Add(typeof(UserViewModel), typeof(UserViewPage));

            _pages.Add(typeof(ClientInfoListViewModel), typeof(ClientListPage));
            _pages.Add(typeof(ClientInfoViewModel), typeof(ClientViewPage));

            _pages.Add(typeof(SessionStartViewModel), typeof(SessionStartPage));
            _pages.Add(typeof(ShoppingViewModel), typeof(ShoppingPage));
            _pages.Add(typeof(SessionStopViewModel), typeof(SessionStopPage));

            _pages.Add(typeof(DiscountListViewModel), typeof(DiscountListPage));
            _pages.Add(typeof(DiscountViewModel), typeof(DiscountViewPage));

            _pages.Add(typeof(ProductListViewModel), typeof(ProductListPage));
            _pages.Add(typeof(ProductViewModel), typeof(ProductViewPage));

            _pages.Add(typeof(ActiveClientListViewModel), typeof(ActiveClientListPage));

        }

        public void Navigate(Type modelType, int? id = null)
        {
            if (_pages.ContainsKey(modelType))
            {
                var pageType = _pages[modelType];
                var rootFrame = Window.Current.Content as Frame;
                if (id.HasValue)
                {
                    rootFrame.Navigate(pageType, id.Value);
                }
                else
                {
                    rootFrame.Navigate(pageType);
                }
            }
            else
            {
                throw new NotSupportedException(string.Format("ModelType not registered:{0}", modelType.FullName));
            }
        }
    }
}
