using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class App 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        [DisplayName("Application Name")]
        public String Name { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(1024)]
        [DisplayName("Description Of The Application")]
        [Column(TypeName = "nvarchar(max)")]
        public String Description { get; set; }

        public ICollection<AppPc> AppPcs { get; set; }


        //[DisplayName("Pc")]
        //public int? PcID { get; set; }
        //[ForeignKey("PcID")]
        //public virtual Pc Pc { get; set; }

    }
}
