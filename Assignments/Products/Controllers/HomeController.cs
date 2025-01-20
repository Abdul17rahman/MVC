using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Products.Models;

namespace Products.Controllers
{
    public class HomeController : Controller
    {
        products_dbEntities _dbEFOnject = new products_dbEntities();
        // GET: Home
        public ActionResult Index()
        {
            var indexdata = _dbEFOnject.products.ToList();
            return View(indexdata);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(product frommodel)
        {
            if (ModelState.IsValid)
            {
                _dbEFOnject.products.Add(frommodel);
                _dbEFOnject.SaveChanges();

                return RedirectToAction("Index");

            }
            else
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            var data = _dbEFOnject.products.Where(y=>y.PID == id).FirstOrDefault();

            return View(data);
        }
    }
}