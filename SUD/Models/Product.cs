using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio" )]
        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Nota")]
        public string Note { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Imagen")]
        public string Image { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Medida")]
        public string Medida { get; set; }

        public virtual ICollection<CellarProduct> CellarProducts { get; set; }

        public virtual ICollection<Bar> Bars { get; set; }

        public virtual Department Department { get; set; }

        public virtual Measure Measure { get; set; }


    }
}