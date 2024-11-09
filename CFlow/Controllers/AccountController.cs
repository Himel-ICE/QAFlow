using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CFlow.Models;

namespace CFlow.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult CreateAccount(FormCollection formCollection)
        {
            if (formCollection["FirstName"] == "" || formCollection["LastName"] == "" || formCollection["Password"] == "" || formCollection["ConPassword"] == "" || formCollection["Email"] == "")
            {
                Session["SignInSuccess"] = "False";
                return RedirectToAction("Login", "Account");
            }
            string FirstName = formCollection["FirstName"].ToString();
            string LastName = formCollection["LastName"].ToString();
            string Email = formCollection["Email"].ToString();
            string Password = formCollection["Password"].ToString();

            int result = Account.UserSignIn(FirstName, LastName, Email, Password);

            if (result == 0) Session["SignInSuccess"] = "False";
            else Session["SignInSuccess"] = "True";

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]

        public ActionResult Login(FormCollection formCollection)
        {
            if ((formCollection["Email"].ToString() == "") || (formCollection["Password"].ToString() == ""))
            {
                Session["Valid"] = "Fasle";
            }
            Account User = Account.LoginValidation(formCollection["Email"].ToString(), formCollection["Password"].ToString());

            if(User != null)
            {
                
                Session["UserID"] = User.ID;
                Session["FirstName"] = User.FirstName;
                Session["LastName"] = User.LastName;
                Session["Email"] = User.Email;
                Session["Valid"] = "True";

                return RedirectToAction("Index", "Home");
            }
            Session["Valid"] = "Fasle";
            return RedirectToAction("Login", "Account");



        }
        [HttpPost]
        public ActionResult LogOut(FormCollection formCollection)
        {
            if (formCollection["LogOut"] == "LogOut")
            {
                Session.Remove("User");
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Home", "Index");
        }
    }
}