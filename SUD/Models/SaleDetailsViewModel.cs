using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class SaleDetailsViewModel
    {
        public int SaleDetailId { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public int Quantity { get; set; }

        public float IVAPercentage { get; set; }

        public float DiscountRate { get; set; }



        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int KardexId { get; set; }
    }
}