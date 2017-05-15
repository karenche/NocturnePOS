using System;

namespace Nocturne.App.Helpers
{
    public interface INavigationService
    {
        void Navigate(Type modelType, int? id = null);
    }
}
