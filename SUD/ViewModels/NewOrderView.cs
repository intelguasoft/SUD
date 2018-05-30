using SUD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUD.ViewModels
{
    public class NewOrderView
    {
        public int OrderId { get; set; }

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

        [Required]
        [Display(Name = "Ruta")]
        public int RouteId { get; set; }

        public List<OrderDetailBk> Details { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double TotalQuantity { get { return Details == null ? 0 : Details.Sum(d => d.Quantity); } }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal TotalValue { get { return Details == null ? 0 : Details.Sum(d => d.Value); } }

    }
}