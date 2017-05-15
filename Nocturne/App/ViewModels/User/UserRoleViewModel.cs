namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// Used to view user role information.
    /// </summary>
    public class UserRoleViewModel : ViewModelBase
    {
        #region Properties

        private string _name;
        public string Name
        {
            get { return _name; }                                                    
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { if (_isSelected != value) { _isSelected = value; NotifyPropertyChanged(); } }
        }

        #endregion Properties

        public UserRoleViewModel(string name)
        {
            _name = name;
        }
    }
}
