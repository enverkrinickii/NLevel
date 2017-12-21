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
        public string ProductName { get; set; }

        [Required]
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