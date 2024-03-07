using Motor_Test.Common.ArrayMotor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Motor_Test.Common
{
    public class RowPoint
    {
        public ObservableCollection<MyPoint> RowPoints { get; set; }=new ObservableCollection<MyPoint>();

    }
}
