//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_Food_Delivery.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Staff_Details_Master
    {
        public int WorkerTypeID { get; set; }
        public Nullable<int> UserID { get; set; }


        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please Enter name")]
        [RegularExpression(@"^[A-Za-z]*$|^[a-zA-Z]+\s[A-Za-z]*$|^[a-zA-Z]+\.[a-zA-Z]*$", ErrorMessage = "Please Enater Valid Name")]
        public string WorkerName { get; set; }


        [Display(Name = "Mo.Number")]
        [Required(ErrorMessage = "Please Enter Mobile Number")]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*|\s)?|[0]?)?[6789]\d{9}$", ErrorMessage = "Please Enater Valid Mobile Number")]
        public string WorkerMobileNo { get; set; }


        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please Enter Address")]
        public string WorkerAddress { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please Enter Email")]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Please Enater Valid Email")]
        public string WorkerEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter Password")]
        [RegularExpression(@"^((?=\S*[a-z])(?=\S*[A-Z])(?=\S*\d)(?=\S*[^\w\s])\S{6,20})$", ErrorMessage = "Password Should be 6 characters long , 1 uppercase , 1 lowercase char , 1 number , 1 Special Char.")]
        [DataType(DataType.Password)]
        public string WorkerPassword { get; set; }



        [Display(Name = "New Password")]
        [RegularExpression(@"^((?=\S*[a-z])(?=\S*[A-Z])(?=\S*\d)(?=\S*[^\w\s])\S{6,20})$", ErrorMessage = "Password Should be 6 characters long , 1 uppercase , 1 lowercase char , 1 number , 1 Special Char.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Password and Confirmation Password Not Matched")]
        [DataType(DataType.Password)]
        public string ConfirmPassowrd { get; set; }



        public virtual Staff_Manage_Master Staff_Manage_Master { get; set; }
    }
}
