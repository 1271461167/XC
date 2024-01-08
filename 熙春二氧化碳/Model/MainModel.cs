using _2023_12_11XiChun.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_12_11XiChun.Model
{
    public class MainModel:NotifyBase
    {
		private string pagename;

		public string PageName
		{
			get { return pagename; }
			set 
			{
				pagename = value;
				this.DoNotify();
			}
		}

	}
}
