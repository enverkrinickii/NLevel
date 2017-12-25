using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nlevel.Web.Models
{
    public class ManagerViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Surname { get; set; }
    }
}