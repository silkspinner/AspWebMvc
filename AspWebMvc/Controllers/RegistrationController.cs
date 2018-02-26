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

                Message m = new Message();
                m.MessageTitle = "Registration";
                m.MessageText = "Thank You, " + p.FirstName + " for registering";
                return RedirectToAction("Result", m);
            }

            Message bad = new Message();
            bad.MessageTitle = "Registration";
            bad.MessageText = "Sorry, misaglignment of quantum entanglement occured, your registration failed";
            return View("Result", bad);
        }
    }
}