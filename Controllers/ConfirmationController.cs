using Donations_Software.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Donations_Software.Controllers
{
    public class ConfirmationController : Controller
    {
        private teamdonationsEntities db = new teamdonationsEntities();

        // GET: Confirmation
        public ActionResult Index()
        {
            if (Session["isAdmin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            //return View();
            //int id = Session["DonationID"];
            return View(db.DonationDetails.Find(Convert.ToInt32(Session["DonationID"])));
        }
    }
}