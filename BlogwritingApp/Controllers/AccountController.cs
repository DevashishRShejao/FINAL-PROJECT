using BlogwritingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace BlogwritingApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        // public ActionResult Index()
        //{
        //  return View();
        //}
        [AllowAnonymous]
        public ActionResult Index() => View();
        [HttpGet]
        public ActionResult Login(string content)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserTable postedData)
        {
           
                var context = new BlogDatabaseEntities();
                var user = context.UserTables.SingleOrDefault(rec => rec.UserName == postedData.UserName && rec.UserPassword == postedData.UserPassword);
                if (user != null)
                {
                    Session["currentUser"] = user; //To store it till the User logs off!!!
                    //FormsAuthentication.RedirectFromLoginPage(UserName, true);
                    return RedirectToAction("Index","Account");
                }
                else
                {
                    ViewBag.ErrorInfo = "Login has failed!";
                    return View();
                }
            }

        public ActionResult RegisterNew()
        {
            var userDetails = new UserTable();
            return View(userDetails);
        }
        [HttpPost]
        public ActionResult RegisterNew(UserTable postedData)
        {
            if (ModelState.IsValid && isValidUser(postedData.UserName))
            {
                var context = new BlogDatabaseEntities();
                var userTableRow = new UserTable
                {
                    UserEmailAddress = postedData.UserEmailAddress,
                    UserPassword = postedData.UserPassword,
                    UserName = postedData.UserName,
                    Address=postedData.Address
                };
                context.UserTables.Add(userTableRow);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Failuers", "User Already Exists");
                return View();
            }
        }

        private bool isValidUser(string userName)
        {
            var context = new BlogDatabaseEntities();
            var selected = context.UserTables.FirstOrDefault((user) => user.UserName == userName);
            return selected == null ? true : false;
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}