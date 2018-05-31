using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_PaymentMethods")]

    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Metodo de Pago")]
        public string Description { get; set; }

        [Display(Name = "Detalles")]
        public string Details { get; set; }

       // public virtual ICollection<Sale> Sales { get; set; }
    }
}