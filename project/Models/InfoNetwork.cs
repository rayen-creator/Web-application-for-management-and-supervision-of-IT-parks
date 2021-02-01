using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class InfoNetwork
    {
        [Key]
        public int IdInfoNet { get; set; }
        public string ComputerName { get; set; }
        public string IP_Adress { get; set; }
        public string MAC_Adress { get; set; }
        [ForeignKey("PcName")]
        public virtual Pc Pc { get; set; }
    }
}
