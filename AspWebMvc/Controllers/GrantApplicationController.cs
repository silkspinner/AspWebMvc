using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspWebMvc.Models;

namespace AspWebMvc.Controllers
{
    public class GrantApplicationController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        // GET: Donations
        public ActionResult Index()
        {
            if (Session["personKey"] == null)
            {
                Message m = new Message();
                m.MessageTitle = "Grant Applications";
                m.MessageText = "Must Be logged in to apply for a grant";
                return RedirectToAction("Result", m);
            }
            ViewBag.GrantTypeKey = new SelectList(db.GrantTypes, "GrantTypeKey", "GrantTypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PersonKey, GrantApplicationDate, GrantTypeKey, GrantApplicationRequestAmount," +
            "GrantApplicationReason, GrantApplicationStatusKey, ")]GrantApplication ga)
        {
            ga.GrantAppicationDate = DateTime.Now;
            ga.PersonKey = (int)Session["personKey"];
            ga.GrantApplicationStatusKey = 1;
            db.GrantApplications.Add(ga);
            db.SaveChanges();

            Message m = new Message();
            m.MessageTitle = "Grant Applications";
            m.MessageText = "Thank you, your grant application has been accepted for processing.";
            return RedirectToAction("Result", m);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }
    }
}