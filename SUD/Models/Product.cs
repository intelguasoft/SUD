using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_Products")]

    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Departamento")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Medida")]
        public int MeasureId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio" )]
        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Nota")]
        public string Note { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Imagen")]
        public string Image { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Medida")]
        public string Medida { get; set; }

        public virtual ICollection<CellarProduct> CellarProducts { get; set; }

        public virtual ICollection<BarCode> BarCodes { get; set; }

        public virtual ICollection<ClientRefundDetail> ClientRefundDetails { get; set; }

        public virtual ICollection<SaleDetail> SaleDetails { get; set; }

        public virtual ICollection<PurchaseDetails> PurchaseDetails { get; set; }

        public virtual Department Department { get; set; }

        public virtual Measure Measure { get; set; }

        


    }
}