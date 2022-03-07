using MVC_Food_Delivery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Food_Delivery.Controllers
{
    public class AdminController : Controller
    {
        private Food_Delivery_DBEntities db = new Food_Delivery_DBEntities();
        // GET: Admin
        public ActionResult Index()
        {
            try
            {

                var ordercount = db.Order_Master.ToList().Count();
                Session["ordercount"] = ordercount;

                var ordertotalprice = (from emp in db.Order_Master

                                       select emp.TotalPrice).Sum();

                Session["totalprice"] = ordertotalprice;

                var text = ordertotalprice * 20 / 100;
                Session["ordertotalprice"] = text;

                var revanustatus = ordertotalprice - text;
                Session["revenuestatus"] = revanustatus;

                var customercount = db.Customer_Master.ToList().Count();
                Session["customercount"] = customercount;

                var productcount = db.Product_Master.ToList().Count();
                Session["productcount"] = productcount;

                var categorycount = db.Category_Master.ToList().Count();
                Session["categorycount"] = categorycount;

                var paymentcount = db.Payment_Master.ToList().Count();
                Session["paymentcount"] = paymentcount;

                var coockcount = db.Staff_Details_Master.Where(x=>x.UserID==2).ToList().Count();
                Session["coockcount"] = coockcount;

                var deliverykcount = db.Staff_Details_Master.Where(x => x.UserID == 3).ToList().Count();
                Session["deliverykcount"] = deliverykcount;

                return View();
            }
            catch (Exception)
            {
                TempData["emessage"] = "Not Count Details...";
                return View();

            }
        }

        public ActionResult OrderView()
        {
            try
            {

                var orderList = db.Order_Master.ToList();

                return View(orderList);
            }
            catch (Exception)
            {

                TempData["emessage"] = "Order List Not Found...";
                return View();
            }
        }
       
        public ActionResult CustomerList()
        {
            try
            {

                var customerlist = db.Customer_Master.ToList();

                return View(customerlist);
            }
            catch (Exception)
            {

                TempData["emessage"] = "Customer List Not Found...";
                return View();
            }
        }

        public ActionResult PaymentList()
        {
            try
            {

                var paymentlist = db.Payment_Master.ToList();

                return View(paymentlist);
            }
            catch (Exception)
            {

                TempData["emessage"] = "Payment List Not Found...";
                return View();
            }
        }

        [HttpGet]
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

                TempData["emessage"] = "ID Not Found...";
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
                    return RedirectToAction("Index");
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

                TempData["emessage"] = "ID Not Found...";
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
                        return RedirectToAction("Index");
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

        public ActionResult UserActiveInactive(int id)
        {
            try
            {
                var idd = db.Customer_Master.Find(id);
                if (idd.ISActive == true)
                {
                    idd.ISActive = false;
                    db.Entry(idd).State = EntityState.Modified;
                    db.SaveChanges();
                    //TempData["ssmessage"] = "User Inactive";
                    return RedirectToAction("CustomerList");
                }
                else
                {
                    idd.ISActive = true;
                    //order_Master.DeliveryStatus = true;
                    db.Entry(idd).State = EntityState.Modified;
                    db.SaveChanges();
                    //TempData["ssmessage"] = "User Active";
                    return RedirectToAction("CustomerList");
                }
               
            }
            catch (Exception)
            {

                ViewBag.emassage = "Somethig is wrong";
                return View("CustomerList");
            }
        }
    }
}