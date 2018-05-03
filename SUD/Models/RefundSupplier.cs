using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class RefundSupplier
    {

        public int RefundSupplierId { get; set; }

        public DateTime Date { get; set; }

        public int PurchaseId { get; set; }

        public virtual Purchase Purchase { get; set; }


    }
}