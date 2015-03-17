using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using virtualtri.Models;
using virtualtri.Properties;

namespace virtualtri.Controllers
{
    public class ParticipantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //
        // GET: /Particpants/
        public ActionResult Index()
        {
            ViewBag.TeamName = Settings.Default.team_name;
            var participants = new List<ParticpantModel>();

            var users = from u in db.Users
                        select new { u.UserName, u.EmailAddress, u.DateStarted, u.DateCompleted };

            foreach (var user in users) 
            {
                participants.Add(new ParticpantModel()
                {
                    UserName = user.UserName,
                    EmailAddress = user.EmailAddress,
                    DateStarted = user.DateStarted,
                    DateCompleted = user.DateCompleted
                });
            }

            return View(participants);

        }

        //
        // GET: /Particpants/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.TeamName = Settings.Default.team_name;
            return View();
        }

        //
        // GET: /Particpants/Create
        public ActionResult Create()
        {
            ViewBag.TeamName = Settings.Default.team_name;
            return View();
        }

        //
        // POST: /Particpants/Create
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

        //
        // GET: /Particpants/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.TeamName = Settings.Default.team_name;
            return View();
        }

        //
        // POST: /Particpants/Edit/5
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

        //
        // GET: /Particpants/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.TeamName = Settings.Default.team_name;
            return View();
        }

        //
        // POST: /Particpants/Delete/5
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
