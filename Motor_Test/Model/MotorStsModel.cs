using Motor_Test.Common;
using Motor_Test.Common.GTS;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Motor_Test.Model
{
    public class MotorStsModel:CommandAndNotifyBase
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }
        private  MotorStsModel() 
        {
            this.CancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => { GetSts(CancellationTokenSource.Token); }, CancellationTokenSource.Token);
        }
        private static MotorStsModel instance = null;
        public static MotorStsModel GetInstance()
        {
            if(instance==null)
            {
                instance = new MotorStsModel();
            }
            return instance;
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

        private double encPos;

        public double EncPos
        {
            get { return encPos; }
            set { encPos = value;this.DoNotify(); }
        }

        private bool alarm;

        public bool Alarm
        {
            get { return alarm; }
            set { alarm = value;this.DoNotify(); }
        }

        private bool opl;

        public bool OPL
        {
            get { return opl; }
            set { opl = value;this.DoNotify(); }
        }

        private bool onl;

        public bool ONL
        {
            get { return onl; }
            set { onl = value;this.DoNotify(); }
        }
        private bool runover;

        public bool RunOver
        {
            get { return runover; }
            set { runover = value;this.DoNotify(); }
        }

        private double prfPos;

        public double PrfPos
        {
            get { return prfPos; }
            set { prfPos = value;this.DoNotify(); }
        }
        private double prfVel;

        public double PrfVel
        {
            get { return prfVel; }
            set { prfVel = value;this.DoNotify(); }
        }

        private double encVel;

        public double EncVel
        {
            get { return encVel; }
            set { encVel = value;this.DoNotify(); }
        }


        private void GetSts(CancellationToken token)
        {
            uint clk;
            int AxisState;
            double dRealPos, dRealVel, encPos,encVel;
            while (true)
            {
                if (token.IsCancellationRequested)
                { break; }
                mc.GT_GetSts(short.Parse((Axis + 1).ToString()), out AxisState, 1, out clk);
                this.Enable = ((AxisState & 0x0200) != 0) ? true : false;
                this.Alarm = ((AxisState & 0x02) != 0) ? true : false;               
                this.OPL = ((AxisState & 0x020) != 0) ? true : false;
                this.ONL = ((AxisState & 0x040) != 0) ? true : false;
                this.RunOver = ((AxisState & 0x0800) != 0) ? true : false;
                mc.GT_GetEncPos(short.Parse((Axis + 1).ToString()), out encPos, 1, out clk);
                this.EncPos = encPos;
                mc.GT_GetPrfPos(short.Parse((Axis + 1).ToString()), out dRealPos, 1, out clk);
                this.PrfPos = dRealPos;
                mc.GT_GetPrfVel(short.Parse((Axis + 1).ToString()), out dRealVel, 1, out clk);
                this.PrfVel = dRealVel;
                mc.GT_GetEncVel(short.Parse((Axis + 1).ToString()), out encVel, 1, out clk);
                this.EncVel = encVel;
            }
        }
        public void Stop()
        {
            this.CancellationTokenSource?.Cancel();
            this.CancellationTokenSource?.Dispose();
            this.CancellationTokenSource = null;
        }
        public void Restart()
        {
            if(this.CancellationTokenSource==null)
            {
                this.CancellationTokenSource= new CancellationTokenSource();
                Task.Run(() => { GetSts(CancellationTokenSource.Token); }, CancellationTokenSource.Token);
            }
        }
    }
}
