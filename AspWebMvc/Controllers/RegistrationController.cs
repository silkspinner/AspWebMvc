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

        public ActionResult Result()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "LastName, FirstName, Email, Phone, PlainPassword, Apartment, Street, City, State, Zipcode")]NewPerson p)
        {
            int result = np.usp_Register(
                p.LastName,
                p.FirstName,
                p.Email,
                p.Phone,
                p.PlainPassword,
                p.Apartment,
                p.Street,
                p.City,
                p.State,
                p.Zipcode);

            if (result != -1)
            {
                // Registration succeeded

                Message msg = new Message();
                msg.MessageText = "Thank You, " + p.FirstName + "for registering";
                return RedirectToAction("Result", msg);
            }

            Message msgInvalid = new Message();
            msgInvalid.MessageText = "Registration Failed";
            return View("Result", msgInvalid);
        }
    }
}