using Motor_Test.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Motor_Test.Model
{
    public class MotorArrayModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public double RowSpacing { get; set; }
        public double ColumnSpacing { get; set; }
        public Point StartPoint { get; set; }
        public ObservableCollection<Position> Points { get; set; } = new ObservableCollection<Position>();
    }
}
