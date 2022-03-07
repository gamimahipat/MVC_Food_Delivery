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
    public class DeliveryController : Controller
    {
        private Food_Delivery_DBEntities db = new Food_Delivery_DBEntities();

        // GET: Delivery
        public ActionResult Index()
        {
            try
            {

                var list = db.Order_Master.Where(x => x.PaymentStatus == true &&
                x.CoockStatus == true &&
                (x.DeliveryStatus == false || x.DeliveryStatus == null)).ToList();

                var h = list.Count();
                Session["count"] = h;
                return View(list);
            }
            catch (Exception)
            {

                ViewBag.emassage = "Customer Order List Not Found...";
                return View();
            }

        }

        public ActionResult Location(int id)
        {
            try
            {


                var OrderList = db.Order_Master.Find(id);

                var customerDetails = db.Customer_Master.Where(x => x.CustomerID == OrderList.CustomerID).SingleOrDefault();

                //var CustomerAddress = customerDetails.CustomerAddress;

                //Session["CustomerAddrsss"] = customerDetails.CustomerAddress;

                Dictionary<String, String> items = new Dictionary<String, String>();

                //if (CustomerAddress == "Science City")
                //{
                //    //items.Add("23.07504", "72.51116"); //Prompt lat log
                //    items.Add("23.08020", "72.49525"); // science city lat log Customer address            
                //}
                //if (CustomerAddress == "Sola Hospital")
                //{
                //    //items.Add("23.07504", "72.51116"); //Prompt lat log
                //    items.Add("23.07036", "72.52258"); // Sola Hospital lat log Customer address            
                //}
                //if (CustomerAddress == "Shivam Boys PG")
                //{
                //    //items.Add("23.07504", "72.51116"); //Prompt lat log
                //    items.Add("23.06680", "72.72774");  //Shivam Boys PG lat log Customer address
                //}
                //if (CustomerAddress == "AUDA Garden")
                //{
                //    // items.Add("23.07504", "72.51116"); //Prompt lat log
                //    items.Add("23.06813", "72.51564"); // AUDA Garden PG lat log Customer address
                //}

                Locations locations = new Locations();
                locations.Latitude = Convert.ToDecimal(items.ElementAt(0).Key);
                locations.Longitude = Convert.ToDecimal(items.ElementAt(0).Value);



                L l = new L();
                l.locations = locations;

                l.id = id;

                Session["Latitude"] = l.locations.Latitude;
                Session["Longitude"] = l.locations.Longitude;

                return View(OrderList);
            }
            catch (Exception)
            {

                ViewBag.emassage = "Customer ID Not Found...";
                return View();
            }
        }


        [HttpPost]
        public ActionResult Location(Order_Master order_Master)
        {
            try
            {

                order_Master.DeliveryStatus = true;
                db.Entry(order_Master).State = EntityState.Modified;
                db.SaveChanges();
                TempData["ssmessage"] = "Delivery Succesfully";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["emessage"] = "Something is Wrong...";
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

                TempData["emessage"] = "Profile Not Update";
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
                    return RedirectToAction("Index", "Delivery");
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
                        return RedirectToAction("Index", "Delivery");
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


        public ActionResult DeliveryList()
        {
            try
            {
                var List = db.Staff_Details_Master.Where(x => x.UserID == 3).ToList();
                return View(List);
            }
            catch (Exception)
            {
                TempData["emessage"] = "Delivery List Not Found...";
                return View();
            }
        }

        public ActionResult DeliveryRegister()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult DeliveryRegister(Staff_Details_Master staff_Details_Master)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Staff_Details_Master.Add(staff_Details_Master);
                    db.SaveChanges();
                    TempData["ssmessage"] = "Delivery Register Succesfully";
                    return RedirectToAction("DeliveryList");
                }
                else
                {
                    ViewBag.emassage = "Delivery Not Register..";
                    return View();
                }
            }
            catch (Exception)
            {

                ViewBag.emassage = "Delivery Not Register..";
                return View();
            }
        }

       
        public ActionResult DeliverySuccess(int id)
        {
            try
            {
                var idd = db.Order_Master.Find(id);
                idd.DeliveryStatus = true;
                //order_Master.DeliveryStatus = true;
                db.Entry(idd).State = EntityState.Modified;
                db.SaveChanges();
                TempData["ssmessage"] = "Delivery Succesfully";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["emessage"] = "Something is Wrong...";
                return View();
            }
        }

    }
}