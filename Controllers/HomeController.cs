using System.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using C_Sharp_Login_Registration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
namespace C_Sharp_Login_Registration.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController (MyContext context){
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Register")]
        public IActionResult Register(RegistrationUser newregistrationUser){
            if(ModelState.IsValid)
            {
                if(dbContext.RegUser.Any(u => u.Email == newregistrationUser.Email))
                {
                    ModelState.AddModelError("Email","Email already Exist");
                    return View("Index");
                }
                dbContext.RegUser.Add(newregistrationUser);
                PasswordHasher<RegistrationUser> Hasher = new PasswordHasher<RegistrationUser>();
                string pwdHash = Hasher.HashPassword(newregistrationUser,newregistrationUser.Password);
                newregistrationUser.Password = pwdHash;
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserId",newregistrationUser.RegUserId);
                // dbContext.LoginUser.Add(newregistrationUser);
                // Console.WriteLine(newregistrationUser);

            return RedirectToAction("Success");
            }
            else{
                return View("Index");
            }
        }
        [HttpGet("Success")]
        public IActionResult Success(RegistrationUser newregistrationUser)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                ModelState.AddModelError("Login","You need to Login");
                return View("Index");
            }
            else
            {
                return View("Success");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLogin newLogin)
        {
            if(ModelState.IsValid){
                var UserInDb = dbContext.RegUser.FirstOrDefault(user => user.Email == newLogin.LoginEmail);
                if(UserInDb == null)
                {
                    ModelState.AddModelError("InValid","Entered EMail is not exist");
                    return View("Index");
                }
                else{
                    PasswordHasher<UserLogin> Hasher = new PasswordHasher<UserLogin>();
                    var result = Hasher.VerifyHashedPassword(newLogin,UserInDb.Password,newLogin.LoginPassword);
                    if(result == 0){
                        ModelState.AddModelError("Invalid","Invalid Login Attemp");
                        return View("Index");
                    }
                    else
                    {
                        return View("Success");
                    }
                }
            }
            return View("Index");
        }
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
