﻿using Mapster;
using Motor_Test.Common;
using Motor_Test.Common.ArrayMotor;
using Motor_Test.Common.GTS;
using Motor_Test.Model;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading.Tasks;

namespace Motor_Test.Dto
{
    public class ArrayModelDto : CommandAndNotifyBase
    {
        private IRunController controller = GTS.GetGTS();
        private readonly ArrayModel _model;
        public CommandAndNotifyBase FirstArrayCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase SecondArrayCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase ThirdArrayCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase FourthArrayCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase ArrayRunCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase CheckAllCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase CheckNonCommand { get; set; } = new CommandAndNotifyBase();
        public ArrayModelDto(ArrayModel array)
        {
            _model = array;
            _model.Adapt(this);
            this.Points = _model.PointList;

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
        public int Row { get; set; }
        public int Col { get; set; }
        public double RowSpace { get; set; }
        public double ColSpace { get; set; }
        public Point StartPoint { get; set; } = new Point();
        public double Vel { get; set; }
        public double Acc { get; set; }
        public double Dec { get; set; }
        public short SmoothTime { get; set; }
        public ObservableCollection<RowPoint> Points { get; set; }
        public void ApplyChanges() => this.Adapt(this._model);
        public void DiscardChanges() => _model.Adapt(this);

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
            ApplyChanges();
            foreach (var item in this.Points)
            {
                foreach (var i in item.RowPoints)
                {
                    if (i.IsChecked == true)
                    {
                        Task[] tasks = new Task[2];
                        tasks[0] = Task.Run(() =>
                        {
                            controller.Trap(1, new TrapModel()
                            {
                                Acc = _model.Acc,
                                Dec = _model.Dec,
                                SmoothTime = _model.SmoothTime,
                                Vel = _model.Vel,
                                Position = i.Point.X,
                            });
                        });
                        tasks[1] = Task.Run(() =>
                        {
                            controller.Trap(2, new TrapModel()
                            {
                                Acc = _model.Acc,
                                Dec = _model.Dec,
                                SmoothTime = _model.SmoothTime,
                                Vel = _model.Vel,
                                Position = i.Point.X,
                            });
                        });
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
                for (int j = 0; j < this.Col; j++)
                {
                    this.Points[i].RowPoints.Add(new MyPoint(this.StartPoint.X + this.RowSpace * j, this.StartPoint.Y - this.ColSpace * i));
                }
            }
        }

        private void ThirdArrayFunction()
        {
            this.Points.Clear();
            for (int i = 0; i < this.Row; i++)
            {
                this.Points.Add(new RowPoint());
                for (int j = this.Col - 1; j >= 0; j--)
                {
                    this.Points[i].RowPoints.Add(new MyPoint(this.StartPoint.X - this.RowSpace * j, this.StartPoint.Y - this.ColSpace * i));
                }
            }
        }

        private void SecondArrayFunction()
        {
            this.Points.Clear();
            for (int i = this.Row - 1, index = 0; i >= 0; i--, index++)
            {
                this.Points.Add(new RowPoint());
                for (int j = this.Col - 1; j >= 0; j--)
                {
                    this.Points[index].RowPoints.Add(new MyPoint(this.StartPoint.X - this.RowSpace * j, this.StartPoint.Y + this.ColSpace * i));
                }
            }
        }

        private void FirstArrayFunction()
        {
            this.Points.Clear();
            for (int i = this.Row - 1, index = 0; i >= 0; i--, index++)
            {
                this.Points.Add(new RowPoint());
                for (int j = 0; j < this.Col; j++)
                {
                    this.Points[index].RowPoints.Add(new MyPoint(this.StartPoint.X + this.RowSpace * j, this.StartPoint.Y + this.ColSpace * i));
                }
            }
        }
    }
}
