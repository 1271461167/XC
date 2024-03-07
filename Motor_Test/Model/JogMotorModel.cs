using Motor_Test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Model
{
    public class JogMotorModel:CommandAndNotifyBase
    {
		private int axis;

		public int Axis
		{
			get { return axis; }
			set { axis = value; this.DoNotify(); }
		}
		private double vel;

		public double Vel
		{
			get { return vel; }
			set { vel = value;this.DoNotify(); }
		}

		private double acc;

		public double Acc
		{
			get { return acc; }
			set { acc = value;this.DoNotify(); }
		}
		private double dec;

		public double Dec
		{
			get { return dec; }
			set { dec = value;this.DoNotify(); }
		}
		private int pul;

		public int Pul
		{
			get { return pul; }
			set { pul = value;this.DoNotify(); }
		}


	}
}
