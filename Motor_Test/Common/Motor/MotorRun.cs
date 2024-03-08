using Motor_Test.Common.GTS;
using System.Windows;

namespace Motor_Test.Common.Motor
{
    public class MotorRun
    {
        private static mc.TJogPrm jogPrm = new mc.TJogPrm();
        private static mc.TTrapPrm trapPrm = new mc.TTrapPrm();
        private static mc.THomePrm homePrm = new mc.THomePrm();
        /// <summary>
        /// 电机手动
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="vel">速度 单位mm/s</param>
        /// <param name="acc">加速度 单位pul/ms^2</param>
        /// <param name="dec">减速度 单位pul/ms^2</param>
        public static void Jog(short axis, double vel, double acc, double dec)
        {
            CommandHandler(mc.GT_PrfJog(axis));
            jogPrm.acc = acc;
            jogPrm.dec = dec;
            CommandHandler(mc.GT_SetJogPrm(axis, ref jogPrm));
            CommandHandler(mc.GT_SetVel(axis, vel));
            CommandHandler(mc.GT_Update(1 << (axis - 1)));
        }
        /// <summary>
        /// 平滑停止
        /// </summary>
        /// <param name="axis">轴号</param>
        public static void SmoothStop(short axis)
        {
            CommandHandler(mc.GT_Stop(1 << (axis - 1), 0)); 
        }
        /// <summary>
        /// 急停
        /// </summary>
        /// <param name="axis">轴号</param>
        public static void EmergencyStop(short axis)
        {
            CommandHandler(mc.GT_Stop(1 << (axis - 1), 1 << (axis - 1)));
        }
        /// <summary>
        /// 指令执行结果
        /// </summary>
        /// <param name="nRts">指令执行结果</param>
        /// <returns></returns>
        private static void CommandHandler(short nRts)
        {
            switch (nRts)
            {
                case 0:                   
                    break;
                case 1:
                    MessageBox.Show("指令执行错误");
                    break;
                case 2:
                    MessageBox.Show("license 不支持");
                    break;
                case 7:
                    MessageBox.Show("指令参数错误");
                    break;
                case 8:
                    MessageBox.Show("不支持该指令");
                    break;
                case -1:
                case -2:
                case -3:
                case -4:
                case -5:
                    MessageBox.Show("主机和运动控制器通讯失败");
                    break;
                case -7:
                    MessageBox.Show("运动控制器没有响应");
                    break;
                case -8:
                    MessageBox.Show("多线程资源忙");
                    break;
                default:
                    MessageBox.Show("未知错误");
                    break;

            }

        }
    }
}
