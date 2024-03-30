using Motor_Test.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Model
{
    public class ArrayModel
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public double RowSpace { get; set; }
        public double ColSpace { get; set; }
        public Point StartPoint { get; set; }=new Point();
        public double Vel {  get; set; }
        public double Acc {  get; set; }
        public double Dec { get; set; }
        public short SmoothTime { get; set; }
        public ObservableCollection<RowPoint> PointList { get; set; } = new ObservableCollection<RowPoint>();
    }
}
