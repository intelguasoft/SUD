using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_Cellars")]

    public class Cellar
    {
        [Key]
        public int CellarId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        public virtual ICollection<CellarProduct> CellarProducts { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }



    }
}