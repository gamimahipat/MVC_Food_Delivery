using MVC_Food_Delivery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace MVC_Food_Delivery.Controllers
{
    public class ProductMasterController : Controller
    {
        private Food_Delivery_DBEntities db = new Food_Delivery_DBEntities();
        // GET: ProductMaster
        public ActionResult Index()
        {
            try
            {

                var List = db.Product_Master.ToList();
                return View(List);
            }
            catch (Exception)
            {

                TempData["emessage"] = "Product List Not Found...";
                return View();
            }
        }

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Category_Master, "CategoryID", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {

                    string ImageName = System.IO.Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/img/" + ImageName);
                    file.SaveAs(physicalPath);

                    Product_Master product_Master = new Product_Master();
                    product_Master.ProductID = Convert.ToInt32(Request.Form["CategoryID"]);
                    product_Master.CategoryID = Convert.ToInt32(Request.Form["CategoryID"]);
                    product_Master.ProductName = Request.Form["ProductName"];
                    product_Master.ProductDetails = Request.Form["ProductDetails"];
                    product_Master.ProductPrice = Convert.ToInt32(Request.Form["ProductPrice"]);
                    product_Master.ProductImage = ImageName;

                    db.Product_Master.Add(product_Master);
                    db.SaveChanges();
                    TempData["ssmessage"] = "Product Add Succesfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.emessage = "Image Not Upload";
                    ViewBag.CategoryID = new SelectList(db.Category_Master, "CategoryID", "CategoryName");
                    return View();
                }
            }
            catch
            {
                TempData["esmessage"] = "Something Is Wrong...";
                ViewBag.CategoryID = new SelectList(db.Category_Master, "CategoryID", "CategoryName");
                return View();
            }
        }

        // GET: ProductMaster/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {


                // ViewBag.CategoryID = new SelectList(db.Category_Master, "CategoryID", "CategoryName");
                //ViewBag.DefaultBrand = CategoryID.ToString();
                var model = new Product_Master();
                if (id != 0)
                {
                    model = db.Product_Master.Where(x => x.ProductID == id).FirstOrDefault();
                }
                ViewBag.CategoryID = new SelectList(db.Category_Master.ToList(), "CategoryID", "CategoryName", model.CategoryID);
                Session["img"] = model.ProductImage;
                return View(model);
            }
            catch (Exception)
            {

                TempData["emessage"] = "Product ID Not Found...";
                return View();
            }

        }

        // POST: ProductMaster/Edit/5
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, Product_Master product_Master)
        {
            try
            {


                if (file != null)
                {

                    string ImageName = System.IO.Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/img/" + ImageName);
                    file.SaveAs(physicalPath);

                    //Product_Master product_Master = new Product_Master();
                    //var idd = db.Order_Master.Find(Request.Form["CategoryID"]);
                    product_Master.CategoryID = Convert.ToInt32(Request.Form["CategoryID"]);
                    product_Master.ProductName = Request.Form["ProductName"];
                    product_Master.ProductDetails = Request.Form["ProductDetails"];
                    product_Master.ProductPrice = Convert.ToInt32(Request.Form["ProductPrice"]);
                    product_Master.ProductImage = ImageName;

                    db.Entry(product_Master).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ssmessage"] = "Product Add Succesfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    var d = Session["img"].ToString();
                    //FileStream p = new FileStream(d);
                    // file.FileName = Session["img"].ToString();
                    string ImageName = System.IO.Path.GetFileName(d);
                    string physicalPath = Server.MapPath("~/img/" + ImageName);
                    // file.SaveAs(physicalPath);

                    //Product_Master product_Master = new Product_Master();
                    //var idd = db.Order_Master.Find(Request.Form["CategoryID"]);
                    product_Master.CategoryID = Convert.ToInt32(Request.Form["CategoryID"]);
                    product_Master.ProductName = Request.Form["ProductName"];
                    product_Master.ProductDetails = Request.Form["ProductDetails"];
                    product_Master.ProductPrice = Convert.ToInt32(Request.Form["ProductPrice"]);
                    product_Master.ProductImage = ImageName;

                    db.Entry(product_Master).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ssmessage"] = "Product Add Succesfully";
                    return RedirectToAction("Index");

                    //ViewBag.emessage = "Image Not Upload";
                    //ViewBag.CategoryID = new SelectList(db.Category_Master, "CategoryID", "CategoryName");
                    //return View();
                }
            }
            catch
            {
                TempData["esmessage"] = "Something Is Wrong...";
                ViewBag.CategoryID = new SelectList(db.Category_Master.ToList(), "CategoryID", "CategoryName");
                return View();
            }
        }




        // POST: ProductMaster/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Product_Master product_Master = db.Product_Master.Find(id);
                db.Product_Master.Remove(product_Master);
                db.SaveChanges();

                //return RedirectToAction("Index");
                return Json(new { status = "OK" });
            }
            catch
            {
                ViewBag.emassage = "Product Not Deleted";
                return View();
            }
        }
    }
}
