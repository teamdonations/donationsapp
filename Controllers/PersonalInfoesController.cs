using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Donations_Software.Models;

namespace Donations_Software.Controllers
{
    public class PersonalInfoesController : Controller
    {
        private teamdonationsEntities db = new teamdonationsEntities();

        // GET: PersonalInfoes
        public ActionResult Index()
        {
            if (Session["isAdmin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (Session["isAdmin"].ToString() != "True")
            {
                return RedirectToAction("Login", "Home");
            }

            var personalInfoes = db.PersonalInfoes.Include(p => p.User);
            return View(personalInfoes.ToList());
        }
        public ActionResult UserCreate()
        {
            if (Session["isAdmin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult UserCreate([Bind(Include = "personalInfoID,UserID,FirstName,LastName,CMA_,Email,Address1,Address2,City,State,ZipPostalCode,Country,Urbanization")] PersonalInfo personalInfo)
        {
            Session["FirstName"] = Request["FirstName"];
            Session["LastName"] = Request["LastName"];
            Session["Email"] = Request["Email"];
            Session["Address1"] = Request["Address1"];

            return RedirectToAction("Index","Confirmation");
            //return View();
        }

        // GET: PersonalInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalInfo personalInfo = db.PersonalInfoes.Find(id);
            if (personalInfo == null)
            {
                return HttpNotFound();
            }
            return View(personalInfo);
        }

        // GET: PersonalInfoes/Create
        public ActionResult Create()
        {
            if (Session["isAdmin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        // POST: PersonalInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "personalInfoID,UserID,FirstName,LastName,CMA_,Email,Address1,Address2,City,State,ZipPostalCode,Country,Urbanization")] PersonalInfo personalInfo)
        {
            if (ModelState.IsValid)
            {
                db.PersonalInfoes.Add(personalInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", personalInfo.UserID);
            return View(personalInfo);
        }

        // GET: PersonalInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["isAdmin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (Session["isAdmin"].ToString() != "True")
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalInfo personalInfo = db.PersonalInfoes.Find(id);
            if (personalInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", personalInfo.UserID);
            return View(personalInfo);
        }

        // POST: PersonalInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "personalInfoID,UserID,FirstName,LastName,CMA_,Email,Address1,Address2,City,State,ZipPostalCode,Country,Urbanization")] PersonalInfo personalInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personalInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", personalInfo.UserID);
            return View(personalInfo);
        }

        // GET: PersonalInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["isAdmin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (Session["isAdmin"].ToString() != "True")
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalInfo personalInfo = db.PersonalInfoes.Find(id);
            if (personalInfo == null)
            {
                return HttpNotFound();
            }
            return View(personalInfo);
        }

        // POST: PersonalInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonalInfo personalInfo = db.PersonalInfoes.Find(id);
            db.PersonalInfoes.Remove(personalInfo);
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
