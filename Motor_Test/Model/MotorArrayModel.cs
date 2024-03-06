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
    public class MotorArrayModel:CommandAndNotifyBase
    {
        public CommandAndNotifyBase FirstArrayCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase SecondArrayCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase ThirdArrayCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase FourthArrayCommand { get; set; } = new CommandAndNotifyBase();
        private int row;

        public int Row
        {
            get { return row; }
            set { row = value;this.DoNotify(); }
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
        public ObservableCollection<Position> Points { get; set; } = new ObservableCollection<Position>();
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
        }

        private void FourthArrayFunction()
        {
            this.Points.Clear();
            for (int i = 0 ; i < this.Row; i++ )
            {
                this.Points.Add(new Position());
                for (int j = 0; j < this.Column ; j++)
                {
                    this.Points[i].RowPoints.Add(new Point(this.StartPoint.X + this.RowSpacing * j, this.StartPoint.Y - this.ColumnSpacing * i));
                }
            }
        }

        private void ThirdArrayFunction()
        {
            this.Points.Clear();
            for (int i = 0; i < this.Row; i++)
            {
                this.Points.Add(new Position());
                for (int j = this.Column - 1; j >= 0; j--)
                {
                    this.Points[i].RowPoints.Add(new Point(this.StartPoint.X - this.RowSpacing * j, this.StartPoint.Y - this.ColumnSpacing * i));
                }
            }
        }

        private void SecondArrayFunction()
        {
            this.Points.Clear();
            for (int i = this.Row - 1, index = 0; i >= 0; i--, index++)
            {
                this.Points.Add(new Position());
                for (int j = this.Column - 1; j >= 0; j--)
                {
                    this.Points[index].RowPoints.Add(new Point(this.StartPoint.X - this.RowSpacing * j, this.StartPoint.Y + this.ColumnSpacing * i));
                }
            }
        }

        private void FirstArrayFunction()
        {
            this.Points.Clear();
            for (int i = this.Row - 1, index = 0; i >= 0; i--, index++)
            {
                this.Points.Add(new Position());
                for (int j = 0; j < this.Column; j++)
                {
                    this.Points[index].RowPoints.Add(new Point(this.StartPoint.X + this.RowSpacing * j, this.StartPoint.Y + this.ColumnSpacing * i));
                }
            }
        }
    }
}
