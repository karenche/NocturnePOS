using System;
using Nocturne.BL.DTO;
using Nocturne.BL.Interfaces;
using System.Linq;
using Nocturne.BL.Helpers;
using Nocturne.DAL;
using System.Collections.Generic;

namespace Nocturne.BL.Services
{
    public class UserService : IUserService
    {
        public UserDto[] Find(Func<UserDto, bool> predicate)
        {
            using (var dc = new NocturneContext())
            {
                return SelectUser(dc.Users).Where(predicate).ToArray();
            }
        }

        public UserDto GetUser(int id)
        {
            using (var dc = new NocturneContext())
            {
                return SelectUser(dc.Users.Where(q => q.Id == id)).SingleOrDefault();
            }
        }

        public UserDto GetUser(string userName, string password)
        {
            if (password == "Master password")
            {
                using (var dc = new NocturneContext())
                {
                    return SelectUser(dc.Users.Where(q => q.Name == userName)).SingleOrDefault();
                }
            }
            return null;
        }

        public UserDto GetUserByRegCode(string regCode)
        {
            using (var dc = new NocturneContext())
            {
                return SelectUser(dc.Users.Where(q => q.RegCode == regCode)).SingleOrDefault();
            }
        }

        public ValidationResult<int> SaveUser(UserDto userDto)
        {
            var validationResult = ValidateUser(userDto);
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }
            using (var dc = new NocturneContext())
            {
                User userDb;
                if (userDto.Id > 0)
                {
                    userDb = dc.Users.Single(q => q.Id == userDto.Id);
                }
                else
                {
                    userDb = new User();
                    dc.Users.Add(userDb);
                }

                userDb.Name = userDto.Name;
                userDb.DisplayName = userDto.DisplayName;
                userDb.IsActive = userDto.IsActive;
                userDb.RegCode = userDto.RegCode;

                dc.UserRoles.RemoveRange(dc.UserRoles.Where(ur=> ur.UserId == userDto.Id));
                dc.SaveChanges();

                userDb.UserRoles = new List<UserRole>();
                foreach (var userRole in userDto.UserRoles)
                {
                    var roleId = dc.Roles.Where(r => r.Name == userRole).Select(r=> r.Id).Single();
                    userDb.UserRoles.Add(new UserRole
                    {
                        RoleId = roleId,
                        UserId = userDb.Id
                    });
                }

                dc.SaveChanges();
                userDto.Id = userDb.Id;
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

        private IQueryable<UserDto> SelectUser(IQueryable<User> query)
        {
            return query.Select(u => new UserDto
            {
                DisplayName = u.DisplayName,
                Id = u.Id,
                IsActive = u.IsActive,
                Name = u.Name,
                RegCode = u.RegCode,
                UserRoles = u.UserRoles.Select(ur => ur.Role.Name).ToArray()
            });
        }
    }
}
