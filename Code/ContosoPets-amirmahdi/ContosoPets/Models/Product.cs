﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoPets.Models
{
    public class Product
    {
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
