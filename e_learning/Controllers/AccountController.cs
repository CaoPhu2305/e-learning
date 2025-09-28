using e_learning.Models;
using Ninject;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace e_learning.Controllers
{
    public class AccountController : Controller
    {


        private readonly IAccountService accountService;
        private readonly IUserService userService;

        public AccountController([Named("Oracle")] IAccountService accountService
            , IUserService userService)
        {
            this.accountService = accountService;
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = accountService.Login(model.Email, model.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);

                    var role = userService.GetUserRole(user.UserID);

                    if(role != null)
                    {
                        switch (role.RoleID)
                        {
                            case 1:
                                return RedirectToAction("AdminHomePage", "Admin");
                            case 2:
                                return RedirectToAction("LectureHomePage", "Lecture");
                            case 3:
                                return RedirectToAction("StudentHomePage", "Student");
                            default:
                                return RedirectToAction("Index", "Home");
                        }
                    }     
                }else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }
                   
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                accountService.Register(model.Email, model.Password, model.UserName);
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
       
    }
}