using Nocturne.BL.DTO;
using Nocturne.App.Helpers;
using System.Windows.Input;
using System.Linq;
using Nocturne.BL.Interfaces;
using System.Collections.ObjectModel;
using System;
using Nocturne.BL.Helpers;

namespace Nocturne.App.ViewModels
{
    /// <summary>
    /// Handles viewing, editing and adding users.
    /// </summary>
    public class UserViewModel : PageViewModelBase
    {
        private readonly IUserService _userService;

        #region Properties 

        public override string PageName { get { return "User"; } }

        private readonly ObservableCollection<UserRoleViewModel> _userRoles = new ObservableCollection<UserRoleViewModel>();
        public ObservableCollection<UserRoleViewModel> UserRoles
        {
            get { return _userRoles; }
        }

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

        private string _regCode;
        public string RegCode
        {
            get { return _regCode; }
            set { if (_regCode != value) { _regCode = value; NotifyPropertyChanged(); } }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { if (_name != value) { _name = value; NotifyPropertyChanged(); } }
        }

        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { if (_displayName != value) { _displayName = value; NotifyPropertyChanged(); } }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { if (_isActive != value) { _isActive = value; NotifyPropertyChanged(); } }
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

        public UserViewModel(string[] avaliableRoles, IUserService userService, INavigationService navigation) : base(navigation)
        {
            _userService = userService;
            IsActive = true;
            PageMode = PageMode.Add;
            foreach (var roleName in avaliableRoles)
            {
                UserRoles.Add(new UserRoleViewModel(roleName));
            }
        }

        public UserViewModel(UserDto user, string[] avaliableRoles, IUserService userService, INavigationService navigation) : base(navigation)
        {
            _userService = userService;
            DisplayName = user.DisplayName;
            Name = user.Name;
            RegCode = user.RegCode;
            Id = user.Id;
            IsActive = user.IsActive;
            PageMode = PageMode.View;
            foreach (var roleName in avaliableRoles)
            {
                UserRoles.Add(new UserRoleViewModel(roleName)
                {
                    IsSelected = user.UserRoles.Contains(roleName)
                });
            }
        }

        private void Edit()
        {
            PageMode = PageMode.Edit;
        }

        private void Save()
        {
            var userRoles = UserRoles.Where(q => q.IsSelected).Select(q => q.Name).ToArray();
            var userSaveResult = _userService.SaveUser(new UserDto
            {
                DisplayName = DisplayName,
                RegCode = RegCode,
                IsActive = IsActive,
                Name = Name,
                Id = Id,
                UserRoles = userRoles
            });
            SetValidationMessages(userSaveResult.Messages);
            if (!HasValidationMessageType<ValidationErrorMessage>())
            {
                Id = userSaveResult.Result;
                PageMode = PageMode.View;
                Logger.GetLogger().LogMessage("Changed user " + DisplayName + " data");
            }
        }
    }
}
