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

        [Index("Barra_Producto", IsUnique = true, Order = 1)]
        [Display(Name = "Producto")]
        public int ProductId { get; set; }

        [Index("Barra_Codigo", IsUnique = true, Order = 2)]
        public int Bar { get; set; }

        public virtual Product Product { get; set; }

    }
}