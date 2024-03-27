using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Common.GTS
{
    public class GTS : IRunController
    {
        private  mc.TJogPrm jogPrm = new mc.TJogPrm();
        private  mc.TTrapPrm trapPrm = new mc.TTrapPrm();
        private  mc.THomePrm homePrm = new mc.THomePrm();

        private GTS() { }
        private static GTS gTS = null;
        public static GTS GetGTS() 
        {
            if(gTS==null)
                gTS = new GTS();
            return gTS;
        }
        public void Command(short s)
        {
            if (s != 0)
            {
                throw new Exception(ReturnInfo(s));
            }
        }

        public void Home()
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Jog(short axis, double vel, double acc, double dec)
        {
            try
            {
                Command(mc.GT_PrfJog(axis));
                jogPrm.acc = acc;
                jogPrm.dec = dec;
                Command(mc.GT_SetJogPrm(axis, ref jogPrm));
                Command(mc.GT_SetVel(axis, vel));
                Command(mc.GT_Update(1 << (axis - 1)));
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
            }

        }

        public string ReturnInfo(short s)
        {
            switch (s)
            {
                case 1:
                    return "GooGol:  " + "指令执行错误";
                case 2:
                    return "GooGol:  " + "license 不支持";
                case 7:
                    return "GooGol:  " + "指令参数错误";
                case 8:
                    return "GooGol:  " + "不支持该指令";
                case -1:
                case -2:
                case -3:
                case -4:
                case -5:
                    return "GooGol:  " + "主机和运动控制器通讯失败";
                case -6:
                    return "GooGol:  " + "打开控制器失败";
                case -7:
                    return "GooGol:  " + "运动控制器没有响应";
                case -8:
                    return "GooGol:  " + "多线程资源忙";
                default:
                    return "GooGol:  " + "未知错误";
            }
        }

        public void Stop(short axis)
        {
            try
            {
                Command(mc.GT_Stop(1<<(axis-1),0));
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        public void Trap(short axis, int pos, double vel, double acc, double dec,short smoothtime)
        {
            try
            {
                Command(mc.GT_PrfTrap(axis));
                trapPrm.acc = acc;
                trapPrm.dec = dec;
                trapPrm.smoothTime = smoothtime;
                Command(mc.GT_SetTrapPrm(axis, ref trapPrm));
                Command(mc.GT_SetPos(axis, pos));
                Command(mc.GT_Update(1 << (axis - 1)));
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }

        }

        public void GetSts(short axis, out int AxisState)
        {
            uint clk;
            try
            {
                Command(mc.GT_GetSts(axis, out AxisState, 1, out clk));
            }
            catch (Exception e)
            {
                AxisState = 0;
                Log.Error(e.Message);
            }
        }
    }
}
