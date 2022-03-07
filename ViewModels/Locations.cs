using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Food_Delivery.ViewModels
{
    public class Locations
    {
        public int ID { get; set; }
       
        public string CityName { get; set; }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(18, 5)")]
        public decimal Longitude { get; set; }

        public string Description { get; set; }

    }
}