using Nocturne.App.Helpers;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// Used to display specific client information in active clients list.
    /// </summary>
    public class ClientViewModel : ViewModelBase
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

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set { if (_surname != value) { _surname = value; NotifyPropertyChanged(); } }
        }

        private string _idcode;
        public string IdCode
        {
            get { return _idcode; }
            set { if (_idcode != value) { _idcode = value; NotifyPropertyChanged(); } }
        }

        // Formatted header, e.g. "Surname, Name"
        public string ClientViewHeader
        {
            get
            {
                return string.Format("{0}{1}{2}", Surname, (Surname != null && Name != null) ? ", " : "", Name);
            }
        }

        private decimal _currentBill;
        public decimal CurrentBill
        {
            get { return _currentBill; }
            set { if (_currentBill != value) { _currentBill = value; NotifyPropertyChanged(); } }
        }
    }
}
