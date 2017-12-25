using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nlevel.Web.Models
{
    public class PurchaseInfoViewModel
    {
        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string ProductName { get; set; }

        public double ProductCost { get; set; }

        [Required]
        public string ManagerSurname { get; set; }

        [Required]
        public string ClientSurname { get; set; }

        [Required]
        public string SaleDate { get; set; }

        public int Id { get; set; }
    }
}