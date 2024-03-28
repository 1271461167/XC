using Motor_Test.Common.GTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Common
{
    public interface IRunController
    {
        #region 读取信息
        /// <summary>
        /// 获取轴的状态
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="AxisState">读取的状态字</param>
        void GetSts(short axis, out int AxisState);
        /// <summary>
        /// 获取当前轴的误差带设置
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="band">误差带 单位 pul</param>
        /// <param name="time">保持时间</param>
        void GetAxisBand(short axis, out int band, out int time);
        #endregion
        void Init();
        void Jog(short axis,JogPrm jog);
        void Trap(short axis,TrapPrm trap);
        void Home(short axis,HomePrm home);
        void Stop(short axis);
        void Command(short st);
        string ReturnInfo(short s);
        void SetAxisBand(short axis,int band,int time);
        void ZeroPos(short axis);
        void Enable(short axis);
        void ClrSts(short axis, int count);
        void UnEnable(short axis);
    }
}
