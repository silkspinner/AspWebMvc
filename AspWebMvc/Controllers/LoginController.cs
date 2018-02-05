using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspWebMvc.Models;

namespace AspWebMvc.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Username, Password")]LoginClass lc)
        {
            CommunityAssist2017Entities db = new CommunityAssist2017Entities();
            int loginresult = db.usp_Login(lc.Username, lc.Password);
            if (loginresult != -1)
            {
                var uid = (from p in db.People
                           where p.PersonLastName.Equals(lc.Username)
                           select p.PersonKey).FirstOrDefault();
                int pKey = (int)uid;
                Session["personKey"] = pKey;

                Message msg = new Message();
                msg.MessageText = "Thank You, " + lc.Username + "for logging in. You can now donate or apply for grants";

                return RedirectToAction("Result", msg);
            }

            Message msgInvalid = new Message();
            msgInvalid.MessageText = "Invalid Login";
            return View("Result", msgInvalid);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }


    }
}