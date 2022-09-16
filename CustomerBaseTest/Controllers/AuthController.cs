﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

using System.Security.Claims;

using CustomerBaseTest.Models;
using CustomerBaseTest.ViewModels;

namespace CrmBox.WebUI.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        readonly SignInManager<AppUser> _signInManager;
        readonly UserManager<AppUser> _userManager;



        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;

        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {

            var roots = await _userManager.GetUsersInRoleAsync("Root");

            // Claimlerimi liste halinde tuttum. aşağıda ekleme yaptırmak için.

            if (roots.Count == 0)
            {
                AppUser rootUser = new() { FirstName = "root", LastName = "root", UserName = "root", Email = "root@root.com", Password = "pswrd1" };
                IdentityResult result = await _userManager.CreateAsync(rootUser, "pswrd1");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(rootUser, "Root");
   
                }
            
            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginVM vM)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vM.Username, vM.Password, false, false);

                if (result.Succeeded)
                {
                    // Giriş yapan kullanıcının Rolünü session ile viewwde göstermeyi tercih ettim. Viewbag da kullanılabilir.
                    var findUser = _userManager.Users.FirstOrDefault(x => x.UserName == vM.Username);
                    var roleNameVB = await _userManager.GetRolesAsync(findUser);
     
                    //
                    ViewBag.userPass = findUser.Password;

                    ViewBag.State = true;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.State = false;
                    
                }
            }
            return View();

        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }


        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(ResetPasswordVM model) // Maile Mesaj Gönderir.
        //{
        //    if (ModelState.IsValid)
        //    {
        //        AppUser user = await _userManager.FindByEmailAsync(model.Email);
        //        if (user != null)
        //        {

        //            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        //            var link = $"<a target=\"_blank\" href=\"https://localhost:44300{Url.Action("UpdatePassword", "Auth", new { userId = user.Id, token = HttpUtility.UrlEncode(resetToken) })}\">Yeni şifre talebi için tıklayınız</a>"; EmailHelper emailHelper = new EmailHelper();
        //            emailHelper.SendEmailPasswordReset(model.Email, link);
        //            ViewBag.State = true;
        //        }
        //        else { ViewBag.State = false; }

        //    }


        //    return View();
        //}
        //[HttpGet("[action]/{userId}/{token}")]
        //public IActionResult UpdatePassword(string userId, string token)//
        //{
        //    return View();
        //}
        //[HttpPost("[action]/{userId}/{token}")]// Maile gelen mesajdaki linke tıklandığında çalışacak kod yapısı.
        //public async Task<IActionResult> UpdatePassword(UpdatePasswordVM model, string userId, string token)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        AppUser user = await _userManager.FindByIdAsync(userId);
        //        IdentityResult result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(token), model.Password);
        //        if (result.Succeeded)
        //        {
        //            ViewBag.State = true;
        //            await _userManager.UpdateSecurityStampAsync(user);
        //        }
        //        else
        //            ViewBag.State = false;

        //    }
        //    return View();
        //}

    }
}
