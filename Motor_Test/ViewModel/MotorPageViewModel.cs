using Motor_Test.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Motor_Test.ViewModel
{
    public class MotorPageViewModel
    {
        public ObservableCollection<Position> Points { get; set; } = new ObservableCollection<Position>();

        public MotorPageViewModel()
        {
            Points.Add(new Position {RowPoints=new ObservableCollection<Point> { new Point(0, 0),new Point(1,0),new Point(2,2) } });
            Points.Add(new Position { RowPoints = new ObservableCollection<Point> { new Point(0, 0), new Point(1, 0) } });
            Points.Add(new Position { RowPoints = new ObservableCollection<Point> { new Point(0, 0), new Point(1, 0) } });
            Points.Add(new Position { RowPoints = new ObservableCollection<Point> { new Point(0, 0), new Point(1, 0) } });
        }
    }
}
