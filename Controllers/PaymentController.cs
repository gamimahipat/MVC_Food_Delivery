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
    public class PaymentController : Controller
    {
        private Food_Delivery_DBEntities db = new Food_Delivery_DBEntities();
        // GET: Payment
        public ActionResult Index()
        {
            int userid = Convert.ToInt32(Session["Userid"]);
            Session["CustomerID"] = userid;
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder()
        {
            try
            {


                int Price = Convert.ToInt32(Session["price"]);
                var Name = Session["Username"].ToString();
                var Number = Session["Number"].ToString();
                var Email = Session["Email"].ToString();
                var Address = Session["HouseNumber"].ToString()
                    + Session["Street"].ToString()
                    +Session["City"].ToString()+ Session["PostalCode"].ToString();
                 
                // Generate random receipt number for order
                Random randomObj = new Random();
                string transactionId = randomObj.Next(10000000, 100000000).ToString();

                Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_sQV2hKW4KLnyKp", "hxXnkq3xPstZi0PVfhNfKXgH");
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", Price * 100);  // Amount will in paise
                options.Add("receipt", transactionId);
                options.Add("currency", "INR");
                options.Add("payment_capture", "0"); // 1 - automatic  , 2 - manual
                                                     //options.Add("notes", "-- You can put any notes here --");
                Razorpay.Api.Order orderResponse = client.Order.Create(options);
                string orderId = orderResponse["id"].ToString();

                // Create order model for return on view
                OrderModel orderModel = new OrderModel
                {
                    orderId = orderResponse.Attributes["id"],
                    razorpayKey = "rzp_test_sQV2hKW4KLnyKp",
                    amount = Price * 100,
                    currency = "INR",
                    name = Name,
                    email = Email,
                    contactNumber = Number,
                    address = Address,
                    description = "Testing description"
                };
                Session["amount"] = orderModel.amount;
                Session["Amountss"] = orderModel.amount/100;
                // Return on PaymentPage with Order data
                return View("PaymentPage", orderModel);
            }
            catch (Exception)
            {

                TempData["emessage"] = "Your Order Not Created...";
                return View();
            }
        }

        public class OrderModel
        {
            public string orderId { get; set; }
            public string razorpayKey { get; set; }
            public int amount { get; set; }
            public string currency { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string contactNumber { get; set; }
            public string address { get; set; }
            public string description { get; set; }

        }

        [HttpPost]
        public ActionResult Complete()
        {
            try
            {


                // Payment data comes in url so we have to get it from url

                // This id is razorpay unique payment id which can be use to get the payment details from razorpay server
                string paymentId = Request.Params["rzp_paymentid"];

                // This is orderId
                string orderId = Request.Params["rzp_orderid"];

                // Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("-- Razorpay key --", "-- Razorpay secret --");
                Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_sQV2hKW4KLnyKp", "hxXnkq3xPstZi0PVfhNfKXgH");

                Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);


                // This code is for capture the payment 

                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", payment.Attributes["amount"]);
                Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
                string amt = paymentCaptured.Attributes["amount"];

                //// Check payment made successfully

                if (paymentCaptured.Attributes["status"] == "captured")
                {
                    // Create these action method
                    return RedirectToAction("Success");
                }
                else
                {
                    return RedirectToAction("Failed");
                }
            }
            catch (Exception)
            {

                TempData["emessage"] = "Your Payment Not Success...";
                return View();
            }
        }

        public ActionResult Success()
        {
            // Dictionary<String, String> items = new Dictionary<String, String>();
            List<string> OrderProductList = new List<string>();

            int Userid = Convert.ToInt32(Session["Userid"]);


            List<Product_Master> product_Masters = db.Product_Master.ToList();
            List<Add_To_Cart_Master> add_To_Cart_Masters = db.Add_To_Cart_Master.ToList();


            var Record = from e in product_Masters
                         join d in add_To_Cart_Masters on e.ProductID equals d.ProductID
                         where d.CustomerID == Userid

                         select new CartViewModel
                         {
                             Product_Master = e,
                             Add_To_Cart_Master = d,

                         };


            



            Order_Master order_Master = new Order_Master();
            order_Master.CustomerID = Userid;
            order_Master.TotalPrice =Convert.ToDouble(Session["Amountss"]);
            order_Master.OrderDate =DateTime.Now;
            order_Master.PaymentStatus = true;
            order_Master.CoockStatus = false;
            order_Master.DeliveryStatus = false;
            Session["CustomerID"] = order_Master.CustomerID;
            db.Order_Master.Add(order_Master);
            db.SaveChanges();


            int OrderList = (
   from p in db.Order_Master
   where p.CustomerID == Userid
   orderby p.OrderID descending
   select p.OrderID
).Take(1).SingleOrDefault();


            Payment_Master payment_Master = new Payment_Master();
            payment_Master.OrderID = OrderList;
            payment_Master.PaymentDate = DateTime.Now;
            payment_Master.PaymentType = "Bank";
            payment_Master.TotalPrice = Convert.ToInt32(Session["amount"]) / 100;
            Session["TotalPayment"] = payment_Master.TotalPrice;
            db.Payment_Master.Add(payment_Master);
            db.SaveChanges();
            //var OrderList = db.Order_Master.Where(x => x.CustomerID == Userid).Select(x=>x.OrderID).LastOrDefault();
            // var MyRecentRecord = db.Order_Master.Where(a => a.CustomerID== Userid).Select(x => x.OrderID).OrderByDescending(b => b.or).FirstOrDefault(); //This will retrieve the most recent data node from the database.

            Order_Details_Master order_Details_Master = new Order_Details_Master();

            foreach (var item in Record)
            {
                OrderProductList.Add(item.Product_Master.ProductName);

                // order_Details_Master.Order_Details_ID = 0;
                order_Details_Master.ProductID = item.Product_Master.ProductID;
                order_Details_Master.OrderID = OrderList;
                order_Details_Master.Quantity = item.Add_To_Cart_Master.Quantity;
                order_Details_Master.TotalAmount = Convert.ToInt32(item.Product_Master.ProductPrice * item.Add_To_Cart_Master.Quantity);

                db.Order_Details_Master.Add(order_Details_Master);
                db.SaveChanges();
            }

            string[] Productss = OrderProductList.ToArray();

            Add_To_Cart_Master add_To_Cart_Master1 = new Add_To_Cart_Master();

            foreach (var item in Record)
            {
                // order_Details_Master.Order_Details_ID = 0;
                var d = db.Add_To_Cart_Master.Find(item.Add_To_Cart_Master.AddToCartID);
                db.Add_To_Cart_Master.Remove(d);
                db.SaveChanges();


            }
            Session["count"] = 0;

            try
            {


                Uri uri = new Uri("https://www.promptsoftech.com/");

                MailModel mailModel = new MailModel();
                mailModel.To = Session["Email"].ToString();
                mailModel.From = "gamimahipat888@gmail.com";
                mailModel.Subject = "Prompt Softech";

                var sb = new System.Text.StringBuilder();
                for (int index = 0; index < Productss.Length; index++)
                    sb.Append(String.Format(Productss[index] + ", "));



                mailModel.Body = String.Format("Hello, " + Session["Username"].ToString() + "<br /><br />"
                    + "   Thank you for your order confirmed that  we got your ₹"
                    + Session["TotalPayment"].ToString() + " payment. <br /> "
                + "Your Product is " + sb + ".  Here is more information your order." + uri + " Food Delivery");



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
                TempData["smessage"] = "Email Send Succesfully";
            }
            catch (Exception)
            {

                TempData["emessage"] = "Email Not Send...";
            }


            return View();
        }

        public ActionResult Failed()
        {
            return View();
        }

        public ActionResult Invoice()
        {

            try
            {



                int Userid = Convert.ToInt32(Session["Userid"]);

                int orderid = (from p in db.Order_Master
                               orderby p.OrderID descending
                               select p.OrderID).Take(1).SingleOrDefault();



                List<Order_Master> order_Masters = db.Order_Master.ToList();
                List<Order_Details_Master> order_Details_Masters = db.Order_Details_Master.ToList();
                List<Customer_Master> customer_Masters = db.Customer_Master.ToList();
                List<Product_Master> product_Masters = db.Product_Master.ToList();
                List<Category_Master> category_Masters = db.Category_Master.ToList();
                List<Payment_Master> payment_Masters = db.Payment_Master.ToList();

                var InvoiceList = from om in order_Masters
                                  join odm in order_Details_Masters on om.OrderID equals odm.OrderID
                                  join cm in customer_Masters on om.CustomerID equals cm.CustomerID
                                  join pm in product_Masters on odm.ProductID equals pm.ProductID
                                  join km in category_Masters on pm.CategoryID equals km.CategoryID
                                  join py in payment_Masters on om.OrderID equals py.OrderID
                                  where om.OrderID == orderid

                                  select new InvoiceViewModel
                                  {
                                      order_Master = om,
                                      order_Details_Master = odm,
                                      product_Master = pm,
                                      customer_Master = cm,
                                      category_Master = km,
                                      payment_Master = py
                                  };

                foreach (var item in InvoiceList)
                {
                    

                    Session["CustomerID"] = item.customer_Master.CustomerID;
                    Session["CustomerName"] = item.customer_Master.CustomerName;
                    Session["HouseNumber"] = item.customer_Master.HouseNumber;
                    Session["Street"] = item.customer_Master.Street;
                    Session["City"] = item.customer_Master.City;
                    Session["PostalCode"] = item.customer_Master.PostalCode;
                    Session["OrderID"] = item.order_Master.OrderID;
                    Session["OrderDate"] = item.order_Master.OrderDate.Value.ToShortDateString(); ;
                    Session["OrderTotalPrice"] = item.order_Master.TotalPrice;
                    Session["paymentdate"] = item.payment_Master.PaymentDate.Value.ToShortDateString(); ;
                }





                return View(InvoiceList);
            }
            catch (Exception)
            {

                TempData["emessage"] = "Your Invoise Not Cretaed...";
                return View();
            }
        }


    }


}