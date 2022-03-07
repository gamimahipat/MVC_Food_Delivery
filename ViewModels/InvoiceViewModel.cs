using MVC_Food_Delivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Food_Delivery.ViewModels
{
    public class InvoiceViewModel
    {
        public Customer_Master customer_Master { get; set; }
        public Order_Master order_Master { get; set; }
        public Order_Details_Master order_Details_Master { get; set; }
        public Product_Master product_Master { get; set; }
        public Category_Master category_Master { get; set; }
        public Payment_Master payment_Master { get; set; }
    }
}