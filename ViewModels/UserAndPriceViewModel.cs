using MVC_Food_Delivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Food_Delivery.ViewModels
{
    public class UserAndPriceViewModel
    {
        public Customer_Master customer_Master { get; set; }
        public Order_Master order_Master { get; set; }
    }
}