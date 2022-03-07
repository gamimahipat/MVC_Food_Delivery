using MVC_Food_Delivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Food_Delivery.Services
{
    public class Product_Master_Services
    {
        public List<Product_Master> GetProduct(List<int> IDs)
        {
            using (var db = new Food_Delivery_DBEntities())
            {
                return db.Product_Master.Where(Product => IDs.Contains(Product.ProductID)).ToList();
            }
        }
    }
}