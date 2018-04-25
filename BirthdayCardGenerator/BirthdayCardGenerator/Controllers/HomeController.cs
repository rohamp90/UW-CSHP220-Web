using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BirthdayCardGenerator.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.BirthdayCard confirmation)
        {
            if (ModelState.IsValid)
            {
                return View("Confirmation", confirmation);
            }
            else
            {
                return View();
            }
        }
    }
}