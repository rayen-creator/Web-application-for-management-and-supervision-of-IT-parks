using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class Pc
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(15)")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long Exemple : 192.168.1.1", MinimumLength = 9)]
        [DisplayName("IP Address")]
        public String IP { get; set; }

        [Required]
        [DisplayName("Mac Address ")]
        [Column(TypeName = "nvarchar(20)")]
        public String Adress_Mac { get; set; }

        [Required]
        [DisplayName("Name")]
        [Column(TypeName = "varchar(20)")]
        public String Name { get; set; }

      
        [DisplayName("Parc")]
        public int ParcID { get; set; }
        [ForeignKey("ParcID")]
        public virtual Parc Parc { get; set; }

        [Required]
        [DisplayName("Active directory")]
        [Column(TypeName = "nvarchar(20)")]
        [StringLength(3, ErrorMessage = "The {0} must be yes or no", MinimumLength = 2)]
        public string AD { get; set; }

        public ICollection<AppPc> AppsPCs { get; set; }

        //[DisplayName("Application")]
        //public int? ApplicationID { get; set; }
        //[ForeignKey("AppID")]
        //public virtual App App { get; set; }

    }
}
