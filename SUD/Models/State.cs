﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class State
    {

        [Key]
        public int StateId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener un maximo de  {1} caracteres de longitud.")]
        [Display(Name = "Estado")]
        [Index("State_Description_Index", IsUnique = true)]
        public string Description { get; set; }


    }
}