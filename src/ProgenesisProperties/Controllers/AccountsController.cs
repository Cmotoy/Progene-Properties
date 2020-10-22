using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgenesisProperties.data.Entities;
using ProgenesisProperties.Interfaces;
using ProgenesisProperties.Models;

namespace ProgenesisProperties.Controllers
{  
       public class AccountsController : Controller
       {
            private readonly IAccountsService _accountsService;            
            private readonly SignInManager<ApplicationUser> _signInManager;

           public AccountsController (IAccountsService accountsService, SignInManager <ApplicationUser> signInManager)
            {
               _accountsService = accountsService;
               _signInManager = signInManager;
            }
       public IActionResult About()
       {
           return View();
       }
       [HttpPost]
        public async Task <IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect ("/");
        }
                   
        [HttpGet]
        public IActionResult Login () 
            {
        
             return View ();
            }

        [HttpPost]
    
        public async Task <IActionResult> Login (LoginModel model)
        {
            if (!ModelState.IsValid)
            return View();
            
           try 
           {
               var result = await _signInManager.PasswordSignInAsync (model.Email, model.Password, false, false);
               if (!result.Succeeded)
               {
                   ModelState.AddModelError ("", "Login failed, please check your details");
                   return View();
               }
               return LocalRedirect ("~/");
           }
           catch (Exception e)  
            {
               ModelState.AddModelError("", e.Message);
               return View ();
            }
        }
        [HttpGet]
        public IActionResult SignUp ()
            {
                return View();
            }

          [HttpPost]
        public async Task <IActionResult> SignUp (SignUpModel model)
        {
                 if (!ModelState.IsValid)  
                 return View();

            try      
            {
               var user = await _accountsService.CreateUserAsync(model);
               await _signInManager.SignInAsync (user, isPersistent : false);
               return LocalRedirect("~/");
            }   
                catch (Exception e)  
            {
               ModelState.AddModelError("", e.Message);
               return View ();
            }
        }
    }
}
