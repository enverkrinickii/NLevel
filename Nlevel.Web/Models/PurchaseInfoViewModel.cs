using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
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
        [RegularExpression("^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$", ErrorMessage = "Не правильный формат даты")]
        public string SaleDate { get; set; }

        public int Id { get; set; }
    }
}