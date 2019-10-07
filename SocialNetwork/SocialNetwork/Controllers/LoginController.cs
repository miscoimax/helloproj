using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    public class LoginController : Controller
    {
        Models.SocialNetworkDBEntities db = new Models.SocialNetworkDBEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {

                string username = collection["username"];

                //get the user record
                var theUser = db.Users.SingleOrDefault(u => u.username == username);
                //verify the password
                if (theUser != null && Crypto.VerifyHashedPassword(theUser.password_hash, collection["password_hash"]))
                {
                    Session["user_id"] = theUser.user_id;//must
                    Session["username"] = theUser.username;
                    return RedirectToAction("Index", "Profile");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Login error");
            }

        }


        // GET: Login/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Login/Register
        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Models.User newUser = new Models.User()
                {
                    username = collection["username"],
                    password_hash = Crypto.HashPassword(collection["password_hash"])
                };
                db.Users.Add(newUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}
