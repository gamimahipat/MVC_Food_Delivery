using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Food_Delivery.ViewModels
{
    public class PaymentInitialModel
    {
        [Key]
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string CustomerAddress { get; set; }
        public int TotalPrice { get; set; }
    }
}