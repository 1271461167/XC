using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Pub_Sub
{
    public class MyEventArgs:EventArgs
    {
        public string Value { get; set; }
        public MyEventArgs(string str)
        {
            Value = str;
        }
    }
}
