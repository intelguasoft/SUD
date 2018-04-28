using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int SupplyId { get; set; }

        public int BodegaId { get; set; }

        public virtual Supplier Supplier { get; set; }

        //public virtual Warehouse Warehouse { get; set; }
    }
}