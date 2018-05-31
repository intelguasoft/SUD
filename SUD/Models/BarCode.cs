using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_BarCodes")]

    public class BarCode
    {

        [Key]
        public int BarCodeId { get; set; }

        [Display(Name = "Producto")]
        public int ProductId { get; set; }

        [Index("Barra_Codigo", IsUnique = true, Order = 2)]
        [Display(Name = "Código de Barra")]
        public long Bar { get; set; }

        public virtual Product Product { get; set; }

    }
}