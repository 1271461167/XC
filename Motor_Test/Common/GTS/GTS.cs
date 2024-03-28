using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Motor_Test.Common.GTS
{
    public class GTS : IRunController
    {
        private static readonly object _lock = new object();
        private mc.TJogPrm jogPrm = new mc.TJogPrm();
        private mc.TTrapPrm trapPrm = new mc.TTrapPrm();
        private mc.THomePrm homePrm = new mc.THomePrm();
        private mc.THomeStatus homeStatus = new mc.THomeStatus();

        private GTS() { }
        private static GTS gTS = null;


        public static GTS GetGTS()
        {
            if (gTS == null)
            {
                lock (_lock)
                {
                    if (gTS == null)
                        gTS = new GTS();
                }
            }
            return gTS;
        }
        public void Command(short s)
        {
            if (s != 0)
            {
                throw new Exception(ReturnInfo(s));
            }
        }

        public async void Home(short axis, HomePrm home)
        {
            ZeroPos(axis);
            homePrm.mode = (short)home.Mode;
            homePrm.moveDir = (short)home.MoveDir;
            homePrm.indexDir = (short)home.IndexDir;
            homePrm.edge= (short)home.Edge;
            homePrm.velHigh = (short)home.VelHigh;
            homePrm.velLow = (short)home.VelLow;
            homePrm.acc= (short)home.Acc;
            homePrm.dec= (short)home.Dec;
            homePrm.searchHomeDistance = (short)home.SearchHomeDistance;
            homePrm.searchIndexDistance = (short)home.SearchIndexDistance;
            homePrm.escapeStep= (short)home.EscapeStep;
            try
            {
                Command(mc.GT_GoHome(axis, ref homePrm));
                await Task.Run(() => 
                {
                    do
                    {
                        Command(mc.GT_GetHomeStatus(axis, out homeStatus));
                    }while (homeStatus.run!=0);
                    Thread.Sleep(200);
                    ZeroPos(axis);
                    Thread.Sleep(100);
                    Command(mc.GT_SynchAxisPos(1<<(axis - 1)));
                });                
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
            }
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Jog(short axis, JogPrm jog)
        {
            try
            {
                Command(mc.GT_PrfJog(axis));
                jogPrm.acc = jog.Acc;
                jogPrm.dec = jog.Dec;
                Command(mc.GT_SetJogPrm(axis, ref jogPrm));
                Command(mc.GT_SetVel(axis, jog.Vel));
                Command(mc.GT_Update(1 << (axis - 1)));
            }
            catch (Exception e)
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
                Command(mc.GT_Stop(1 << (axis - 1), 0));
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        public void Trap(short axis,TrapPrm trap)
        {
            try
            {
                //Command(mc.GT_PrfTrap(axis));
                //trapPrm.acc = acc;
                //trapPrm.dec = dec;
                //trapPrm.smoothTime = smoothtime;
                //Command(mc.GT_SetTrapPrm(axis, ref trapPrm));
                //Command(mc.GT_SetVel(axis, vel));
                //Command(mc.GT_SetPos(axis, pos));
                //Command(mc.GT_Update(1 << (axis - 1)));
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

        public void SetAxisBand(short axis, int band, int time)
        {
            try
            {
                Command(mc.GT_SetAxisBand(axis, band, time));
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        public void GetAxisBand(short axis, out int band, out int time)
        {
            try
            {
                Command(mc.GT_GetAxisBand(axis, out band, out time));
            }
            catch (Exception e)
            {
                band = 0;
                time = 0;
                Log.Error(e.Message);
            }
        }

        public void ZeroPos(short axis)
        {
            try
            {
                Command(mc.GT_ZeroPos(axis,1)); ;
            }
            catch (Exception e)
            {               
                Log.Error(e.Message);
            }
        }

        public void Enable(short axis)
        {
            try
            {
                Command(mc.GT_AxisOn(axis)); ;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        public void ClrSts(short axis, int count)
        {
            throw new NotImplementedException();
        }

        public void UnEnable(short axis)
        {
            try
            {
                Command(mc.GT_AxisOff(axis)); ;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }
    }
}
