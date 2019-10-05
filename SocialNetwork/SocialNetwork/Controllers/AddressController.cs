using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    public class AddressController : Controller
    {
        Models.SocialNetworkDBEntities db = new Models.SocialNetworkDBEntities();
        // GET: Address/5
        public ActionResult Index(int id) //profile ID
        {
            ViewBag.id = id;
            ViewBag.username = GetProfile(id).User.username;
            return View(db.Addresses.Where(address => address.profile_id == id));
        }

        // GET: Address/Details/5
        public ActionResult Details(int id)
        {
            var theAddress = GetAddress(id);
            ViewBag.id = theAddress.profile_id;
            return View(theAddress);
        }

        // GET: Address/Create
        public ActionResult Create(int id) //profile ID
        {
            ViewBag.countries = new SelectList(from c in db.Countries
                                               select c.country_name);
            ViewBag.id = id;
            return View();
        }

        // POST: Address/Create
        [HttpPost]
        public ActionResult Create(int id, FormCollection collection)
        {
            string countryName = collection["country_name"];
            try
            {
                // TODO: Add insert logic here
                Models.Address newAddress = new Models.Address()
                {
                    city = collection["city"],
                    description = collection["description"],
                    street = collection["street"],
                    province = collection["province"],
                    postal_code = collection["postal_code"],
                    country_id = (from c in db.Countries
                                where c.country_name.Equals(countryName)
                                select c.country_id).FirstOrDefault(),
                    profile_id = id
                };

                db.Addresses.Add(newAddress);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = id });
            }
            catch
            {
                ViewBag.countries = new SelectList(from c in db.Countries
                                                   select c.country_name);
                ViewBag.id = id;
                return View();
            }
        }

        // GET: Address/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.countries = new SelectList(from c in db.Countries
                                               select c.country_name);
            var theAddress = GetAddress(id);
            ViewBag.id = theAddress.profile_id;
            return View(theAddress);
        }

        // POST: Address/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var theAddress = GetAddress(id);
            string countryName = collection["country_name"];
            try
            {
                // TODO: Add update logic here
                theAddress.city = collection["city"];
                theAddress.description = collection["description"];
                theAddress.street = collection["street"];
                theAddress.province = collection["province"];
                theAddress.postal_code = collection["postal_code"];
                theAddress.country_id = (from c in db.Countries
                                        where c.country_name.Equals(countryName)
                                        select c.country_id).FirstOrDefault();

                db.SaveChanges();

                return RedirectToAction("Index", new { id = theAddress.profile_id });
            }
            catch
            {
                ViewBag.countries = new SelectList(from c in db.Countries
                                                   select c.country_name);
                ViewBag.id = theAddress.profile_id;
                return View(theAddress);
            }
        }

        // GET: Address/Delete/5
        public ActionResult Delete(int id)
        {
            var theAddress = GetAddress(id);
            ViewBag.id = theAddress.profile_id;
            return View(theAddress);
        }

        // POST: Address/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var theAddress = GetAddress(id);
            try
            {
                // TODO: Add delete logic here
                db.Addresses.Remove(theAddress);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = theAddress.profile_id });
            }
            catch
            {
                ViewBag.id = theAddress.profile_id;
                return View(theAddress);
            }
        }

        public Models.Profile GetProfile(int id)
        {
            return db.Profiles.FirstOrDefault(prof => prof.profile_id == id);
        }

        public Models.Address GetAddress(int id)
        {
            return db.Addresses.FirstOrDefault(address => address.address_id == id);
        }
    }
}
