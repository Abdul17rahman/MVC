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

        CarsEntities2 carsdb = new CarsEntities2();

        public ActionResult Index()
        {
            if (Session["Username"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login");
            }

            var data = carsdb.Vehicles.ToList();
            
            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Vehicle veh)
        {
            if (ModelState.IsValid)
            {
                carsdb.Vehicles.Add(veh);
                carsdb.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            var data = carsdb.Vehicles.Where(x => x.vid == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult Delete(Vehicle veh)
        {
            var data = carsdb.Vehicles.Where(x => x.vid == veh.vid).FirstOrDefault();
            carsdb.Vehicles.Remove(data);
            carsdb.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = carsdb.Vehicles.Where(x => x.vid == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Vehicle veh)
        {
            if (ModelState.IsValid)
            {
                var data = carsdb.Vehicles.Where(x => x.vid == veh.vid).FirstOrDefault();
                data.vname = veh.vname;
                data.price = veh.price;
                data.stock = veh.stock;
                data.brand = veh.brand;
                data.supplier = veh.supplier;
                data.category = veh.category;
                data.condition = veh.condition;
                carsdb.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Errro working on your edit");
            }
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
                    Session["Username"] = user.username;
                    Session["UserId"] = user.userid;
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
        }

    }
}