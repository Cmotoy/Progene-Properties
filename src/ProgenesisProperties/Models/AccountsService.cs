using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProgenesisProperties.data.Entities;
using ProgenesisProperties.Interfaces;
using ProgenesisProperties.Models;

namespace ProgenesisProperties.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsService(
             UserManager<ApplicationUser> userManager)
         {
             _userManager = userManager;
         }
        public async Task <ApplicationUser> CreateUserAsync (SignUpModel model)
         {
             if (model is null) throw new ArgumentNullException(message: "Invalid details provided", null);
            
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(message: AddErrors(result), null);
            }
            return user;
        }
        private string AddErrors (IdentityResult result)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var errors in result.Errors)
            {
                 sb.Append (errors.Description + " ");
            }
            return sb.ToString();
        }
    }
}