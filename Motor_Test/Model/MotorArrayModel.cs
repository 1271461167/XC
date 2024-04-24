using Motor_Test.Common;
using Motor_Test.Common.ArrayMotor;
using Motor_Test.Common.GTS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Motor_Test.Model
{
    public class MotorArrayModel : CommandAndNotifyBase
    {
        private IRunController controller = GTS.GetGTS();
        public CommandAndNotifyBase FirstArrayCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase SecondArrayCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase ThirdArrayCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase FourthArrayCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase ArrayRunCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase CheckAllCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase CheckNonCommand { get; set; } = new CommandAndNotifyBase();

        private int row;

        public int Row
        {
            get { return row; }
            set { row = value; this.DoNotify(); }
        }

        private int column;

        public int Column
        {
            get { return column; }
            set { column = value; this.DoNotify(); }
        }

        private double rowspacing;

        public double RowSpacing
        {
            get { return rowspacing; }
            set { rowspacing = value; this.DoNotify(); }
        }

        private double columnspacing;

        public double ColumnSpacing
        {
            get { return columnspacing; }
            set { columnspacing = value; this.DoNotify(); }
        }
        private Point startpoint;

        public Point StartPoint
        {
            get { return startpoint; }
            set { startpoint = value; this.DoNotify(); }
        }
        public ObservableCollection<RowPoint> Points { get; set; } = new ObservableCollection<RowPoint>();
        public MotorArrayModel()
        {
            FirstArrayCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            FirstArrayCommand.DoExecute = new Action<object>((obj) => { FirstArrayFunction(); });
            SecondArrayCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            SecondArrayCommand.DoExecute = new Action<object>((obj) => { SecondArrayFunction(); });
            ThirdArrayCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            ThirdArrayCommand.DoExecute = new Action<object>((obj) => { ThirdArrayFunction(); });
            FourthArrayCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            FourthArrayCommand.DoExecute = new Action<object>((obj) => { FourthArrayFunction(); });
            ArrayRunCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            ArrayRunCommand.DoExecute = new Action<object>((obj) => { ArrayRun(obj); });
            CheckAllCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            CheckAllCommand.DoExecute = new Action<object>((obj) => { CheckAll(); });
            CheckNonCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            CheckNonCommand.DoExecute = new Action<object>((obj) => { CheckNon(); });
        }

        private void CheckNon()
        {
            foreach (var item in this.Points)
            {
                foreach (var i in item.RowPoints)
                {
                    i.IsChecked = false;
                }
            }
        }

        private void CheckAll()
        {
            foreach (var item in this.Points)
            {
                foreach (var i in item.RowPoints)
                {
                    i.IsChecked = true;
                }
            }
        }

        private void ArrayRun(object obj)
        {
            foreach (var item in this.Points)
            {
                foreach (var i in item.RowPoints)
                {
                    if (i.IsChecked == true)
                    {
                        controller.Trap(1,new TrapModel() {});
                    }
                }
            }
        }

        private void FourthArrayFunction()
        {
            this.Points.Clear();
            for (int i = 0; i < this.Row; i++)
            {
                this.Points.Add(new RowPoint());
                for (int j = 0; j < this.Column; j++)
                {
                    this.Points[i].RowPoints.Add(new MyPoint(this.StartPoint.X + this.RowSpacing * j, this.StartPoint.Y - this.ColumnSpacing * i));
                }
            }
        }

        private void ThirdArrayFunction()
        {
            this.Points.Clear();
            for (int i = 0; i < this.Row; i++)
            {
                this.Points.Add(new RowPoint());
                for (int j = this.Column - 1; j >= 0; j--)
                {
                    this.Points[i].RowPoints.Add(new MyPoint(this.StartPoint.X - this.RowSpacing * j, this.StartPoint.Y - this.ColumnSpacing * i));
                }
            }
        }

        private void SecondArrayFunction()
        {
            this.Points.Clear();
            for (int i = this.Row - 1, index = 0; i >= 0; i--, index++)
            {
                this.Points.Add(new RowPoint());
                for (int j = this.Column - 1; j >= 0; j--)
                {
                    this.Points[index].RowPoints.Add(new MyPoint(this.StartPoint.X - this.RowSpacing * j, this.StartPoint.Y + this.ColumnSpacing * i));
                }
            }
        }

        private void FirstArrayFunction()
        {
            this.Points.Clear();
            for (int i = this.Row - 1, index = 0; i >= 0; i--, index++)
            {
                this.Points.Add(new RowPoint());
                for (int j = 0; j < this.Column; j++)
                {
                    this.Points[index].RowPoints.Add(new MyPoint(this.StartPoint.X + this.RowSpacing * j, this.StartPoint.Y + this.ColumnSpacing * i));
                }
            }
        }
    }
}
