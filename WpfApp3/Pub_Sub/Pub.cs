using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp3.Pub_Sub
{
    public class Pub
    {
        private Dictionary<string, EventHandler<EventArgs>> subs = new Dictionary<string, EventHandler<EventArgs>>();
        public void subscribe(string str, EventHandler<EventArgs> e)
        {
            if (!subs.ContainsKey(str))
                subs.Add(str, delegate { });
            subs[str] += e;
        }
        public void publish(string str, EventArgs args)
        {
            if (subs.ContainsKey(str))
            {
                EventHandler<EventArgs> eventHandler = subs[str];
                eventHandler?.Invoke(this, args);
            }
        }
    }
}
