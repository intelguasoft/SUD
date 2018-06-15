using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUD.ViewModels
{
    public class AddProductSaleView
    {
        [Required]
        [Display(Name = "Producto")]
        public int ProductId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(1, double.MaxValue, ErrorMessage = "Debe ingresar en el campo {0} un valor entre {1} y {2}")]
        public double Quantity { get; set; }
        //TODO Modificar las expresiones regulares para que acepten decimales.
        [Required(ErrorMessage = "Cantidad Requerida")]
        [Display(Name = "Porcentaje de IVA", Description = "0.00")]
       // [RegularExpression(@"^[0-9]\d*(\.\d +)?$", ErrorMessage = "Porcentaje Invalido, solo se permiten numeros enteros.")]
        public decimal IVAPercentage { get; set; }

        [Required(ErrorMessage = "Cantidad Requerida")]
        [Display(Name = "Porcentaje de descuento", Description = "0.00")]
        //[RegularExpression(@"^[0-9]\d*(\.\d +)?$", ErrorMessage = "Porcentaje Invalido, solo se permiten numeros enteros.")]
        public decimal DiscountRate { get; set; }

        [Display(Name = "Kardex")]
        public int? KardexId { get; set; }


    }
}