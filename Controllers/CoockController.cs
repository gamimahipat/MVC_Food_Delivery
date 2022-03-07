using Microsoft.Ajax.Utilities;
using MVC_Food_Delivery.Models;
using MVC_Food_Delivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Food_Delivery.Controllers
{
    public class CoockController : Controller
    {
        private Food_Delivery_DBEntities db = new Food_Delivery_DBEntities();
        public ActionResult Index()
        {
            try
            {

                int id = Convert.ToInt32(Session["CoockID"]);

                var list = db.Order_Master.Where(x => x.PaymentStatus == true && (x.CoockStatus == false || x.CoockStatus == null)).ToList();

                var h = list.Count();
                Session["count"] = h;


                List<Order_Master> order_Masters = db.Order_Master.Where(x => x.PaymentStatus == true && x.CoockStatus == false || x.CoockStatus == null).ToList();
                List<Order_Details_Master> order_Details_Masters = db.Order_Details_Master.ToList();
                List<Product_Master> product_Masters = db.Product_Master.ToList();
                List<Category_Master> category_Masters = db.Category_Master.ToList();


                var result = (from om in order_Masters
                              join odm in order_Details_Masters on om.OrderID equals odm.OrderID
                              join pm in product_Masters on odm.ProductID equals pm.ProductID
                              join cm in category_Masters on pm.CategoryID equals cm.CategoryID
                              group odm by new { om, om.OrderID, pm.ProductName, odm.ProductID, pm.CategoryID } into g
                              select new CoockOrderViewModel
                              {
                                  //CategoryIDNew = Convert.ToInt32(g.Key.CategoryID),
                                  //ProductNameNew = String.Join(",", g.Select(x => x.Product_Master.ProductName)),
                                  OrderIDNew = g.Key.OrderID,
                                  ProductIDNew = Convert.ToInt32(g.Key.ProductID),
                                  ProductNameNew = g.Key.ProductName,
                                  CategoryIDNew = Convert.ToInt32(g.Key.CategoryID),
                                  order_Master = g.Key.om

                              }).ToList();

                return View(result);
            }
            catch (Exception)
            {

                TempData["emessage"] = "Customer Order List Not Found... Please Contect Admin.";
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                Order_Master order_Master = db.Order_Master.Find(id);
                Session["ODate"]= order_Master.OrderDate;
                return View(order_Master);
            }
            catch (Exception)
            {
                TempData["emessage"] = "Customer Order ID Not Found... Please Contect Admin.";
                return View();
            }
        }
        [HttpPost]
        public ActionResult Edit(Order_Master order_Master)
        {
            try
            {
                Order_Master order_Master1 = new Order_Master();
                order_Master1.OrderID = order_Master.OrderID;
                order_Master1.OrderDate =Convert.ToDateTime(order_Master.OrderDate);
                order_Master1.CustomerID = order_Master.CustomerID;
                order_Master1.PaymentStatus = order_Master.PaymentStatus;
                order_Master1.CoockStatus = order_Master.CoockStatus;
                order_Master1.DeliveryStatus = order_Master.DeliveryStatus;
                order_Master1.TotalPrice = order_Master.TotalPrice;

                db.Entry(order_Master1).State = EntityState.Modified;
                db.SaveChanges();
                TempData["ssmessage"] = "Recipes Complated";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                TempData["emessage"] = "Recipies Not Complated...";
                return View();
            }
        }

        public ActionResult EditProfile(int id)
        {
            try
            {

                Staff_Details_Master staff_Details_Master = db.Staff_Details_Master.Find(id);

                Session["pwd"] = staff_Details_Master.WorkerPassword;

                return View(staff_Details_Master);
            }
            catch (Exception)
            {

                TempData["emessage"] = "Coock ID Not Found...";
                return View();
            }
        }
        [HttpPost]
        public ActionResult EditProfile(Staff_Details_Master staff_Details_Master)
        {
            try
            {
                if (Session["pwd"].ToString() == staff_Details_Master.WorkerPassword)
                {
                    db.Entry(staff_Details_Master).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ssmessage"] = "Profile Update Succesfully";
                    return RedirectToAction("Index", "Coock");
                }
                else
                {
                    ViewBag.emassage = "Please Enter Correct Password";
                    return View();
                }

            }
            catch (Exception)
            {

                ViewBag.emassage = "Profile Not Udated";
                return View();
            }
        }

        public ActionResult ChangePassword(int id)
        {
            try
            {


                Staff_Details_Master staff_Details_Master = db.Staff_Details_Master.Find(id);
                Session["pwd"] = staff_Details_Master.WorkerPassword;
                return View(staff_Details_Master);
            }
            catch (Exception)
            {
                ViewBag.emassage = "Coock ID Not Found...";
                return View();
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(Staff_Details_Master staff_Details_Master)
        {
            try
            {
                if (Session["pwd"].ToString() == staff_Details_Master.WorkerPassword)
                {
                    if (staff_Details_Master.NewPassword != null)
                    {


                        int row = (from g in db.Staff_Details_Master
                                   where g.WorkerTypeID == staff_Details_Master.WorkerTypeID
                                   select g.WorkerTypeID
                                          ).Take(1).SingleOrDefault();



                        var idd = db.Staff_Details_Master.Find(row);
                        idd.WorkerPassword = staff_Details_Master.NewPassword;


                        db.Entry(idd).State = EntityState.Modified;
                        db.SaveChanges();


                        TempData["ssmessage"] = "Password Update Succesfully";
                        return RedirectToAction("Index", "Coock");
                    }
                    else
                    {
                        ViewBag.emassage = "Please Enter Your New Password";
                        return View();
                    }
                }
                else
                {
                    ViewBag.emassage = "Old Password not Correct";
                    return View();
                }

            }
            catch (Exception)
            {

                ViewBag.emassage = "Password Not Udated";
                return View();
            }
        }

        public ActionResult CooclList()
        {
            try
            {
                var List = db.Staff_Details_Master.Where(x => x.UserID == 2).ToList();
                return View(List);
            }
            catch (Exception)
            {
                TempData["emessage"] = "Coock List Not Found...";
                return View();
            }
        }

        public ActionResult CoockRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CoockRegister(Staff_Details_Master staff_Details_Master)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Staff_Details_Master.Add(staff_Details_Master);
                    db.SaveChanges();
                    TempData["ssmessage"] = "Coock Register Succesfully";
                    return RedirectToAction("CooclList");
                }
                else
                {
                    ViewBag.emassage = "Coock Not Register..";
                    return View();
                }
            }
            catch (Exception)
            {

                ViewBag.emassage = "Coock Not Register..";
                return View();
            }
        }
   
    }
}