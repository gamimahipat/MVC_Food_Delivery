using MVC_Food_Delivery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Food_Delivery.Controllers
{
    public class Customer_MasterController : Controller
    {
        private Food_Delivery_DBEntities db = new Food_Delivery_DBEntities();
        // GET: Customer_Master
        public ActionResult EditProfile(int id)
        {
            try
            {

                Customer_Master customer_Master = db.Customer_Master.Find(id);

                Session["pwd"] = customer_Master.CustomerPassword;
                Session["pincode"] = customer_Master.PostalCode;                

                return View(customer_Master);
            }
            catch (Exception)
            {

                ViewBag.emassage = "Customer ID Not Found...";
                return View();
            }
        }
        [HttpPost]
        public ActionResult EditProfile(Customer_Master customer_Master)
        {
            try
            {
                bool exists = true;

                exists = db.Customer_Master.Any(x => x.Email == customer_Master.Email && x.CustomerID != customer_Master.CustomerID);

                if (exists == false)
                {


                    if (Session["pwd"].ToString() == customer_Master.CustomerPassword)
                    {
                        db.Entry(customer_Master).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["ssmessage"] = "Profile Update Succesfully";
                        return RedirectToAction("Index", "ShowProduct");
                    }
                    else
                    {
                        ViewBag.emassage = "Please Enter Correct Password";
                        return View();
                    }
                }
                else
                {
                    ViewBag.emassage = "Email Allredy Exits";
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

                Customer_Master customer_Master = db.Customer_Master.Find(id);
                Session["pwd"] = customer_Master.CustomerPassword;
                return View(customer_Master);
            }
            catch (Exception)
            {

                ViewBag.emassage = "Customer ID Not Found...";
                return View();
            }

        }

        [HttpPost]
        public ActionResult ChangePassword(Customer_Master customer_Master)
        {
            try
            {
                if (Session["pwd"].ToString() == customer_Master.CustomerPassword)
                {
                    if (customer_Master.NewPassword != null)
                    {


                        int row = (from g in db.Customer_Master
                                   where g.CustomerID == customer_Master.CustomerID
                                   select g.CustomerID
                                          ).Take(1).SingleOrDefault();



                        var idd = db.Customer_Master.Find(row);
                        idd.CustomerPassword = customer_Master.NewPassword;


                        db.Entry(idd).State = EntityState.Modified;
                        db.SaveChanges();


                        TempData["ssmessage"] = "Password Update Succesfully";
                        return RedirectToAction("Index", "ShowProduct");
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


    }
}