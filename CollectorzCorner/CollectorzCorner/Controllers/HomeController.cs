using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollectorzCorner.Models;
using CollectorzCorner.Utilities;

namespace CollectorzCorner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Collectorz Corner!";

            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        // GET: /Home/Contact
        public ActionResult Contact()
        {
            return View();
        }

        // POST: /Home/Contact
        [HttpPost]
        public ActionResult Contact(ContactusModel model)
        {
            if (ModelState.IsValid)
            {
                Utility.SendEmail(model.Email, model.Comments);
                return RedirectToAction("Success");
            }
            else
            {
                ModelState.AddModelError("", "The Email field requires a valid email. For example: 'someone@example.com'.");
            }
            return View(model);
        }

        public ActionResult Success()
        {
            return View();
        }

        [Authorize]
        public ActionResult Profile()
        {
            var userToken = Utility.GetUserToken();
            User user = DBUtility.FindUserByUserId(userToken.UserId);
            return View(user);
        }
        
        [HttpDelete]
        public ActionResult SetTheme(string theme)
        {
            Session["theme"] = theme;
            return null;
        }

    }
}
