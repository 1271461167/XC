using _2023_12_11XiChun.Base;
using _2023_12_11XiChun.Settings;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace _2023_12_11XiChun.Model
{
    public class ParameterModel : NotifyBase
    {
        private ParameterModel()
        {
            for (int i = 0; i < 8; i++) { IN.Add(false); OUT.Add(false); }
            for (int i = 0; i < 3; i++) { Process.Add(new AutoProcess()); }
            CancelSave();

        }
        private static ParameterModel parameter = null;
        public static ParameterModel GetParameter()
        {
            if (parameter == null)
            {
                parameter = new ParameterModel();
            }
            return parameter;
        }
        private Motor xmotor = new Motor { Pulse = MyApp.Default.XPulse };

        public Motor XMotor
        {
            get { return xmotor; }
            set { xmotor = value; }
        }
        private Motor ymotor = new Motor { Pulse = MyApp.Default.YPulse };

        public Motor YMotor
        {
            get { return ymotor; }
            set { ymotor = value; }
        }
        public ObservableCollection<bool> IN { get; set; } = new ObservableCollection<bool>();
        public ObservableCollection<bool> OUT { get; set; } = new ObservableCollection<bool>();
        private List<AutoProcess> process = new List<AutoProcess>();

        public List<AutoProcess> Process
        {
            get { return process; }
            set { process = value; }
        }
        private BlindProcess bprocess =new BlindProcess();

        public BlindProcess Bprocess
        {
            get { return bprocess ; }
            set { bprocess  = value; }
        }
        private AxisPosition waitposition =new AxisPosition {XPosition=MyApp.Default.WaitX,YPosition=MyApp.Default.WaitY };

        public AxisPosition WaitPosition
        {
            get { return waitposition; }
            set { waitposition = value; }
        }


        public void Save()
        {
            try
            {
                MyApp.Default.YVelocity1 = Process[0].YVelocity;
                MyApp.Default.XVelocity1 = Process[0].XVelocity;
                MyApp.Default.DelayTime1 = Process[0].DelayTime;
                MyApp.Default.ObjCount1 =  Process[0].AllEntCount;
                MyApp.Default.MoveCount1 = Process[0].MoveCount;

                MyApp.Default.YVelocity2 = Process[1].YVelocity;
                MyApp.Default.XVelocity2 = Process[1].XVelocity;
                MyApp.Default.DelayTime2 = Process[1].DelayTime;
                MyApp.Default.ObjCount2 = Process[1].AllEntCount;
                MyApp.Default.MoveCount2 = Process[1].MoveCount;

                MyApp.Default.YVelocity3 = Process[2].YVelocity;
                MyApp.Default.XVelocity3 = Process[2].XVelocity;
                MyApp.Default.DelayTime3 = Process[2].DelayTime;
                MyApp.Default.ObjCount3 = Process[2].AllEntCount;
                MyApp.Default.MoveCount3 = Process[2].MoveCount;

                MyApp.Default.Xposition1_1 = Process[0].MovePosition[0].XPosition;
                MyApp.Default.Yposition1_1 = Process[0].MovePosition[0].YPosition;
                MyApp.Default.Xposition1_2 = Process[0].MovePosition[1].XPosition;
                MyApp.Default.Yposition1_2 = Process[0].MovePosition[1].YPosition;
                MyApp.Default.Xposition1_3 = Process[0].MovePosition[2].XPosition;
                MyApp.Default.Yposition1_3 = Process[0].MovePosition[2].YPosition;

                MyApp.Default.Xposition2_1 = Process[1].MovePosition[0].XPosition;
                MyApp.Default.Yposition2_1 = Process[1].MovePosition[0].YPosition;
                MyApp.Default.Xposition2_2 = Process[1].MovePosition[1].XPosition;
                MyApp.Default.Yposition2_2 = Process[1].MovePosition[1].YPosition;
                MyApp.Default.Xposition2_3 = Process[1].MovePosition[2].XPosition;
                MyApp.Default.Yposition2_3 = Process[1].MovePosition[2].YPosition;

                MyApp.Default.Xposition3_1 = Process[2].MovePosition[0].XPosition;
                MyApp.Default.Yposition3_1 = Process[2].MovePosition[0].YPosition;
                MyApp.Default.Xposition3_2 = Process[2].MovePosition[1].XPosition;
                MyApp.Default.Yposition3_2 = Process[2].MovePosition[1].YPosition;
                MyApp.Default.Xposition3_3 = Process[2].MovePosition[2].XPosition;
                MyApp.Default.Yposition3_3 = Process[2].MovePosition[2].YPosition;

                MyApp.Default.Save();
            }
            catch { MyApp.Default.Save(); }
        }

        public void CancelSave()
        {
            try
            {
                Process[0].YVelocity = MyApp.Default.YVelocity1;
                Process[0].XVelocity = MyApp.Default.XVelocity1;
                Process[0].DelayTime = MyApp.Default.DelayTime1;
                Process[0].AllEntCount = MyApp.Default.ObjCount1;
                Process[0].MoveCount = MyApp.Default.MoveCount1;

                Process[1].YVelocity = MyApp.Default.YVelocity2;
                Process[1].XVelocity = MyApp.Default.XVelocity2;
                Process[1].DelayTime = MyApp.Default.DelayTime2;
                Process[1].AllEntCount = MyApp.Default.ObjCount2;
                Process[1].MoveCount = MyApp.Default.MoveCount2;


                Process[2].YVelocity = MyApp.Default.YVelocity3;
                Process[2].XVelocity = MyApp.Default.XVelocity3;
                Process[2].DelayTime = MyApp.Default.DelayTime3;
                Process[2].AllEntCount = MyApp.Default.ObjCount3;
                Process[2].MoveCount = MyApp.Default.MoveCount3;

                Process[0].MovePosition[0].XPosition = MyApp.Default.Xposition1_1;
                Process[0].MovePosition[0].YPosition = MyApp.Default.Yposition1_1;
                Process[0].MovePosition[1].XPosition = MyApp.Default.Xposition1_2;
                Process[0].MovePosition[1].YPosition = MyApp.Default.Yposition1_2;
                Process[0].MovePosition[2].XPosition = MyApp.Default.Xposition1_3;
                Process[0].MovePosition[2].YPosition = MyApp.Default.Yposition1_3;

                Process[1].MovePosition[0].XPosition = MyApp.Default.Xposition2_1;
                Process[1].MovePosition[0].YPosition = MyApp.Default.Yposition2_1;
                Process[1].MovePosition[1].XPosition = MyApp.Default.Xposition2_2;
                Process[1].MovePosition[1].YPosition = MyApp.Default.Yposition2_2;
                Process[1].MovePosition[2].XPosition = MyApp.Default.Xposition2_3;
                Process[1].MovePosition[2].YPosition = MyApp.Default.Yposition2_3;

                Process[2].MovePosition[0].XPosition = MyApp.Default.Xposition3_1;
                Process[2].MovePosition[0].YPosition = MyApp.Default.Yposition3_1;
                Process[2].MovePosition[1].XPosition = MyApp.Default.Xposition3_2;
                Process[2].MovePosition[1].YPosition = MyApp.Default.Yposition3_2;
                Process[2].MovePosition[2].XPosition = MyApp.Default.Xposition3_3;
                Process[2].MovePosition[2].YPosition = MyApp.Default.Yposition3_3;

            }
            catch { }

        }

    }
}
