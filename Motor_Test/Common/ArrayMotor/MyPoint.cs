using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Motor_Test.Common.ArrayMotor
{
    public class MyPoint : CommandAndNotifyBase
    {

        private Point point = new Point();

        public Point Point
        {
            get { return point; }
            set { point = value; this.DoNotify(); }
        }
        private bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; this.DoNotify(); }
        }

        public MyPoint(double x, double y)
        {
            point.X = x;
            point.Y = y;
        }
    }
}
