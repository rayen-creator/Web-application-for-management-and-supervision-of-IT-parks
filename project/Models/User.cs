using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
        public string Samaccountname { get; set; }
        public string Description { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        public string GroupName { get; set; }
    }
}
