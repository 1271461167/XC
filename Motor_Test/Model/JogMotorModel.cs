using Motor_Test.Common;
using Motor_Test.Common.GTS;
using Motor_Test.Common.Motor;
using Motor_Test.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Model
{
    public class JogMotorModel:CommandAndNotifyBase
    {
        private IRunController RunController=GTS.GetGTS();
		public JogMotorModel() 
		{
            JogPDownCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            JogPDownCommand.DoExecute = new Action<object>((obj) => { JogPDown(); });
            JogPUpCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            JogPUpCommand.DoExecute = new Action<object>((obj) => { JogPUp(); });
            JogNUpCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            JogNUpCommand.DoExecute = new Action<object>((obj) => { JogNUp(); });
            JogNDownCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            JogNDownCommand.DoExecute = new Action<object>((obj) => { JogNDown(); });
            SelectChangedCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            SelectChangedCommand.DoExecute = new Action<object>((obj) => { SelectChanged(); });
        }
        #region Command
        public CommandAndNotifyBase JogPDownCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase JogPUpCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase JogNDownCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase JogNUpCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase SelectChangedCommand { get; set; }= new CommandAndNotifyBase();
        #endregion
        #region 方法
        private void JogPUp()
        {
            RunController.Stop(this.Axis);
        }
        private void SelectChanged()
        {
            this.Pul = MotorSettings.Motor_Setting[this.Axis].Puls;
        }
        private void JogPDown()
        {
            double Vel_Tem = Vel * Pul / 1000.0;
            double Acc = Vel_Tem / AccTime;
            double Dec = Vel_Tem / DecTime;
            RunController.Jog(short.Parse((Axis + 1).ToString()),Vel_Tem,Acc,Dec);
        }

        private void JogNUp()
        {
            RunController.Stop(this.Axis);
        }

        private void JogNDown()
        {
            double Vel_Tem = Vel * Pul / 1000.0;
            double Acc = Vel_Tem / AccTime;
            double Dec = Vel_Tem / DecTime;
            RunController.Jog(short.Parse((Axis + 1).ToString()), -Vel_Tem, Acc, Dec);
        }
        #endregion
        #region 字段
        private short axis;//轴号
        private double vel;//速度 单位 pul/ms
        private double acc;//加速度时间 单位 ms
        private double dec;//减速度时间 单位 ms
        private int pul;//脉冲当量 单位 pul/mm
        #endregion
        #region 属性
        /// <summary>
        /// 轴号
        /// </summary>
        public short Axis
		{
			get { return axis; }
			set { axis = value; this.DoNotify(); }
		}
        /// <summary>
        /// 速度 单位 pul/ms
        /// </summary>
        public double Vel
		{
			get { return vel; }
			set { vel = value;this.DoNotify(); }
		}
        /// <summary>
        /// 加速度 单位 pul/ms^2
        /// </summary>
		public double AccTime
		{
			get { return acc; }
			set { acc = value;this.DoNotify(); }
		}
        /// <summary>
        /// 减速度 单位 pul/ms^2
        /// </summary>
        public double DecTime
		{
			get { return dec; }
			set { dec = value;this.DoNotify(); }
		}
        /// <summary>
        /// 脉冲当量 单位 pul/mm
        /// </summary>
        public int Pul
		{
			get { return pul; }
			set { pul = value;this.DoNotify(); }
		}
        #endregion
    }
}
