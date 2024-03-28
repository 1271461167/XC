using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Model
{
    public class MotorModel
    {
        public short Axis {  get; set; }
        public double Vel {  get; set; }
        public double AccTime {  get; set; }
        public double DecTime { get; set; }
        public int Pul { get; set; }
    }
}
