using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Food_Delivery.ViewModels
{
    public class LogintypeViewModel
    {
        public int UserID { get; set; }

       
        [Required(ErrorMessage = "Please Enter Email")]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Please Enater Valid Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Password")]
        [RegularExpression(@"^((?=\S*[a-z])(?=\S*[A-Z])(?=\S*\d)(?=\S*[^\w\s])\S{6,20})$", ErrorMessage = "Password Should be 6 characters long , 1 uppercase , 1 lowercase char , 1 number , 1 Special Char.")]     
        public string Password { get; set; }
    }
}