using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class AppPc
    {
        [Key, Column(Order = 0)]
        public int PcId { get; set; }
        public Pc Pc { get; set; }
        [Key, Column(Order = 1)]
        public int AppId { get; set; }
        public App App { get; set; }
    }
}
