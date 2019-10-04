using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    public class FriendLinkController : Controller
    {
        // GET: FriendLink
        public ActionResult Index()
        {
            return View();
        }

        // GET: FriendLink/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FriendLink/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FriendLink/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FriendLink/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FriendLink/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FriendLink/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FriendLink/Delete/5
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
    }
}
