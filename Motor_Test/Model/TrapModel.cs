using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Model
{
    public class TrapModel
    {
        public TrapModel() { }
        public TrapModel(TrapModel trapModel) 
        {
            Vel=trapModel.Vel;
            Acc=trapModel.Acc;
            Dec=trapModel.Dec;
            SmoothTime=trapModel.SmoothTime;
            Position=trapModel.Position;
        }
        public double Vel { get; set; }
        public double Acc { get; set; }
        public double Dec { get; set; }
        public short SmoothTime {  get; set; }
        public int Position { get; set; }
    }
}
