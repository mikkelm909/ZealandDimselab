using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZealandDimselab.Models;
using ZealandDimselab.Interfaces;

namespace ZealandDimselab.Services
{
    public class UserService
    {
        private readonly PasswordHasher<string> _passwordHasher;
        private readonly IUserDb dbService;

        public UserService(IUserDb dbService)
        {
            this.dbService = dbService;
            _passwordHasher = new PasswordHasher<string>();
            // Test user: Admin Admin@Dimselab.dk secret1234 (don't tell anyone)
            // Test user: Oscar Oscar@email.com password
            //User user = new User("Oscar", "Osca2324@edu.easj.dk");
            //User user = new User("Admin", "Admin@Dimselab.dk", "secret1234");
            //AddUserAsync(user);

        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await dbService.GetObjectsAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await dbService.GetObjectByKeyAsync(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            
            return await dbService.GetUserByEmail(email); // Checks all users in list "users" if incoming email matches one of them.
        }

        public async Task AddUserAsync(User user)
        {
            //user.Password = _passwordHasher.HashPassword(null, user.Password);
            string[] subs = user.Email.Split("@");

            if (subs[1].ToLower() == "edu.easj.dk")
            {
                if (!(await EmailInUseAsync(user.Email)))
                {
                    user.Role = AssignRoleToUser(subs);
                    await dbService.AddObjectAsync(user);
                }
            } else { await dbService.AddObjectAsync(user); }
        }

        public async Task DeleteUserAsync(int id)
        {
            await dbService.DeleteObjectAsync(await GetUserByIdAsync(id));
        }
        
        public async Task UpdateUserAsync(User updatedUser)
        {
            await dbService.UpdateObjectAsync(updatedUser);
        }

        public async Task<bool> ValidateEmail(string email, string password)
        {
            if (await EmailInUseAsync(email))
            {
                var user = await GetUserByEmail(email);
                if (user != null)
                {
                    if (PasswordVerification(user.Password, password) == PasswordVerificationResult.Success) // Checks if password matches password.
                    {
                        return true;
                    }
                    return false;
                }
            }

            return false;
        }
        private PasswordVerificationResult PasswordVerification(string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
        }

        public ClaimsIdentity CreateClaimIdentity(string email)
        {
            User user = GetUserByEmail(email).Result;
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email)
            };

            // Regular expression to assing role
            string[] subs = email.Split('@');

            if (email.ToLower() == "Admin@Dimselab.dk".ToLower()) // This checks if the user attempts to login as an administrator account.
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }

            claims.Add(new Claim(ClaimTypes.Role, user.Role));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return claimsIdentity;
        }

        public async Task<bool> EmailInUseAsync(string email)
        {
            return await dbService.DoesEmailExist(email);
        }

        private string AssignRoleToUser(string[] emailSubs)
        {
            string role = string.Empty;
                switch (emailSubs[0].Length)
                {
                    case (8):
                        role = "student";
                        break;
                    case (4):
                        role = "teacher";
                        break;
                    default:
                        break;
                }
            return role;
        }

    }
}
