using Nocturne.App.Helpers;
using Nocturne.BL.DTO;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// Used to display client card information in shopping page.
    /// </summary>
    public class CardViewModel : ViewModelBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            private set { if (_id != value) { _id = value; NotifyPropertyChanged(); } }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { if (_name != value) { _name = value; NotifyPropertyChanged(); } }
        }

        public CardViewModel(CardDto card)
        {
            Id = card.Id;
            Name = card.DisplayName;
        }
    }
}
