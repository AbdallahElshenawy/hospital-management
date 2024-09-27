using Hospital_Management.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Claims;

namespace Hospital_Management.Services
{
    public class UserServices<TUser>(UserManager<TUser> userManager, SignInManager<TUser> signInManager) : IUserServices<TUser> where TUser : ApplicationUser
    {
        public async Task<bool> Register(TUser user, string password, string role = "Patient")
        {
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
            return result.Succeeded;
        }

        public async Task<bool> Login(string usernameOrEmail, string password)
        {
            TUser user;

            if (usernameOrEmail.Contains("@"))
            {
                user = await userManager.FindByEmailAsync(usernameOrEmail);
            }
            else
            {
                user = await userManager.FindByNameAsync(usernameOrEmail);
            }

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user.UserName, password, false, false);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> Logout()
        {
            await signInManager.SignOutAsync();
            return true;
        }

        public async Task<TUser> GetUserByIdAsync(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }

        public async Task<TUser> GetUserByUsernameAsync(string username)
        {
            return await userManager.FindByNameAsync(username);
        }

        public async Task<bool> UpdateUserAsync(TUser user)
        {
            var result = await userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<IList<string>> GetUserRolesAsync(TUser user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public async Task<bool> AddUserToRoleAsync(TUser user, string role)
        {
            var result = await userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<bool> RemoveUserFromRoleAsync(TUser user, string role)
        {
            var result = await userManager.RemoveFromRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<TUser> GetUserByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public Task<string> GetUserEmailAsync(TUser user)
        {
            return userManager.GetEmailAsync(user);
        }

        public Task<string> GenerateEmailConfirmationTokenAsync(TUser user)
        {
            return userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<bool> ConfirmEmailAsync(TUser user, string token)
        {
            var result = await userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }

        public Task<string> GenerateEmailPasswordResetTokenAsync(TUser user)
        {
            return userManager.GeneratePasswordResetTokenAsync(user);
        }

        public Task<TUser> GetUserByEmailAsync(string email)
        {
            return userManager.FindByEmailAsync(email);
        }

        public Task ResetPasswordAsync(TUser user, string token, string newPassword)
        {
            return userManager.ResetPasswordAsync(user, token, newPassword);
        }
    }
}
