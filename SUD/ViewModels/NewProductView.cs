using SUD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.ViewModels
{
    public class NewProductView
    {
        public Product Product { get; set; }

        public BarCode BarCode { get; set; }

        public List<BarCode> BarCodes { get; set; }

    }
}