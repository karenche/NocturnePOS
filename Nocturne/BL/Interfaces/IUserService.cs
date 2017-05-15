using System;
using Nocturne.BL.DTO;
using Nocturne.BL.Helpers;

namespace Nocturne.BL.Interfaces
{
    /// <summary>
    /// Interface to work with users
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>UserDto, or null</returns>
        UserDto GetUser(int id);

        /// <summary>
        /// Get user by username and password
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <returns>UserDto, or null</returns>
        UserDto GetUser(string userName, string password);

        /// <summary>
        /// Get user by reg. code
        /// </summary>
        /// <param name="regCode">User reg. code</param>
        /// <returns>UserDto, or null</returns>
        UserDto GetUserByRegCode(string regCode);

        /// <summary>
        /// Filters users based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An user collection that satisfy the condition.</returns>
        UserDto[] Find(Func<UserDto, bool> predicate);

        /// <summary>
        /// Save user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>Save messages and user id(if save success)</returns>
        ValidationResult<int> SaveUser(UserDto userDto);

        /// <summary>
        /// Get all avaliable user roles
        /// </summary>
        /// <returns>User role names</returns>
        string[] GetAvaliableRoles();
    }
}
