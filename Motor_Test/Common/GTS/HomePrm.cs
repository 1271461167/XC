using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Common.GTS
{
    public class HomePrm
    {
        public double VelHigh { get; set; }
        public double VelLow { get; set;}
        public int Mode { get; set; }
        public double Acc {  get; set; }
        public double Dec { get; set; }
        public int SearchHomeDistance { get; set; }
        public int SearchIndexDistance { get; set; }
        public int MoveDir { get; set; }
        public int IndexDir { get; set; }
        public int EscapeStep { get; set; }
        public int Edge {  get; set; }
        public int SmoothTime { get; set; }
        public int Offset { get; set; }
    }
}
