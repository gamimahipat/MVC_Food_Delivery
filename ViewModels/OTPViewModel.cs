using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Food_Delivery.ViewModels
{
    public class OTPViewModel
    {
        [Required(ErrorMessage = "Please Enter OTP")]
        public string otp { get; set; }
    }
}