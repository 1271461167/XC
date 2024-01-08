using _2023_12_11XiChun.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _2023_12_11XiChun.Model
{
    public class MarkPageModel:NotifyBase
    {
        private string message;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                this.DoNotify();
            }
        }
        private string filename;

        public string FileName
        {
            get { return filename; }
            set 
            { 
                filename = value;
                this.DoNotify();
            }
        }
        private ImageSource myImage;

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
