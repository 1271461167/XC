using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_12_11XiChun.Base
{
    public class AxisPosition
    {
		private double x_position;

		public double XPosition
		{
			get { return x_position; }
			set { x_position = value; }
		}
		private double y_position;

		public double YPosition
		{
			get { return y_position; }
			set { y_position = value; }
		}
		private int id;

		public int ID
		{
			get { return id; }
			set { id = value; }
		}
 
    }
}
