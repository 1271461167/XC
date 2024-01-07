using _2023_12_11XiChun.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _2023_12_11XiChun.Model
{
    public class HomePageModel:NotifyBase
    {
		private string errmessage;

		public string ErrMessage
		{
			get { return errmessage; }
			set 
			{ 
				errmessage = value;
				this.DoNotify();
			}
		}
        private ImageSource myImage = null;
        public ImageSource MyImage
        {
            get { return myImage; }
            set
            {
                myImage = value;
                this.DoNotify();
            }
        }


    }
}
