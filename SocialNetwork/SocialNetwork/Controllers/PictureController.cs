using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    public class PictureController : Controller
    {
        Models.SocialNetworkDBEntities db = new Models.SocialNetworkDBEntities();
        // GET: Picture
        public ActionResult Index(int id) //Profile ID
        {
            ViewBag.id = id;
            ViewBag.username = GetProfile(id).User.username;
            return View(db.Pictures.Where(pic => pic.profile_id == id));
        }

        // GET: Picture/Details/5
        public ActionResult Details(int id)
        {
            var thePicture = GetPicture(id);
            ViewBag.id = thePicture.profile_id;
            return View(thePicture);
        }

        public ActionResult SetProfilePicture(int id)
        {
            var thePicture = GetPicture(id);
            ViewBag.id = thePicture.profile_id;
            return View(thePicture);
        }

        [HttpPost]
        public ActionResult SetProfilePicture(int id, FormCollection collection)
        {
            int profileId = int.Parse(collection["profile_id"]);
            try
            {
                var theProfile = (from p in db.Profiles
                                 where p.profile_id == profileId
                                 select p).FirstOrDefault();

                theProfile.profile_picture = id;
                db.SaveChanges();

                return RedirectToAction("Index", new { id = theProfile.profile_id });
            }
            catch
            {
                var thePicture = GetPicture(id);
                ViewBag.id = thePicture.profile_id;
                return View(thePicture);
            }
        }

        // GET: Picture/Create
        public ActionResult Create(int id) //Profile ID
        {
            ViewBag.id = id;
            return View();
        }

        // POST: Picture/Create
        [HttpPost]
        public ActionResult Create(int id, FormCollection collection, HttpPostedFileBase picture)
        {
            try
            {
                string[] allowedPicTypes = { "image/png", "image/jpeg", "image/gif", "image/bmp", "image/svg+xml",
                   "image/tiff", "image/x-icon" };
                if (picture != null && allowedPicTypes.Contains(picture.ContentType) && picture.ContentLength > 0)
                {
                    Guid g = Guid.NewGuid();
                    string filename = g.ToString() + Path.GetExtension(picture.FileName);

                    string picPath = Path.Combine(Server.MapPath("~/Pictures/"), filename);
                    picture.SaveAs(picPath);
                    // TODO: Add insert logic here
                    Models.Picture newPicture = new Models.Picture()
                    {
                        caption = collection["caption"],
                        picture_path = "~/Pictures/" + filename,
                        location = collection["location"],
                        profile_id = id
                    };

                    db.Pictures.Add(newPicture);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", new { id = id });
            }
            catch
            {
                ViewBag.id = id;
                return View();
            }
        }

        // GET: Picture/Edit/5
        public ActionResult Edit(int id)
        {
            var thePicture = GetPicture(id);
            ViewBag.id = thePicture.profile_id;
            return View(thePicture);
        }

        // POST: Picture/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, HttpPostedFileBase picture)
        {
            var thePicture = GetPicture(id);
            try
            {
                string[] allowedPicTypes = { "image/png", "image/jpeg", "image/gif", "image/bmp", "image/svg+xml",
                   "image/tiff", "image/x-icon" };
                if (picture != null && allowedPicTypes.Contains(picture.ContentType) && picture.ContentLength > 0)
                {
                    System.IO.File.Delete(Server.MapPath(thePicture.picture_path));
                    picture.SaveAs(Server.MapPath(thePicture.picture_path));
                }
                thePicture.caption = collection["caption"];
                thePicture.location = collection["location"];

                db.SaveChanges();
                
                return RedirectToAction("Index", new { id = thePicture.profile_id });
            }
            catch
            {
                ViewBag.id = thePicture.profile_id;
                return View(thePicture);
            }
        }

        // GET: Picture/Delete/5
        public ActionResult Delete(int id)
        {
            var thePicture = GetPicture(id);
            ViewBag.id = thePicture.profile_id;
            return View(thePicture);
        }

        // POST: Picture/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var thePicture = GetPicture(id);
            try
            {
                System.IO.File.Delete(Server.MapPath(thePicture.picture_path));
                if(thePicture.Profile.profile_picture == id)
                    thePicture.Profile.profile_picture = null;

                db.Pictures.Remove(thePicture);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = thePicture.profile_id });
            }
            catch
            {
                ViewBag.id = thePicture.profile_id;
                return View(thePicture);
            }
        }

        public Models.Picture GetPicture(int id)
        {
            return db.Pictures.SingleOrDefault(pic => pic.picture_id == id);
        }

        public Models.Profile GetProfile(int id)
        {
            return db.Profiles.FirstOrDefault(prof => prof.profile_id == id);
        }
    }
}
