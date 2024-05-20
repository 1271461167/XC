using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3.Pub_Sub
{
    public class Sub
    {
        //private string _name;
        //public Sub(string name)
        //{
        //    _name = name;
        //}
        public  void Notify(object sender, EventArgs args)
        {
            MyEventArgs myEvent = args as MyEventArgs;
            MessageBox.Show(myEvent.Value);
        }
    }
}
