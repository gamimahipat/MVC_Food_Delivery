using MVC_Food_Delivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Food_Delivery.ViewModels
{
    public class CheckoutViewModel
    {        
        public List<Product_Master> CartProducts { get; set; }
    }
}