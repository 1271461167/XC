﻿using Motor_Test.Common;
using Motor_Test.Common.GTS;
using Motor_Test.Global;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Motor_Test.Model
{
    public class MotorStsModel : CommandAndNotifyBase
    {
        private IRunController _runController = GTS.GetGTS();
        private static readonly object locker=new object();
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public CommandAndNotifyBase SelectChangedCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase ClrStsCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase EnableCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase ZeroPosCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase StopAxisCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase HomeCommand { get; set; }= new CommandAndNotifyBase();
        private MotorStsModel()
        {
            this.CancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => { GetSts(CancellationTokenSource.Token); }, CancellationTokenSource.Token);

            Pul = int.Parse(CreateIni.ReadIni("Axis" + Axis.ToString(), "Puls", ""));

            SelectChangedCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            SelectChangedCommand.DoExecute = new Action<object>((obj) => { SelectChangedFunction(); });
            ClrStsCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            ClrStsCommand.DoExecute = new Action<object>((obj) => { ClrSts(); });
            EnableCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            EnableCommand.DoExecute = new Action<object>((obj) => { Enabled(); });
            ZeroPosCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            ZeroPosCommand.DoExecute = new Action<object>((obj) => { ZeroPos(); });
            StopAxisCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            StopAxisCommand.DoExecute = new Action<object>((obj) => { StopAxis(); });
            HomeCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            HomeCommand.DoExecute = new Action<object>((obj) => { GoHome(); });
        }

        private void GoHome()
        {
            _runController.Home(short.Parse((Axis + 1).ToString()), new HomeModel() { });
        }

        private void StopAxis()
        {
            _runController.Stop(short.Parse((Axis + 1).ToString()));

        }

        private void ZeroPos()
        {
            _runController.ZeroPos(short.Parse((Axis + 1).ToString()));
        }

        private void Enabled()
        {
            if (Enable == false)
                _runController.Enable(short.Parse((Axis + 1).ToString()));
            else
                _runController.UnEnable(short.Parse((Axis + 1).ToString()));
        }

        private void ClrSts()
        {
            _runController.ClrSts(short.Parse((Axis + 1).ToString()),1);
        }

        private void SelectChangedFunction()
        {
            Pul = int.Parse(CreateIni.ReadIni("Axis" + Axis.ToString(), "Puls", ""));
        }

        private static MotorStsModel instance = null;
        public static MotorStsModel GetInstance()
        {
            if (instance == null)
            {
                lock(locker)
                {
                    if (instance == null)
                    {
                        instance = new MotorStsModel();
                    }
                }
            }
            return instance;
        }
        private bool enable;

        public bool Enable
        {
            get { return enable; }
            set { enable = value; this.DoNotify(); }
        }
        private bool run_error;

        public bool Run_Error
        {
            get { return run_error; }
            set { run_error = value; this.DoNotify(); }
        }

        private short axis;

        public short Axis
        {
            get { return axis; }
            set { axis = value; this.DoNotify(); }
        }

        private double encPos;

        public double EncPos
        {
            get { return encPos; }
            set { encPos = value; this.DoNotify(); }
        }

        private bool alarm;

        public bool Alarm
        {
            get { return alarm; }
            set { alarm = value; this.DoNotify(); }
        }

        private bool opl;

        public bool OPL
        {
            get { return opl; }
            set { opl = value; this.DoNotify(); }
        }

        private bool onl;

        public bool ONL
        {
            get { return onl; }
            set { onl = value; this.DoNotify(); }
        }
        private bool runover;

        public bool RunOver
        {
            get { return runover; }
            set { runover = value; this.DoNotify(); }
        }

        private double prfPos;

        public double PrfPos
        {
            get { return prfPos; }
            set { prfPos = value; this.DoNotify(); }
        }
        private double prfVel;

        public double PrfVel
        {
            get { return prfVel; }
            set { prfVel = value; this.DoNotify(); }
        }

        private double encVel;

        public double EncVel
        {
            get { return encVel; }
            set { encVel = value; this.DoNotify(); }
        }

        private int pul;

        public int Pul
        {
            get { return pul; }
            set { pul = value; }
        }
        private double enc_mm;

        public double Enc_mm
        {
            get { return enc_mm; }
            set { enc_mm = value; this.DoNotify(); }
        }
        private double vel_mm;

        public double Vel_mm
        {
            get { return vel_mm; }
            set { vel_mm = value; this.DoNotify(); }
        }
        private double acc;

        public double Acc
        {
            get { return acc; }
            set { acc = value; this.DoNotify(); }
        }
        private int mode;

        public int Mode
        {
            get { return mode; }
            set { mode = value; this.DoNotify(); }
        }


        private void GetSts(CancellationToken token)
        {
            uint clk;
            int AxisState;
            double dRealPos, dRealVel, encPos, encVel, enc_acc;
            int _mode;
            while (true)
            {
                if (token.IsCancellationRequested)
                { break; }
                mc.GT_GetSts(short.Parse((Axis + 1).ToString()), out AxisState, 1, out clk);
                this.Enable = ((AxisState & 0x0200) != 0) ? true : false;
                this.Alarm = ((AxisState & 0x02) != 0) ? true : false;
                this.OPL = ((AxisState & 0x020) != 0) ? true : false;
                this.ONL = ((AxisState & 0x040) != 0) ? true : false;
                this.RunOver = ((AxisState & 0x0800) != 0) ? true : false;
                this.Run_Error = ((AxisState & 0x10) != 0) ? true : false;
                mc.GT_GetEncPos(short.Parse((Axis + 1).ToString()), out encPos, 1, out clk);
                this.EncPos = encPos;
                this.Enc_mm = Math.Round(encPos / (double)this.Pul, 3);
                mc.GT_GetPrfPos(short.Parse((Axis + 1).ToString()), out dRealPos, 1, out clk);
                this.PrfPos = dRealPos;
                mc.GT_GetPrfVel(short.Parse((Axis + 1).ToString()), out dRealVel, 1, out clk);
                this.PrfVel = dRealVel;
                mc.GT_GetEncVel(short.Parse((Axis + 1).ToString()), out encVel, 1, out clk);
                this.EncVel = encVel;
                this.Vel_mm = Math.Round(encVel * 1000.0 / (double)this.Pul, 3);
                mc.GT_GetPrfMode(short.Parse((Axis + 1).ToString()), out _mode, 1, out clk);
                this.Mode = _mode;
                mc.GT_GetAxisEncAcc(short.Parse((Axis + 1).ToString()), out enc_acc, 1, out clk);
                this.Acc = enc_acc;
            }
        }
        public void Stop()
        {
            this.CancellationTokenSource?.Cancel();
            this.CancellationTokenSource?.Dispose();
            this.CancellationTokenSource = null;
        }
        public void Restart()
        {
            if (this.CancellationTokenSource == null)
            {
                this.CancellationTokenSource = new CancellationTokenSource();
                Task.Run(() => { GetSts(CancellationTokenSource.Token); }, CancellationTokenSource.Token);
            }
        }
    }
}
