using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ass_4.Model;

namespace ass_4.Controllers
{
    public class HomeController : Controller
    {

        CarsEntities1 carsdb = new CarsEntities1();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            
            if (ModelState.IsValid)
            {
                var user = carsdb.users.SingleOrDefault(u => u.username == username && u.password == password);
                if (user == null)
                {
                    ModelState.AddModelError("LoginError", "Invalid username or password.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        } 
    }
}