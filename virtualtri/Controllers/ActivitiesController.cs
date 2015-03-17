﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using virtualtri.Entities;
using virtualtri.Models;
using virtualtri.Properties;

namespace virtualtri.Controllers
{
    [Authorize(Users="argodev,argodev02")]
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Activities/
        public async Task<ActionResult> Index()
        {
            ViewBag.TeamName = Settings.Default.team_name;
            return View(await db.Activities.ToListAsync());
        }

        // GET: /Activities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.TeamName = Settings.Default.team_name;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: /Activities/Create
        public ActionResult Create()
        {
            ViewBag.TeamName = Settings.Default.team_name;
            return View();
        }

        // POST: /Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,ActivityType,Distance,ActivityDateTime")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(activity);
        }

        // GET: /Activities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.TeamName = Settings.Default.team_name;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: /Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,ActivityType,Distance,ActivityDateTime")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: /Activities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.TeamName = Settings.Default.team_name;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: /Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Activity activity = await db.Activities.FindAsync(id);
            db.Activities.Remove(activity);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
