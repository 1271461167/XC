﻿using Motor_Test.Common;
using Motor_Test.Common.GTS;
using Motor_Test.Common.Motor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Model
{
    public class PrfMotorModel : CommandAndNotifyBase
    {
        #region 字段
        private short axis;             //轴号
        private double vel;             //速度 单位 pul/ms
        private double acc;             //加速度时间 单位 ms
        private double dec;             //减速度时间 单位 ms
        private int pul;                //脉冲当量 单位 pul/mm
        private short smoothtime;      //平滑时间 ms
        private double position;        //设定位置 ms
        private int count;              //往返次数
        #endregion
        #region 属性
        public short Axis
        {
            get { return axis; }
            set { axis = value; this.DoNotify(); }
        }
        public double Vel
        {
            get { return vel; }
            set { vel = value; this.DoNotify(); }
        }
        public double AccTime
        {
            get { return acc; }
            set { acc = value; this.DoNotify(); }
        }
        public double DecTime
        {
            get { return dec; }
            set { dec = value; this.DoNotify(); }
        }
        public int Pul
        {
            get { return pul; }
            set { pul = value; this.DoNotify(); }
        }
        public short Smoothtime
        {
            get { return smoothtime; }
            set { smoothtime = value; this.DoNotify(); }
        }
        public double Position
        {
            get { return position; }
            set { position = value; this.DoNotify(); }
        }
        public int Count
        {
            get { return count; }
            set { count = value; this.DoNotify(); }
        }
        #endregion
        #region 方法
        private void TrapRun()
        {
            double Vel_Tem = Vel * Pul / 1000.0;
            double Acc = Vel_Tem / AccTime;
            double Dec = Vel_Tem / DecTime;
            int Pos_Tem = (int)(Position * Pul);
            MotorRun.Trap(short.Parse((Axis + 1).ToString()), Vel_Tem,Pos_Tem,Acc,Dec,Smoothtime);
        }

        private void RoundTrap()
        {
            double enc_pos;
            mc.GT_GetEncPos();
        }
        #endregion
    }
}
