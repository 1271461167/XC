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
        private IRunController runController = GTS.GetGTS();
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
                if (value == pul)
                    return;
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
                if (band == value)
                    return;
                runController.SetAxisBand(short.Parse((this.Axis + 1).ToString()), band, this.Time);
                int b, t;
                runController.GetAxisBand(short.Parse((this.Axis + 1).ToString()), out b, out t);
                MotorSettings.Motor_Setting[this.Axis].Band = b;
                band = b;
                this.DoNotify();
            }
        }
        private int time;

        public int Time
        {
            get { return time; }
            set
            {
                if (time == value)
                    return;
                runController.SetAxisBand(short.Parse((this.Axis + 1).ToString()), this.Band, time);
                int b, t;
                runController.GetAxisBand(short.Parse((this.Axis + 1).ToString()), out b, out t);
                MotorSettings.Motor_Setting[this.Axis].Time = t;
                time = t;
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
            MotorSettings.Motor_Setting[this.Axis].Axis = this.Axis;
            this.Band = MotorSettings.Motor_Setting[this.Axis].Band;
            this.Time = MotorSettings.Motor_Setting[this.Axis].Time;          
            this.Pul = MotorSettings.Motor_Setting[this.Axis].Puls;
        }
    }
}
