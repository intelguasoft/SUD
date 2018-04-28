using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class Inventory
    {
        [Key]
        public int IdInventory { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Paso")]
        public int Step { get; set; }

        public virtual Cellar Cellar { get; set; }

        public virtual ICollection<InventoryDetail> InventoryDetails { get; set; }


    }
}