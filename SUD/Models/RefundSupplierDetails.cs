using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class RefundSupplierDetails
    {

        public int RefundSupplierDetailsId { get; set; }

        public int RefundSupplierId { get; set; }

        public int ProductId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public float Quantity { get; set; }

        public int KardexId { get; set; }

        public float VATRate { get; set; }

        public float DiscountRate { get; set; }


    }
}