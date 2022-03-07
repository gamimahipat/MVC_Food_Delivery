using MVC_Food_Delivery.Models;
using MVC_Food_Delivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;

namespace MVC_Food_Delivery.Controllers
{

    public class ShowProductController : Controller
    {
        private Food_Delivery_DBEntities db = new Food_Delivery_DBEntities();

        // GET: ShowProduct
        public ActionResult Index()
        {
            try
            {

                int q = Convert.ToInt32(Session["Userid"]);


                List<Product_Master> product_Masters = db.Product_Master.ToList();
                List<Add_To_Cart_Master> add_To_Cart_Masters = db.Add_To_Cart_Master.ToList();


                var employeeRecord = from e in product_Masters
                                     join d in add_To_Cart_Masters on e.ProductID equals d.ProductID
                                     where d.CustomerID == q

                                     select new CartViewModel
                                     {
                                         Product_Master = e,
                                         Add_To_Cart_Master = d,

                                     };


                var h = employeeRecord.Count();
                Session["count"] = h;

                var p = db.Product_Master.ToList();
                return View(p);
            }
            catch (Exception)
            {

                TempData["emessage"] = "Products Not Found...";
                return View();
            }
        }

        public ActionResult ProductDetails(int id)
        {
            try
            {


                Product_Master product_Master = db.Product_Master.Find(id);
                return View(product_Master);
            }
            catch (Exception)
            {

                TempData["emessage"] = "Products Details Not Found...";
                return View();
            }
        }

        [HttpPost]
        public ActionResult CartProducts(Add_To_Cart_Master add_To_Cart_Master)
        {
            try
            {

                int q = Convert.ToInt32(Session["Userid"]);

                //var p = db.Add_To_Cart_Master.Where(x => x.CustomerID == q).ToList();


                List<Product_Master> product_Masters = db.Product_Master.ToList();
                List<Add_To_Cart_Master> add_To_Cart_Masters = db.Add_To_Cart_Master.ToList();


                var employeeRecord = from e in product_Masters
                                     join d in add_To_Cart_Masters on e.ProductID equals d.ProductID
                                     where d.CustomerID == q

                                     select new CartViewModel
                                     {
                                         Product_Master = e,
                                         Add_To_Cart_Master = d,

                                     };


                var h = employeeRecord.Count();
                Session["count"] = h;

                Add_To_Cart_Master model = new Add_To_Cart_Master();

                model.CustomerID = Convert.ToInt32(Session["Userid"]);
                model.ProductID = add_To_Cart_Master.ProductID;
                model.Quantity = 1;

                db.Add_To_Cart_Master.Add(model);
                db.SaveChanges();


                return Json(new { status = "OK" });

            }
            catch (Exception)
            {

                TempData["nasserte"] = "Somthing is rong plase try agin";
                return View();
            }
        }


        [HttpPost]
        public ActionResult QuantityAdd(Add_To_Cart_Master add_To_Cart_Master)
        {
            try
            {

                var idd = db.Add_To_Cart_Master.Find(add_To_Cart_Master.AddToCartID);
                idd.Quantity = add_To_Cart_Master.Quantity;


                db.Entry(idd).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { status = "OK" });
            }
            catch (Exception)
            {
                TempData["nasserte"] = "Qty Not Add...";
                return View();
            }
        }

    }
}