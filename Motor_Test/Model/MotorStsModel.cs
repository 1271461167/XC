using Motor_Test.Common;
using Motor_Test.Common.GTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Model
{
    public class MotorStsModel:CommandAndNotifyBase
    {
        public MotorStsModel() 
        {
            Task.Run(() => { GetSts(); });
        }
        private bool enable;

        public bool Enable
        {
            get { return enable; }
            set { enable = value;this.DoNotify(); }
        }
        private short axis;

        public short Axis
        {
            get { return axis; }
            set { axis = value;this.DoNotify(); }
        }

        private void GetSts()
        {
            uint clk;
            int AxisState;
            double dRealPos, dRealVel, encPos;
            while (true)
            {               
                mc.GT_GetSts(this.Axis, out AxisState, 1, out clk);
                this.Enable = ((AxisState & 0x0200) != 0) ? true : false;
            }
        }
    }
}
