using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class Purchase
    {

        public int PurchaseId { get; set; }

        public DateTime Date { get; set; }

        public int SupplierId { get; set; }

        public int CellarId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual Cellar Cellar { get; set; }
    }
}