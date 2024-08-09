﻿using Hospital_Management.Models;

namespace Hospital_Management.Services
{
    public interface IUserServices<TUser> where TUser : ApplicationUser
    {
        Task<bool> Register(TUser user, string password, string role = "Patient");
        Task<bool> Login(string usernameOrEmail, string password);
        Task<bool> Logout();
        Task<TUser> GetUserByIdAsync(string userId);
        Task<TUser> GetUserByUsernameAsync(string username);
        Task<bool> UpdateUserAsync(TUser user);
        Task<bool> DeleteUserAsync(string userId);
        Task<IList<string>> GetUserRolesAsync(TUser user);
        Task<bool> AddUserToRoleAsync(TUser user, string role);
        Task<bool> RemoveUserFromRoleAsync(TUser user, string role);
    }
}
