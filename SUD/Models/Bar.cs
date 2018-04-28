using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class Bar
    {
        [Key]
        public int IdProduct { get; set; }

        [Key]
        public int Barra { get; set; }

        public virtual Product Product { get; set; }


    }
}