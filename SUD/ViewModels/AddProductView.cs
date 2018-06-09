using SUD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUD.ViewModels
{
    public class AddProductView
    {
        [Required]
        [Display(Name = "Producto")]
        public int ProductId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(1, double.MaxValue, ErrorMessage = "Debe ingresar en el campo {0} un valor entre {1} y {2}")]
        public double Quantity { get; set; }

        [Display(Name = "Lote")]
        public string ManufacturingLot { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de fabricación")]
        public DateTime DueDate { get; set; }
    }
}