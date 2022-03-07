using MVC_Food_Delivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Food_Delivery.ViewModels
{
    public class CoockOrderViewModel
    {
        public int CategoryIDNew { get; set; }
        public string ProductNameNew { get; set; }
        public int ProductIDNew { get; set; }
        public int OrderIDNew { get; set; }
        public Order_Master order_Master { get; set; }
        public Order_Details_Master order_Details_Master { get; set; }
        public Product_Master product_Master { get; set; }
        public Category_Master category_Master { get; set; }

        //public List<Order_Master> order_MastersList { get; set; }
        //public List<Order_Details_Master> order_Details_MastersList { get; set; }
        //public List<Product_Master> product_MastersList { get; set; }
    }
}