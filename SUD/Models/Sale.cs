using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_Sales")]

    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [Required(ErrorMessage ="Ingrese una fecha")]
        [Display(Name = "Fecha", Description = "dd/mm/yyyy")]
        [DataType(DataType.Date)]
        public DateTime Datetime { get; set; }

        [Display(Name = "Cliente")]
        public int ClientId { get; set; }

        [Display(Name = "Bodega")]
        public int CellarId { get; set; }


        public virtual Client Client { get; set; }
        public virtual Cellar Cellar { get; set; }

        public virtual ICollection<ClientRefund> ClientRefunds { get; set; }
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }


    }
}