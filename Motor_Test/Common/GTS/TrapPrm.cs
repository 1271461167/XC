using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Common.GTS
{
    public class TrapPrm
    {
        public double Acc { get; set; }
        public double Dec { get; set; }
        public double Vel { get; set; }
        public int Position { get; set; }
        public int SmoothTime { get; set; }
    }
}
