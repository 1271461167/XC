using Motor_Test.Common;
using Motor_Test.Common.GTS;
using Motor_Test.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Model
{
    public class MotorSettingsModel : CommandAndNotifyBase
    {
        public CommandAndNotifyBase SelectChangedCommand { get; set; } = new CommandAndNotifyBase();

        private int axis;

        public int Axis
        {
            get { return axis; }
            set { axis = value; this.DoNotify(); }
        }
        private int pul;

        public int Pul
        {
            get { return pul; }
            set
            {
                pul = value;
                MotorSettings.Motor_Setting[this.Axis].Puls = pul;
                this.DoNotify();
            }
        }
        private int band;

        public int Band
        {
            get { return band; }
            set
            {
                band = value;
                MotorSettings.Motor_Setting[this.Axis].Band = band;
                mc.GT_SetAxisBand(short.Parse((this.Axis + 1).ToString()), band, this.Time);
                this.DoNotify();
            }
        }
        private int time;

        public int Time
        {
            get { return time; }
            set
            {
                time = value;
                MotorSettings.Motor_Setting[this.Axis].Time = time;
                mc.GT_SetAxisBand(short.Parse((this.Axis + 1).ToString()), this.Band, time);
                this.DoNotify();
            }
        }
        public MotorSettingsModel()
        {
            SelectChangedCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            SelectChangedCommand.DoExecute = new Action<object>((obj) => { SelectChangedFunction(); });
        }

        private void SelectChangedFunction()
        {          
            int band, time;
            MotorSettings.Motor_Setting[this.Axis].Axis = this.Axis;
            this.Band = MotorSettings.Motor_Setting[this.Axis].Band;
            this.Time = MotorSettings.Motor_Setting[this.Axis].Time;
            mc.GT_GetAxisBand(short.Parse((this.Axis + 1).ToString()), out band, out time);
            this.Band= band;
            this.Time= time;           
            this.Pul = MotorSettings.Motor_Setting[this.Axis].Puls;
        }
    }
}
