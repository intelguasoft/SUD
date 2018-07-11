using SUD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUD.ViewModels
{
    public class NewSaleView
    {

        public int SaleId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public int ClientId { get; set; }

        [Required]
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

        public List<SaleDetailBk> Details { get; set; }


        public double TotalQuantity { get { return Details == null ? 0 : Details.Sum(d => d.Quantity); } }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        public decimal TotalValue { get { return Details == null ? 0 : Details.Sum(d => d.Value); } }

    }
}