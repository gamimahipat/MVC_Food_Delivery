using MVC_Food_Delivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Food_Delivery.ViewModels
{
    public class CartViewModel
    {
        public Product_Master Product_Master { get; set; }
        public Add_To_Cart_Master Add_To_Cart_Master { get; set; }
       
    }
}