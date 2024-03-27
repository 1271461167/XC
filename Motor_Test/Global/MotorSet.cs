using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Common.Motor
{
    public class MotorSet
    {
		private int axis;

		public int Axis
		{
			get { return axis; }
			set 
			{ 
				axis = value;
			}
		}
		private int puls;

		public int Puls
		{
			get { return puls; }
			set 
			{
				puls = value;
                CreateIni.WriteIni("Axis" + this.Axis.ToString(), "Puls", this.Puls.ToString());
            }
		}
		private int band;

		public int Band
		{
			get { return band; }
			set 
			{ 
				band = value;
                CreateIni.WriteIni("Axis" + this.Axis.ToString(), "Band", this.Band.ToString());
            }
		}
		private int time;

		public int Time
		{
			get { return time; }
			set 
			{ 
				time = value;
                CreateIni.WriteIni("Axis" + this.Axis.ToString(), "Time", this.Time.ToString());
            }
		}


	}
}
