using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DawProject.Data;
using DawProject.Models;

namespace DawProject.Controllers
{
    public class RegisterEsController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: RegisterEs
        public ActionResult Index()
        {
            return View(db.Register.ToList());
        }

        // GET: RegisterEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterE registerE = db.Register.Find(id);
            if (registerE == null)
            {
                return HttpNotFound();
            }
            return View(registerE);
        }

        // GET: RegisterEs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegisterEs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,Password,ConfirmPassword,BornDate")] RegisterE registerE)
        {
            if (ModelState.IsValid)
            {
                db.Register.Add(registerE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(registerE);
        }

        // GET: RegisterEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterE registerE = db.Register.Find(id);
            if (registerE == null)
            {
                return HttpNotFound();
            }
            return View(registerE);
        }

        // POST: RegisterEs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,Password,ConfirmPassword,BornDate")] RegisterE registerE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registerE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registerE);
        }

        // GET: RegisterEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterE registerE = db.Register.Find(id);
            if (registerE == null)
            {
                return HttpNotFound();
            }
            return View(registerE);
        }

        // POST: RegisterEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisterE registerE = db.Register.Find(id);
            db.Register.Remove(registerE);
            db.SaveChanges();
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
