using Donations_Software.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Donations_Software.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult Login(User user)
        {
            //Checking the state of model passed as parameter.
            if (ModelState.IsValid)
            {

                //Validating the user, whether the user is valid or not.
                var isValidUser = IsValidUser(user);

                //If user is valid & present in database, we are redirecting it to Welcome page.
                if (isValidUser != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Index");
                }
                else
                {
                    //If the username and password combination is not present in DB then error message is shown.
                    ModelState.AddModelError("Failure", "Wrong Username and password combination !");
                    return View();
                }
            }
            else
            {
                //If model state is not valid, the model with error message is returned to the View.
                return View(user);
            }

            return View();
        }

        public User IsValidUser(User user)
        {
            using (var dataContext = new teamdonationsEntities())
            {
                //Retireving the user details from DB based on username and password enetered by user.
                User user1 = dataContext.Users.Where(query => query.Email.Equals(user.Email) && query.Password.Equals(user.Password)).SingleOrDefault();
                //If user is present, then true is returned.
                if (user1 == null)
                    return null;
                //If user is not present false is returned.
                else
                    return user1;
            }
        }

    }
}