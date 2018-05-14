using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearningCenter.Business;
using LearningCenter.WebSite.Models;

namespace LearningCenter.WebSite.Controllers
{
    public class HomeController : Controller
    {

        private readonly IClassManager classManager;
        private readonly IUserManager userManager;
        private readonly IEnrollmentManager enrollmentManager;

        public HomeController(IClassManager classManager, IUserManager userManager, IEnrollmentManager enrollmentManager)
        {
            this.classManager = classManager;
            this.userManager = userManager;
            this.enrollmentManager = enrollmentManager;
        }


        public ActionResult Index()
        {
            return View();
        }

        

        // User Login
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    Session["User"] = new LearningCenter.WebSite.Models.UserModel { Id = user.Id, Name = user.Name };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.UserName, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(loginModel);
        }

        // Log off
        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }

        // Register an account
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel registerModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Register(registerModel.Email, registerModel.Password);


                if (user == null)
                {
                    ModelState.AddModelError("", "That username is taken, please try another or try to Log in instead.");
                    
                }
                else
                {
                    Session["User"] = new LearningCenter.WebSite.Models.UserModel { Id = user.Id, Name = user.Name };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(registerModel.Email, false);

                    return Redirect(returnUrl ?? "~/");
                }

            }
            return View(registerModel);
        }

        public ActionResult Classlist()
        {
            var classlist = classManager.Classes
                                        .Select(t => new LearningCenter.WebSite.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                                        .ToArray();
            var model = new IndexModel { classlist = classlist };
            return View(model);
        }

        // Enroll in a class
        [Authorize]
        [HttpGet]
        public ActionResult Enrollinclass()
        {
            
            var classlist = classManager.Classes
                                        .Select(t => new LearningCenter.WebSite.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                                        .ToArray();
            var model = new IndexModel { classlist = classlist };
            return View(model);
        }
        [HttpPost]
        public ActionResult Enrollinclass(IndexModel enroll)
        {
            var user = (LearningCenter.WebSite.Models.UserModel)Session["User"];
            int classId = int.Parse(enroll.classToAdd);
            enrollmentManager.Add(user.Id, classId);

            
            return RedirectToAction("Studentclasses");
        }


        // Classes registered by student
        [Authorize]
        [HttpGet]
        public ActionResult Studentclasses()
        {
            
            var user = (LearningCenter.WebSite.Models.UserModel)Session["User"];

            var registeredClasses = enrollmentManager.GetAll(user.Id)
                .Select(t => new LearningCenter.WebSite.Models.EnrollmentModel(t.classId, t.name, t.classPrice, t.description))
                .ToList();

            var classlist = classManager.Classes
                                        .Select(t => new LearningCenter.WebSite.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                                        .ToArray();


            var model = new IndexModel { registeredClasses = registeredClasses, user = user, classlist = classlist };
            return View(model);
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
    }


}