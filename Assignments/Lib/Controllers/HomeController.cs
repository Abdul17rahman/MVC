using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lib.Models;

namespace Lib.Controllers
{
    public class HomeController : Controller
    {
        Lib_dbEntities _LibEFObj = new Lib_dbEntities();
        public ActionResult Index()
        {
            var indexdata = _LibEFObj.Bookstbls.ToList();
            return View(indexdata);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Bookstbl frommodel)
        {
            if (ModelState.IsValid)
            {
                _LibEFObj.Bookstbls.Add(frommodel);
                _LibEFObj.SaveChanges();

                return RedirectToAction("Index");
            }
            else 
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book = _LibEFObj.Bookstbls.Find(id);

            if (book == null)
            {
                ModelState.AddModelError("", "The book you are trying to edit does not exist.");
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bookstbl book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var foundBook = _LibEFObj.Bookstbls.Find(book.BID);

                    if (foundBook != null)
                    {
                        foundBook.BTitle = book.BTitle;
                        foundBook.Author = book.Author;
                        foundBook.Category = book.Category;
                        foundBook.Copies = book.Copies;
                        foundBook.Publisher = book.Publisher;
                        foundBook.PublishDate = book.PublishDate;

                        _LibEFObj.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("", "The book could not be found for editing.");
                        return View(book);
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the book: " + ex.Message);
                    return View(book);
                }
            }

            return View(book);
        }
    }
}