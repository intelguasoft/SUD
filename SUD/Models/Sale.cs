using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class Sale
    {
        [Key]
        public int IdSale { get; set; }

        [Required(ErrorMessage ="Ingrese una fecha"]
        [Display(Name = "Fecha", Description = "dd/mm/yyyy")]
        public DateTime Datetime { get; set; }

        public int IdClient { get; set; }
        public int IdCellar { get; set; }


        public virtual Client Client { get; set; }
        public virtual Cellar Cellar { get; set; }



    }
}