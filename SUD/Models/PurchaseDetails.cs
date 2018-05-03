using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class PurchaseDetails
    {

        public int PurchaseDetailsId { get; set; }

        public int PurchaseId { get; set; }

        public int ProductId { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public float Quantity { get; set; }

        public int KardexId { get; set; }

        public float VATRate { get; set; }

        public float DiscountRate { get; set; }

        public string ManufacturingLot { get; set; }

        public DateTime DueDate { get; set; }

        public virtual Product Product { get; set; }

        public virtual Purchase Purchase { get; set; }

        public virtual Kardex Kardex { get; set; }

    }
}