using MVC_Food_Delivery.Models;
using MVC_Food_Delivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MVC_Food_Delivery.Controllers
{
    public class LoginController : Controller
    {
        private Food_Delivery_DBEntities db = new Food_Delivery_DBEntities();
        // GET: Login

        public ActionResult Signin()
        {
            try
            {
                Session["Username"] = "l";

                ViewBag.UserID = new SelectList(db.Staff_Manage_Master, "UserID", "UserTypeName");

                return View();
            }
            catch (Exception)
            {

                ViewBag.emassage = "Logintype ID Not Found...";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Signin(LogintypeViewModel loginViewModel)
        {
            try
            {
                if (loginViewModel.Email != null || loginViewModel.Password != null)
                {
                    if (loginViewModel.Email != null)
                    {
                        if (loginViewModel.Password != null)
                        {
                            if (loginViewModel.UserID == 1)
                            {
                                var CheckEmail = db.Staff_Details_Master.Where(m => m.WorkerEmail == loginViewModel.Email).FirstOrDefault();
                                if (CheckEmail != null)
                                {

                                    var users = db.Staff_Details_Master.Where(m => m.WorkerEmail == loginViewModel.Email && m.WorkerPassword == loginViewModel.Password && m.UserID == 1).FirstOrDefault();
                                    if (users != null)
                                    {
                                        TempData["smessage"] = "Login Successful";
                                        Session["id"] = users.WorkerTypeID;
                                        Session["AdminName"] = users.WorkerName;
                                        Session["AdminEmail"] = users.WorkerEmail;
                                        Session["Types"] = Convert.ToInt32(loginViewModel.UserID);
                                        return RedirectToAction("Index", "Admin");
                                    }
                                    else
                                    {
                                        TempData["emessage"] = "Login failed: Invalid username or password ...";
                                        return RedirectToAction("Signin", "Login");
                                    }
                                }
                                else
                                {
                                    TempData["emessage"] = "Your Email Not Register...";
                                    return RedirectToAction("Signin", "Login");
                                }

                            }
                            else if (loginViewModel.UserID == 2)
                            {
                                var CheckEmail = db.Staff_Details_Master.Where(m => m.WorkerEmail == loginViewModel.Email).FirstOrDefault();
                                if (CheckEmail != null)
                                {
                                    var users = db.Staff_Details_Master.Where(m => m.WorkerEmail == loginViewModel.Email && m.WorkerPassword == loginViewModel.Password && m.UserID == 2).FirstOrDefault();
                                    if (users != null)
                                    {
                                        TempData["smessage"] = "Login Successful";
                                        Session["CoockID"] = users.WorkerTypeID;
                                        Session["Userid"] = users.WorkerTypeID;
                                        Session["Username"] = users.WorkerName;
                                        Session["Types"] = Convert.ToInt32(loginViewModel.UserID);
                                        return RedirectToAction("Index", "Coock");
                                    }
                                    else
                                    {
                                        TempData["emessage"] = "Login failed: Invalid username or password ...";
                                        return RedirectToAction("Signin", "Login");
                                    }
                                }
                                else
                                {
                                    TempData["emessage"] = "Your Email Not Register...";
                                    return RedirectToAction("Signin", "Login");
                                }
                            }
                            else if (loginViewModel.UserID == 3)
                            {
                                var CheckEmail = db.Staff_Details_Master.Where(m => m.WorkerEmail == loginViewModel.Email).FirstOrDefault();
                                if (CheckEmail != null)
                                {
                                    var users = db.Staff_Details_Master.Where(m => m.WorkerEmail == loginViewModel.Email && m.WorkerPassword == loginViewModel.Password && m.UserID == 3).FirstOrDefault();
                                    if (users != null)
                                    {
                                        TempData["smessage"] = "Login Successful";
                                        Session["Username"] = users.WorkerName;
                                        Session["Userid"] = users.WorkerTypeID;
                                        Session["DeliveryID"] = users.WorkerTypeID;
                                        Session["Types"] = Convert.ToInt32(loginViewModel.UserID);
                                        return RedirectToAction("Index", "Delivery");
                                    }
                                    else
                                    {
                                        TempData["emessage"] = "Login failed: Invalid username or password ...";
                                        return RedirectToAction("Signin", "Login");
                                    }
                                }
                                else
                                {
                                    TempData["emessage"] = "Your Email Not Register...";
                                    return RedirectToAction("Signin", "Login");
                                }
                            }
                            else if (loginViewModel.UserID == 4)
                            {

                                //var user = db.Customer_Master.Where(m => m.Email == customer_Master.Email && m.CustomerPassword == customer_Master.CustomerPassword).FirstOrDefault();
                                var CheckEmail = db.Customer_Master.Where(m => m.Email == loginViewModel.Email).FirstOrDefault();
                                if (CheckEmail != null)
                                {
                                    var CheckPwd = db.Customer_Master.Where(m => m.CustomerPassword == loginViewModel.Password).FirstOrDefault();
                                    if (CheckPwd != null)
                                    {

                                        var user = db.Customer_Master.Where(m => m.Email == loginViewModel.Email && m.CustomerPassword == loginViewModel.Password).FirstOrDefault();


                                        if (user != null)
                                        {
                                           
                                            if (user.ISActive==true)
                                            {



                                                int q = user.CustomerID;

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




                                                TempData["smessage"] = "Login Successful";
                                                Session["CustomerID"] = user.CustomerID;
                                                Session["Username"] = user.CustomerName;
                                                Session["Number"] = user.MobileNumber;
                                                Session["Email"] = user.Email;
                                                Session["City"] = user.City;
                                                Session["HouseNumber"] = user.HouseNumber;
                                                Session["Street"] = user.Street;
                                                Session["PostalCode"] = user.PostalCode;
                                                Session["ISActive"] = user.ISActive;
                                                // Session["Address"] = user.CustomerAddress;
                                                Session["Userid"] = user.CustomerID;
                                                Session["Types"] = Convert.ToInt32(loginViewModel.UserID);

                                                return RedirectToAction("Index", "ShowProduct");
                                            }
                                            else
                                            {
                                                TempData["emessage"] = "Sorry Your Account is Locked";
                                                return RedirectToAction("Signin", "Login");
                                            }
                                            }
                                        else
                                        {
                                            TempData["emessage"] = "Login failed: Invalid Email";
                                            return RedirectToAction("Signin", "Login");
                                        }
                                    }
                                    else
                                    {
                                        TempData["emessage"] = "Login failed: Invalid Password";
                                        return RedirectToAction("Signin", "Login");
                                    }
                                }

                                else
                                {
                                    TempData["emessage"] = "Your Email Not Register...";
                                    return RedirectToAction("Signin", "Login");
                                }

                            }
                            else
                            {
                                TempData["emessage"] = "Invalid Login Type ID...";
                                return View();
                            }
                        }
                        else
                        {
                            TempData["emessage"] = "Please Enter Your Password";
                            return RedirectToAction("Signin", "Login");
                        }
                    }
                    else
                    {
                        TempData["emessage"] = "Please Enter Your Email";
                        return RedirectToAction("Signin", "Login");

                    }
                }
                else
                {
                    TempData["emessage"] = "Please Enter Email And Password";
                    return RedirectToAction("Signin", "Login");
                }

            }
            catch (Exception)
            {
                TempData["emessage"] = "Something is Wrong...";
                return RedirectToAction("Login");
            }
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(Customer_Master customer_Master)
        {
            try
            {

                if (customer_Master.PostalCode != null)
                {


                    if (ModelState.IsValid == true)
                    {


                        var exists = db.Customer_Master.Any(x => x.Email == customer_Master.Email);

                        if (exists)
                        {
                            TempData["inserterr"] = "Your Email Allredy Register...";
                            return View();
                        }
                        else
                        {
                            db.Customer_Master.Add(customer_Master);
                            int s = db.SaveChanges();

                            if (s > 0)
                            {
                                TempData["insert"] = "Register Successfully";
                                return RedirectToAction("Signin", "Login");
                            }
                            else
                            {
                                TempData["inserterr"] = " somthing is rong plase try agin";
                                return View();

                            }
                        }

                    }
                    else
                    {
                        TempData["inserterr"] = " somthing is rong plase try agin";
                        return View();
                    }
                }
                else
                {
                    return View();
                }

            }
            catch (Exception)
            {

                TempData["inserterr"] = "somthing is rong plase try agin'";
                return View();
            }
        }


        public ActionResult EnterEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmailCheck(Customer_Master customer_Master)
        {
            try
            {


                var exists = db.Customer_Master.Any(x => x.Email == customer_Master.Email);

                if (exists)
                {
                    var row = db.Customer_Master.Where(x => x.Email == customer_Master.Email).FirstOrDefault();
                    int id = row.CustomerID;
                    Session["Cid"] = id;
                    Random generator = new Random();
                    String randomNumber = generator.Next(0, 1000000).ToString("D6");
                    Session["otp"] = randomNumber;

                    MailModel mailModel = new MailModel();
                    mailModel.To = customer_Master.Email;
                    mailModel.From = "gamimahipat888@gmail.com";
                    mailModel.Subject = "Your Email OTP Forget Password";

                    mailModel.Body = "Hello," + "<br /><br /> Your One Time Password (OTP) to Forget Password is <b>"
                        + randomNumber +
                        "</b>. The OTP is valid for 30 minutes. For account safety, do not share your OTP with others.";


                    MailMessage mail = new MailMessage();
                    mail.To.Add(mailModel.To);    //customere Email
                    mail.From = new MailAddress(mailModel.From);   //admin Email
                    mail.Subject = mailModel.Subject;
                    string Body = mailModel.Body;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("gamimahipat888@gmail.com", "Gami@123");
                    smtp.EnableSsl = true;

                    smtp.Send(mail);

                    TempData["insert"] = "Your Email Send OTP";
                    return RedirectToAction("EnterOTP", "Login");
                }
                else
                {
                    TempData["inserterr"] = "Your Email Not Register... Please Enter valid Email";
                    return RedirectToAction("EnterEmail", "Login");
                }
            }
            catch (Exception)
            {

                TempData["inserterr"] = "Your Email Not Register... Please Enter valid Email";
                return RedirectToAction("EnterEmail", "Login");
            }

        }

        public ActionResult EnterOTP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnterOTP(OTPViewModel oTPViewModel)
        {
            try
            {
                if (oTPViewModel.otp == Session["otp"].ToString())
                {
                    TempData["insert"] = "Your OTP Mathced Suucessfully";
                    return RedirectToAction("CreatePassword", "Login");
                }
                else
                {
                    TempData["inserterr"] = "OTP Not Matched...";
                    return View();
                }

            }
            catch (Exception)
            {

                TempData["inserterr"] = "OTP Not Matched...";
                return View();
            }

        }

        public ActionResult CreatePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePassword(Customer_Master customer_Master)
        {
            try
            {
                if (customer_Master.NewPassword != null)
                {

                    int id = Convert.ToInt32(Session["Cid"]);

                    var idd = db.Customer_Master.Find(id);
                    idd.CustomerPassword = customer_Master.NewPassword;

                    db.Entry(idd).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["insert"] = "Password Change Successfully";
                    return RedirectToAction("Signin", "Login");
                }
                else
                {
                    TempData["inserterr"] = "Please Enter Password";
                    return View();
                }
            }
            catch (Exception)
            {

                TempData["inserterr"] = "Password Not Save...";
                return View();
            }

        }




    }
}