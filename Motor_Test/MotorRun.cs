using Motor_Test.Common.GTS;
using System.Windows;

namespace Motor_Test.Common.Motor
{
    public static class MotorRun
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
            //GtsHandler.CommandHandler(mc.GT_PrfJog(axis));
            //jogPrm.acc = acc;
            //jogPrm.dec = dec;
            //GtsHandler.CommandHandler(mc.GT_SetJogPrm(axis, ref jogPrm));
            //GtsHandler.CommandHandler(mc.GT_SetVel(axis, vel));
            //GtsHandler.CommandHandler(mc.GT_Update(1 << (axis - 1)));

        }
        /// <summary>
        /// 平滑停止
        /// </summary>
        /// <param name="axis">轴号</param>
        public static void SmoothStop(short axis)
        {
           // GtsHandler.CommandHandler(mc.GT_Stop(1 << (axis - 1), 0));
        }
        /// <summary>
        /// 急停
        /// </summary>
        /// <param name="axis">轴号</param>
        public static void EmergencyStop(short axis)
        {
           // GtsHandler.CommandHandler(mc.GT_Stop(1 << (axis - 1), 1 << (axis - 1)));
        }
        public static void Trap(short axis, double vel, int pos, double acc, double dec, short smoothtime)
        {
            //GtsHandler.CommandHandler(mc.GT_PrfTrap(axis));
            //trapPrm.acc = acc;
            //trapPrm.dec = dec;
            //trapPrm.smoothTime = smoothtime;
            //GtsHandler.CommandHandler(mc.GT_SetTrapPrm(axis, ref trapPrm));
            //GtsHandler.CommandHandler(mc.GT_SetPos(axis, pos));
            //GtsHandler.CommandHandler(mc.GT_Update(1 << (axis - 1)));
        }
        public static void SearchHome(short axis,mc.THomePrm home)
        {
            homePrm.mode = home.mode;
            homePrm.moveDir = home.moveDir;
            homePrm.indexDir = home.indexDir;
            homePrm.edge = home.edge;
            homePrm.velHigh = home.velHigh;
            homePrm.velLow = home.velLow;
            homePrm.acc = home.acc;
            homePrm.dec = home.dec;
            homePrm.searchHomeDistance = home.searchHomeDistance;
            homePrm.searchIndexDistance = home.searchIndexDistance;
            homePrm.escapeStep = home.escapeStep;

        }

    }
}
