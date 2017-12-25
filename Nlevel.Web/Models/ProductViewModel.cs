using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nlevel.Web.Models
{
    public class ProductViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string ProductName { get; set; }
        [Range(10.0, 100000.0, ErrorMessage = "Недопустимая цена")]
        public double ProductCost { get; set; }
    }
}