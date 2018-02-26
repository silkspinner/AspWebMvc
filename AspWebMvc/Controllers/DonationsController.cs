using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspWebMvc.Models;

namespace AspWebMvc.Controllers
{
        public class DonationsController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        // GET: Donations
        public ActionResult Index()
        {
            if (Session["personKey"] == null)
            {
                Message m = new Message();
                m.MessageTitle = "Donations";
                m.MessageText = "Must Be logged in make donations";
                return RedirectToAction("Result", m);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "DonationKey, PersonKey, DonationDate, DonationAmount, DonationConfirmationCode, Person")]Donation d)
        {
            d.DonationDate = DateTime.Now;
            d.PersonKey = (int) Session["personKey"];
            d.DonationConfirmationCode = Guid.NewGuid();
            db.Donations.Add(d);
            db.SaveChanges();

            Message m = new Message();
            m.MessageTitle = "Donations";
            m.MessageText = "Thank you, we are very grateful for your donation.";
            return RedirectToAction("Result", m);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }
    }
}