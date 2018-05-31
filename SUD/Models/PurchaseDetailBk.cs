using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SUD.Models
{
    [Table("tbl_PurchaseDetailBks")]
    public class PurchaseDetailBk
    {
        [Key]
        public long PurchaseDetailBkId { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public string User { get; set; }

        [Required]
        [Display(Name = "Producto")]
        public long ProductId { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(1, double.MaxValue, ErrorMessage = "Debe ingresar en el campo {0} un valor entre {1} y {2}")]
        public decimal Cost { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Range(1, double.MaxValue, ErrorMessage = "Debe ingresar en el campo {0} un valor entre {1} y {2}")]
        public double Quantity { get; set; }

        [Display(Name = "Lote")]
        public string ManufacturingLot { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de fabricación")]
        public DateTime DueDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Value { get { return Cost * (decimal)Quantity; } }


        public virtual Product Product { get; set; }

        public virtual Kardex Kardex { get; set; }

    }
}