using MVC_Food_Delivery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Food_Delivery.Controllers
{
    public class Category_MasterController : Controller
    {
        private Food_Delivery_DBEntities db = new Food_Delivery_DBEntities();
        // GET: Category_Master
        public ActionResult Index()
        {
            try
            {

                var List = db.Category_Master.ToList();
                return View(List);
            }
            catch (Exception)
            {

                TempData["emessage"] = "Category List Not Found...";
                return View();
            }
        }


        // GET: Category_Master/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category_Master/Create
        [HttpPost]
        public ActionResult Create(Category_Master category_Master)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (category_Master.CategoryName != null)
                    {

                        var exists = db.Category_Master.Any(x => x.CategoryName == category_Master.CategoryName);

                        if (exists)
                        {
                            ViewBag.emassage = "Category Allredy Exits..";
                            return View();
                        }
                        else
                        {
                            db.Category_Master.Add(category_Master);
                            db.SaveChanges();
                            TempData["ssmessage"] = "Category Add Succesfully";
                            return RedirectToAction("Index");
                        }

                    }
                    return View();
                }

                else
                {
                    // ViewBag.emassage = "Category Name Allredy Exits..";
                    return View();
                }
            }
            catch (NullReferenceException)
            {
                ViewBag.emassage = "Category Name Allredy Exits..";
                return View();
            }
        }

        // GET: Category_Master/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {

                Category_Master category_Master = db.Category_Master.Find(id);
                return View(category_Master);
            }
            catch (Exception)
            {

                TempData["emessage"] = "Category ID Not Found...";
                return View();
            }
        }

        private bool IfExits(string CategoryName, int? CategoryID = null)
        {
            bool exists = true;
            if (CategoryID != null)
            {
                exists = db.Category_Master.Any(x => x.CategoryName == CategoryName && x.CategoryID != CategoryID);
            }
            else
            {
                exists = db.Category_Master.Any(x => x.CategoryName == CategoryName && x.CategoryID == CategoryID);
            }
            return exists;
        }

        // POST: Category_Master/Edit/5
        [HttpPost]
        public ActionResult Edit(Category_Master category_Master)
        {
            try
            {
                //var exists = db.Category_Master.Any(x => x.CategoryName == category_Master.CategoryName);

                if (IfExits(category_Master.CategoryName, category_Master.CategoryID))
                {
                    ViewBag.editmassage = "Category Allredy Exits..";
                    return View();
                }
                else
                {
                    db.Entry(category_Master).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ssmessage"] = "Category Update Succesfully";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ViewBag.emassage = "Category Not Udated";
                return View();
            }
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Category_Master category_Master = db.Category_Master.Find(id);
                db.Category_Master.Remove(category_Master);
                db.SaveChanges();

                //return RedirectToAction("Index");
                return Json(new { status = "OK" });
            }
            catch
            {
                ViewBag.emassage = "Category Not Deleted";
                return View();
            }
        }
    }
}
