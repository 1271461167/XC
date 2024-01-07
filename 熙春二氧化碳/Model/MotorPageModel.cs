using _2023_12_11XiChun.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_12_11XiChun.Model
{
    public class MotorPageModel:NotifyBase
    {
		private string message;

		public string Message
		{
			get { return message; }
			set { message = value;this.DoNotify(); }
		}

	}
}
