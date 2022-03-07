using MVC_Food_Delivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Food_Delivery.ViewModels
{
    public class LoginViewModel
    {
        public Customer_Master customer_Master { get; set; }
        public Staff_Details_Master staff_Details_Master { get; set; }
        public Staff_Manage_Master staff_Manage_Master { get; set; }
    }
}