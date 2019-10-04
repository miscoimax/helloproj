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
        public ActionResult Index(int id) //user ID
        {
            ViewBag.id = id;
            return View(GetProfiles(id));
        }

        // GET: Profile/Details/5
        public ActionResult Details(int id)
        {
            var theProfile = GetProfile(id);
            ViewBag.id = theProfile.user_id;
            return View(theProfile);
        }

        // GET: Profile/Create
        public ActionResult Create(int id) //user ID
        {
            ViewBag.Id = id;
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        public ActionResult Create(int id, FormCollection collection)
        {
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
            var theProfile = GetProfile(id);
            ViewBag.id = theProfile.user_id;
            return View(theProfile);
        }

        // POST: Profile/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
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
            return View();
        }

        // POST: Profile/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

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
