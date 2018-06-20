using SUD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.ViewModels
{
    public class NewProductView
    {
        public int ProductId { get; set; }

        public List<BarCode> BarCodes { get; set; }

        [Index("Barra_Codigo", IsUnique = true, Order = 2)]
        [Display(Name = "Código de Barra")]
        public long Bar { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Unidad de Medida")]
        public int MeasureId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Producto")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Nota")]
        public string Note { get; set; }

        [Display(Name = "Imagen")]
        public string Image { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Medida")]
        public string Medida { get; set; }

        [NotMapped]
        public HttpPostedFileBase FotografiaFile { get; set; }

        public virtual Category Category { get; set; }

        public virtual Measure Measure { get; set; }
    }
}