using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class Parc
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Name")]
        [Column(TypeName = "varchar(30)")]
        public String Name { get; set; }

        [Required]
        [DisplayName("Address")]
        [Column(TypeName = "nvarchar(20)")]
        public String Address { get; set;}

        [Required]
        [DisplayName("Telephone")]
        [Column(TypeName = "nvarchar(22)")]
        [StringLength(12, ErrorMessage = "The {0} must be at  {1} digit long.", MinimumLength = 12)]
        [Phone]
        public String Tel { get; set; }
        
        [Required]
        [DisplayName("Fax")]
        [Column(TypeName = "nvarchar(22)")]
        [StringLength(12, ErrorMessage = "The {0} must be at  {1} digit long.", MinimumLength = 12)]
        [Phone]
        public String Fax { get; set; }

        //[DisplayName("Pc")]
        
        //public virtual List<Pc> Pcs { get ;} 

    }
}
