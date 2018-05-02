using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_ClientRefunds")]
    public class ClientRefund
    {
        [Key]
        public int ClientRefundId { get; set; }

        [Required(ErrorMessage ="Ingrese una fecha")]
        public DateTime Date { get; set; }

        public int SaleId { get; set; }


        public virtual Sale Sale { get; set; }

        


    }
}