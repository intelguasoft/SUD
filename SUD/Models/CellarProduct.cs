using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_CellarProducts")]

    public class CellarProduct
    {
        [Key]
        public int CellarProductId { get; set; }

        [Index("CellarProduct_Bodega", IsUnique = true, Order = 1)]
        [Display(Name = "Bodega")]
        public int CellarId { get; set; }

        [Index("CellarProducto_Producto", IsUnique = true, Order = 2)]
        [Display(Name = "Producto")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Stock")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Minimos")]
        public int Minimum { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Maximos")]
        public int Maximum { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Dias Reposiciones")]
        public int ReplacementDays { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Cantidad Minima")]
        public int MinimumAmount { get; set; }

        public virtual Cellar Cellar { get; set; }

        public virtual Product Product { get; set; }


    }
}