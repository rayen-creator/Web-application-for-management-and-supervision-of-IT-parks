using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class Performance
    {
        [Key]
        public int IdPerf { get; set; }

        public string ComputerName { get; set; }

        public float CPU { get; set; }

        public float RAM { get; set; }

        public float System_Up_Time { get; set; }

        public string ProcName { get; set; }

        public DateTime DateCreation { get; set; }

        [ForeignKey("PcName")]
        public virtual Pc Pc { get; set; }
    }
}
