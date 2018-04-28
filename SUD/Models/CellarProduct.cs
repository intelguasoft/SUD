using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class CellarProduct
    {
        [Key]
        public int IdCellar { get; set; }

        [Key]
        public int IdProduct { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Stock")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Minimos")]
        public int Minimum { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Maximos")]
        public int Maximum { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Dias Reposiciones")]
        public int ReplacementDays { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Cantidad Minima")]
        public int MinimumAmount { get; set; }

        public virtual Cellar Cellar { get; set; }

        public virtual Product Product { get; set; }


    }
}