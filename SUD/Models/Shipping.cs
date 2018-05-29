using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_Shipments")]

    public class Shipping
    {
        [Key]
        public int ShippingId { get; set; }

        [Display(Name = "Número de Orden")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha de Despacho")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Número de Factura")]
        public int InvoiceNumber { get; set; }

        [Display(Name = "Estado")]
        public int StateId { get; set; }

        public virtual Order Order { get; set; }
        public virtual State State { get; set; }


    }
}