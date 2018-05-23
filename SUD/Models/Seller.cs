using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_Sellers")]

    public class Seller
    {
        [Key]
        public int SellerId { get; set; }

        [Required(ErrorMessage = "Ingrese el DPI del Vendedor")]
        [Display(Name = "DPI", Description = "2117502980101")]
        [MaxLength(13)]
        public string Document { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Nombre", Description = "Nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Apellido", Description = "Apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Direccion", Description = "Direccion")]
        public string Address { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Telefono", Description = "Telefono")]
        public int Telephone { get; set; }

        [Display(Name = "Notas", Description = "Notas")]
        public string Note { get; set; }

        public virtual ICollection<Route> Routes { get; set; }

    }
}