using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_Sales")]

    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [Required(ErrorMessage ="Ingrese una fecha")]
        [Display(Name = "Fecha", Description = "dd/mm/yyyy")]
        [DataType(DataType.Date)]
        public DateTime Datetime { get; set; }

        [Display(Name = "Cliente")]
        public int ClientId { get; set; }

        [Display(Name = "Bodega")]
        public int CellarId { get; set; }

        [Display(Name = "Tipo de Documento")]
        public int AccountingDocumentId { get; set; }

        // El valor de esta propiedad debe ser tomada del modelo AccoutingDocument en su propiedad InitialNumber.
        // No debe ser ingresada por el usuario, debe generarse desde el momento en que una venta es ejecutada, 
        // Al momento de guardar la venta el InitialNumber en el modelo AccoutingDocument debe ser incrementado en 1.
        public int DocumentNumber { get; set; }

        [Display(Name = "Metodo de Pago")]
        public int PaymentMethodId { get; set; }

        public virtual Client Client { get; set; }
        public virtual Cellar Cellar { get; set; }
        public virtual AccountingDocument AccountingDocument{ get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }

        public virtual ICollection<ClientRefund> ClientRefunds { get; set; }
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }


    }
}