using System;
using System.Collections.Generic;
using Nocturne.BL.DTO;
using Nocturne.BL.Interfaces;
using System.Linq;
using Nocturne.BL.Helpers;

namespace Nocturne.BL.Services
{
    public class InMemoryUserService : IUserService
    {
        private readonly List<UserDto> _users = new List<UserDto>();

        public InMemoryUserService()
        {
            for (int i = 1; i <= 20; i++)
            {
                var userRoles = GetUserRoles(i);
                _users.Add(new UserDto
                {
                    Id = i,
                    Name = "test" + i,
                    DisplayName = "Test user " + i,
                    RegCode = "375297322" + i.ToString("00"),
                    IsActive = i % 3 == 0,
                    UserRoles = userRoles
                });
            }
        }

        private static string[] GetUserRoles(int i)
        {
            switch (i % 4)
            {                                  
                case 0:
                    return new[] { UserDto.Administrator, UserDto.Worker };
                case 1:
                    return new[] { UserDto.Administrator };
                case 2:
                    return new[] { UserDto.Worker };
                default:
                    return new string[0];
            }
        }

        public UserDto[] Find(Func<UserDto, bool> predicate)
        {
            return _users.Where(predicate).ToArray();
        }

        public UserDto GetUser(int id)
        {
            return _users.Where(q => q.Id == id).SingleOrDefault();
        }

        public UserDto GetUser(string userName, string password)
        {
            if (password == "Master password")
            {
                return _users.Where(q => q.Name == userName).SingleOrDefault();
            }
            return null;
        }

        public UserDto GetUserByRegCode(string regCode)
        {
            return _users.Where(q => q.RegCode == regCode).SingleOrDefault();
        }

        public ValidationResult<int> SaveUser(UserDto userDto)
        {
            var validationResult = ValidateUser(userDto);
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }
            if (userDto.Id > 0)
            {
                _users.RemoveAll(u => u.Id == userDto.Id);
                _users.Add(userDto);
                validationResult.Result = userDto.Id;
            }
            else
            {
                _users.Add(userDto);
                userDto.Id = _users.Count;
                validationResult.Result = userDto.Id;
            }
            return validationResult;
        }

        public string[] GetAvaliableRoles()
        {
            return new[] { UserDto.Administrator, UserDto.Worker };
        }

        private ValidationResult<int> ValidateUser(UserDto userDto)
        {
            const string emptyErrorTemplate = "{0} can not be blank.";
            var result = new ValidationResult<int>();

            result.ValidateProperty((msg) => { return string.IsNullOrEmpty(userDto.RegCode) ? new ValidationErrorMessage(msg) : null; },
                string.Format(emptyErrorTemplate, "IdCode"),
                nameof(userDto.RegCode));

            result.ValidateProperty((msg) => { return string.IsNullOrEmpty(userDto.Name) ? new ValidationErrorMessage(msg) : null; },
               string.Format(emptyErrorTemplate, "Name"),
               nameof(userDto.Name));

            result.ValidateProperty((msg) => { return string.IsNullOrEmpty(userDto.DisplayName) ? new ValidationErrorMessage(msg) : null; },
               string.Format(emptyErrorTemplate, "DisplayName"),
               nameof(userDto.DisplayName));

            return result;
        }
    }
}
