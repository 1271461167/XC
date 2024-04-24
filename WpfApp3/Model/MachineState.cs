using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Model
{
    public class MachineState
    {
        public TimeSpan BootTime { get; set; }      //开机时间
        public TimeSpan EffectiveTime { get; set; } //有效运行时间
        public DateTime RunTime { get; set; }       //日期
    }
}
