using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class InfoSystem
    {
        [Key]
        public int IdSystem { get; set; }
        public string ComputerName { get; set; }
        public string OsSystem { get; set; }
        public string OS_Architecture { get; set; }
        public string OS_Version { get; set; }
        public string CPU_Physical_Core { get; set; }
        public string CPU_Logical_Core { get; set; }
        [ForeignKey("PcName")]
        public virtual Pc Pc { get; set; }
    }
}
