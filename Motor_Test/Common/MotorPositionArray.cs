using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Motor_Test.Common
{
    public class MotorPositionArray
    {
        public static int Row {  get; set; }
        public static int Column { get; set; }
        public static double RowSpacing {  get; set; }
        public static double ColumnSpacing { get; set; }
        public static Point StartPoint { get; set; }
        public List<List<Point>> Points { get; set;}=new List<List<Point>>();
    }
}
