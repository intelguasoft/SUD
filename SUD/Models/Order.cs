using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_Orders")]

    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Número de Orden")]
        public int OrderNumber { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha de Orden")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Cliente")]
        public int ClientId { get; set; }

        [Display(Name = "Bodega")]
        public int CellarId { get; set; }

        [Display(Name = "Ruta")]
        public int RouteId { get; set; }

        [Display(Name = "Estado")]
        public int StateId { get; set; }

        public virtual Client Client { get; set; }
        public virtual Cellar Cellar { get; set; }
        public virtual Route Route { get; set; }

        public virtual ICollection<Shipping> Shipments { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}