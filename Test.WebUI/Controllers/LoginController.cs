using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Test.Domain.Concrete;
using Test.Domain.Abstract;

namespace Test.WebUI.Controllers
{
    public class LoginController : Controller
    {
        IUserRepository userRepository;

        public LoginController(IUserRepository user)
        {
            userRepository = user;
        }

        public ActionResult Index()
        {
            return View();
        }
        //TODO make validation messages for empty fields display properly
        //check database for username nad password
        [HttpPost]
        public ActionResult Authorize(Test.WebUI.Models.UserViewModel userViewModel)
        {
            using (EFDbContext db = new EFDbContext())
            {
                var userDetails = db.Users.Where(x => x.UserName == userViewModel.UserName && x.Password == userViewModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    TempData["message"] = string.Format("Wrong username or password");
                    return View("Index", userViewModel);
                }
                else
                {
                    Session["UserID"] = userDetails.UserID;
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        
        //end session and log out user
        public ActionResult LogOut()
        {
            Session.Abandon();
            return View("Index","Login");
        }
    }
}