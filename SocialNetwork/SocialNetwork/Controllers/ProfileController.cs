using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    public class ProfileController : Controller
    {
        Models.SocialNetworkDBEntities db = new Models.SocialNetworkDBEntities();
        // GET: Profile
        public ActionResult Index() //user ID
        {
            if (Session["user_id"] == null)
                return RedirectToAction("Index", "Login");
            return View(GetProfiles(int.Parse(Session["user_id"].ToString())));
        }

        // GET: Profile/Details/5
        public ActionResult Details(int id)
        {
            if (Session["user_id"] == null)
                return RedirectToAction("Index", "Login");
            return View(GetProfile(id));
        }

        // GET: Profile/Create
        public ActionResult Create() //user ID
        {
            if (Session["user_id"] == null)
                return RedirectToAction("Index", "Login");
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        public ActionResult Create(int id, FormCollection collection)
        {
            if (Session["user_id"] == null)
                return RedirectToAction("Index", "Login");
            try
            {
                Models.Profile newProfile = new Models.Profile()
                {
                    name = collection["name"],
                    bio = collection["bio"],
                    email = collection["email"],
                    phone = collection["phone"],
                    gender = collection["gender"],
                    role = collection["role"],
                    @private = bool.Parse(collection["private"]),
                    user_id = id
                };

                db.Profiles.Add(newProfile);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = id });
            }
            catch
            {
                return Content("Profile creation got messed up, sorry...");
            }
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["user_id"] == null)
                return RedirectToAction("Index", "Login");
            return View(GetProfile(id));
        }

        // POST: Profile/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (Session["user_id"] == null)
                return RedirectToAction("Index", "Login");
            try
            {
                Models.Profile theProfile = GetProfile(id);

                theProfile.name = collection["name"];
                theProfile.bio = collection["bio"];
                theProfile.email = collection["email"];
                theProfile.phone = collection["phone"];
                theProfile.gender = collection["gender"];
                theProfile.role = collection["role"];
                theProfile.@private = bool.Parse(collection["private"]);

                db.SaveChanges();

                return RedirectToAction("Index", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["user_id"] == null)
                return RedirectToAction("Index", "Login");
            return View(GetProfile(id));
        }

        // POST: Profile/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (Session["user_id"] == null)
                return RedirectToAction("Index", "Login");
            try
            {
                var theProfile = GetProfile(id);
                db.Addresses.RemoveRange(theProfile.Addresses);
                db.Comments.RemoveRange(theProfile.Comments);
                db.Comments_Like.RemoveRange(theProfile.Comments_Like);
                db.FriendLinks.RemoveRange(theProfile.FriendLinks_Requester);
                db.Likes.RemoveRange(theProfile.Likes);
                db.Messages.RemoveRange(theProfile.Messages_Sender);
                db.Pictures.RemoveRange(theProfile.Pictures);
                db.Tags.RemoveRange(theProfile.Tags_Tagger);

                db.Profiles.Remove(theProfile);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private Models.Profile GetProfile(int id)
        {
            return db.Profiles.FirstOrDefault(prof => prof.profile_id == id);
        }

        private IEnumerable<Models.Profile> GetProfiles(int userId)
        {
            return db.Profiles.Where(prof => prof.user_id == userId);
        }
    }
}
