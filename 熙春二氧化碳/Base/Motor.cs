using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_12_11XiChun.Base
{
    public class Motor:NotifyBase
    {
		public enum Mode {JOG,AUTO,Home}
		private bool enable;

		public bool Enable
		{
			get { return enable; }
			set 
			{
				enable = value;
				this.DoNotify();
			}
		}
		private bool limit;

		public bool Limit
		{
			get { return limit; }
			set 
			{ 
				limit = value;
				this.DoNotify();
			}
		}
		private bool alarm;

		public bool Alarm
		{
			get { return alarm; }
			set 
			{ 
				alarm = value;
				this.DoNotify();
			}
		}
		private bool opl;

		public bool OPL
		{
			get { return opl; }
			set 
			{ 
				opl = value;
				this.DoNotify();
			}
		}
		private bool onl;

		public bool ONL
		{
			get { return onl; }
			set 
			{ 
				onl = value;
				this.DoNotify();
			}
		}
		private bool run;

		public bool Run
		{
			get { return run; }
			set 
			{ 
				run = value;
				this.DoNotify();
			}
		}
		private bool homeover;

		public bool HomeOver
		{
			get { return homeover; }
			set 
			{ 
				homeover = value;
				this.DoNotify();
			}
		}
		private double realposition;

		public double RealPosition
		{
			get { return realposition; }
			set 
			{ 
				realposition = value;
				this.DoNotify();
			}
		}
		private double realvelocity;

		public double RealVelocity
		{
			get { return realvelocity; }
			set 
			{ 
				realvelocity = value;
				this.DoNotify();
			}
		}
		private double refposition;

		public double RefPosition
		{
			get { return refposition; }
			set 
			{ 
				refposition = value;
				this.DoNotify();
			}
		}
		private double refvelocity;

		public double RefVelocity
		{
			get { return refvelocity; }
			set 
			{ 
				refvelocity = value;
				this.DoNotify();
			}
		}
		private bool runover;

		public bool RunOver
		{
			get { return runover; }
			set { runover = value; }
		}
		private int pulse;

		public int Pulse
		{
			get { return pulse; }
			set 
			{ 
				pulse = value;
				this.DoNotify();
			}
		}
		private Mode axismode;

		public Mode AxisMode
		{
			get { return axismode; }
			set 
			{ 
				axismode = value;
				this.DoNotify();
			}
		}












	}
}
