using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    public class CountryController : Controller
    {
        Models.SocialNetworkDBEntities db = new Models.SocialNetworkDBEntities();
        // GET: Country
        public ActionResult Index()
        {
            return View(db.Countries);
        }

        // GET: Country/Details/5
        public ActionResult Details(int id)
        {
            return View(GetCountry(id));
        }

        // GET: Country/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Country/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Models.Country newCountry = new Models.Country()
                {
                    country_code = collection["country_code"],
                    country_name = collection["country_name"]
                };

                db.Countries.Add(newCountry);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Country/Edit/5
        public ActionResult Edit(int id)
        {
            return View(GetCountry(id));
        }

        // POST: Country/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var theCountry = GetCountry(id);
            try
            {
                // TODO: Add update logic here
                theCountry.country_code = collection["country_code"];
                theCountry.country_name = collection["country_name"];

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Country/Delete/5
        public ActionResult Delete(int id)
        {
            return View(GetCountry(id));
        }

        // POST: Country/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var theCountry = GetCountry(id);
            try
            {
                var addresses = from a in db.Addresses
                                where a.country_id == id
                                select a;

                db.Addresses.RemoveRange(addresses);
                db.Countries.Remove(theCountry);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(theCountry);
            }
        }

        public Models.Country GetCountry(int id)
        {
            return db.Countries.FirstOrDefault(country => country.country_id == id);
        }
    }
}
