using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Common
{
    public interface IRunController
    {
        event EventHandler RunError;
        void Init();
        void Jog(short axis,double vel,double acc,double dec);
        void Trap(short axis,int pos,double vel,double acc,double dec,short smoothtime);
        void Home();
        void Stop(short axis);
        void Command(short st);
        string ReturnInfo(short s);
        void GetSts(short axis,out int AxisState);
    }
}
