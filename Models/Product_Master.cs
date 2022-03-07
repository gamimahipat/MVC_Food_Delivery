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

    public partial class Product_Master
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product_Master()
        {
            this.Order_Details_Master = new HashSet<Order_Details_Master>();
        }
    
        public int ProductID { get; set; }


        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please Enter Product Category")]
        public Nullable<int> CategoryID { get; set; }


        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please Enter Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Details")]
        [Required(ErrorMessage = "Please Enter Product Details")]
        public string ProductDetails { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Please Enter Product Price")]
        public Nullable<double> ProductPrice { get; set; }
       

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Please Enter Product Image")]
        public string ProductImage { get; set; }





        public virtual Category_Master Category_Master { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details_Master> Order_Details_Master { get; set; }
    }
}