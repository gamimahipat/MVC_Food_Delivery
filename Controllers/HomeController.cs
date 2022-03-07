using MVC_Food_Delivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Food_Delivery.Controllers
{
    public class HomeController : Controller
    {
        private Food_Delivery_DBEntities db = new Food_Delivery_DBEntities();
        public ActionResult Index()
        {
           
            
            return View();
        }

        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {
           

            return View();
        }
         
       
    }
}