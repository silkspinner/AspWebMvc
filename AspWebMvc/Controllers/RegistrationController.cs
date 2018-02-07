using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspWebMvc.Models;

namespace AspWebMvc.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        CommunityAssist2017Entities np = new CommunityAssist2017Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "LastName, FirstName, Email, PlainPassword, Apartment, Street, City, State, Zipcode, Phone")]NewPerson p)
        {
            int result = np.usp_Register(
                p.LastName,
                p.FirstName,
                p.Email,
                p.PlainPassword,
                p.Apartment,
                p.Street,
                p.City,
                p.State,
                p.Zipcode,
                p.Phone);

            if (result != -1)
            {
                // Registration succeeded

                Message msg = new Message();
                msg.MessageText = "Thank You, " + p.FirstName + " for registering";
                return RedirectToAction("Result", msg);
            }

            Message msgInvalid = new Message();
            msgInvalid.MessageText = "Sorry, misaglignment of quantum entaglement occured, your registration failed";
            return View("Result", msgInvalid);
        }
    }
}