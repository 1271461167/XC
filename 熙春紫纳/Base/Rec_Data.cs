using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_12_11XiChun.Base
{
    public class Rec_Data:NotifyBase
    {
        private double x_offset;//x偏移

        public double X_Offset
        {
            get { return x_offset; }
            set
            {
                x_offset = value;
                this.DoNotify();
            }
        }

        private double y_offset; //y偏移

        public double Y_Offset
        {
            get { return y_offset; }
            set { y_offset = value; this.DoNotify(); }
        }
        private double angle_offset;//角度偏移

        public double Angle_Offset
        {
            get { return angle_offset; }
            set { angle_offset = value; this.DoNotify(); }
        }

        public Rec_Data()
        {
            x_offset = 0;
            y_offset = 0;
            angle_offset = 0;
        }

        public void Init()
        {
            x_offset = 0;
            y_offset = 0;
            angle_offset = 0;
        }
    }
}
