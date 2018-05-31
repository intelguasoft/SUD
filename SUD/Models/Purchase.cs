using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_Purchases")]
    public class Purchase
    {
        [Key]
        public long PurchaseId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Proveedor")]
        public long SupplierId { get; set; }

        [Required]
        [Display(Name = "Bodega")]
        public int CellarId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual Cellar Cellar { get; set; }

        public virtual State States { get; set; }

        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}