﻿using _2023_12_11XiChun.Base;
using _2023_12_11XiChun.Model;
using _2023_12_11XiChun.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace _2023_12_11XiChun.ViewModel
{
    public class MotorPageViewModel : CommandBase
    {
        short sRtn;
        public static System.Timers.Timer _timer = null;
        public CommandBase XEnableCommand { get; set; } = new CommandBase();//X轴使能
        public CommandBase XLimitCommand { get; set; } = new CommandBase();//X轴限位开关
        public CommandBase XClrAlarmCommand { get; set; } = new CommandBase();//X轴清报警
        public CommandBase XJogNDownCommand { get; set; } = new CommandBase();//X轴JOG-按下 
        public CommandBase XJogNUpCommand { get; set; } = new CommandBase();//X轴JOG-抬起
        public CommandBase XModeCommand { get; set; } = new CommandBase();
        public CommandBase XAutoCommand { get; set; } = new CommandBase();
        public CommandBase XAutoStopCommand { get; set; } = new CommandBase();

        public ParameterModel Parameter { get; set; } = ParameterModel.GetParameter();

        private static gts.mc.TJogPrm jog;
        private static gts.mc.TTrapPrm trap;


        public MotorPageViewModel()
        {
            if (_timer == null)
            {
                InitCard();
                _timer = new System.Timers.Timer();
                _timer.AutoReset = true;
                _timer.Interval = 2000;
                _timer.Elapsed += OnTmrTrg;
                _timer.Start();
            }
            NowPage now = NowPage.GetInstance();
            now.PageNow = NowPage.nowPage.MotorPL;
            XEnableCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            XEnableCommand.DoExecute = new Action<object>((obj) => { XEnable(obj); });
            XLimitCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            XLimitCommand.DoExecute = new Action<object>((obj) => { XLimit(obj); });
            XClrAlarmCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            XClrAlarmCommand.DoExecute = new Action<object>((obj) => { XClrAlarm(); });
            XJogNDownCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            XJogNDownCommand.DoExecute = new Action<object>((obj) => { XJogNDown(); });
            XJogNUpCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            XJogNUpCommand.DoExecute = new Action<object>((obj) => { XJogNUp(); });
            XModeCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            XModeCommand.DoExecute = new Action<object>((obj) => { XModeChange(obj); });
            XAutoCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            XAutoCommand.DoExecute = new Action<object>((obj) => { XAutoRun(); });
            XAutoStopCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            XAutoStopCommand.DoExecute = new Action<object>((obj) => { gts.mc.GT_Stop(1, 0); });
        }

        private void XAutoRun()
        {
            double vel = Parameter.XMotor.RefVelocity * Parameter.XMotor.Pulse / 1000;
            int pos = int.Parse((Parameter.XMotor.RefPosition * Parameter.XMotor.Pulse).ToString());
            trap.acc = 0.5;
            trap.dec = 0.5;
            trap.smoothTime = 0;
            trap.velStart = 0;
            gts.mc.GT_SetTrapPrm(1, ref trap);//设置点位运动参数
            gts.mc.GT_SetVel(1, vel);//设置目标速度
            gts.mc.GT_SetPos(1, pos);//设置目标位置
            gts.mc.GT_Update(1);//更新轴运动

        }

        private void XModeChange(object obj)
        {
            Button button = (Button)obj;
            if (Parameter.XMotor.AxisMode == Motor.Mode.AUTO)
            {
                Parameter.XMotor.AxisMode = Motor.Mode.JOG;
                button.Content = "点动";
                sRtn = gts.mc.GT_PrfJog(1);//设置为jog模式
                gts.mc.commandhandler("GT_PrfJog", sRtn);
            }
            else
            {
                Parameter.XMotor.AxisMode = Motor.Mode.AUTO;
                button.Content = "自动";
                sRtn = gts.mc.GT_PrfTrap(1);
            }
        }

        private void OnTmrTrg(object sender, ElapsedEventArgs e)
        {
            uint clk;
            int XAxisState;
            double dRealPos, dRealVel;
            gts.mc.GT_GetSts(1, out XAxisState, 1, out clk);
            Parameter.XMotor.Alarm = ((XAxisState & 0x02) == 1) ? true : false;
            Parameter.XMotor.Enable = ((XAxisState & 0x0200) == 1) ? true : false;
            Parameter.XMotor.OPL = ((XAxisState & 0x020) == 1) ? true : false;
            Parameter.XMotor.ONL = ((XAxisState & 0x040) == 1) ? true : false;
            Parameter.XMotor.RunOver = ((XAxisState & 0x0800) == 1) ? true : false;

            sRtn = gts.mc.GT_GetEncPos(1, out dRealPos, 1, out clk);
            gts.mc.commandhandler("GT_GetPrfPos", sRtn);
            Parameter.XMotor.RealPosition = dRealPos;
            sRtn = gts.mc.GT_GetEncVel(1, out dRealVel, 1, out clk);
            gts.mc.commandhandler("GT_GetPrfVel", sRtn);
            Parameter.XMotor.RealVelocity = dRealVel;
        }

        private void XJogNDown()
        {
            double vel = Parameter.XMotor.RefVelocity * Parameter.XMotor.Pulse / 1000;
            jog.acc = 0.5;
            jog.dec = 0.5;

            gts.mc.GT_SetJogPrm(1, ref jog);//设置jog运动参数

            gts.mc.GT_SetVel(1, vel);//设置目标速度

            gts.mc.GT_Update(1);//更新轴运动

        }

        private void XJogNUp()
        {
            gts.mc.GT_Stop(1, 0);
        }

        private void XClrAlarm()
        {
            gts.mc.GT_ClrSts(1, 4);
        }

        private void XLimit(object obj)
        {
            Button button = obj as Button;
            if (!Parameter.XMotor.Limit)
            {
                gts.mc.GT_LmtsOn(1, -1);//限位开
                button.Content = "无限位";
                Parameter.XMotor.Limit = true;
            }
            else
            {
                gts.mc.GT_LmtsOff(1, -1);//限位关
                button.Content = "限位";
                Parameter.XMotor.Limit = false;
            }

        }

        private async void XEnable(object obj)
        {
            Button button = obj as Button;
            if (!Parameter.XMotor.Enable)
            {
                gts.mc.GT_AxisOn(1);//上伺服
                await Task.Delay(300);
                if (Parameter.XMotor.Enable)
                {
                    button.Content = "断使能";
                }
                //Parameter.XMotor.Enable = true;
            }
            else
            {
                gts.mc.GT_AxisOff(1);//下伺服
                await Task.Delay(300);
                if (!Parameter.XMotor.Enable)
                {
                    button.Content = "使能";
                }
                //Parameter.XMotor.Enable = false;
            }
        }

        /// <summary>
        /// 初始化运动控制卡板卡
        /// </summary>
        private async void InitCard()
        {
            sRtn = gts.mc.GT_Open(0, 1);//打开运动控制卡
            gts.mc.commandhandler("GT_Open", sRtn);
            sRtn = gts.mc.GT_Reset();   //复位控制卡
            gts.mc.commandhandler("GT_Reset", sRtn);

            //gts.mc.GT_LoadConfig("GT800_test.cfg");//加载配置文件

            sRtn = gts.mc.GT_ClrSts(1, 4);//清除各轴报警和限位
            gts.mc.commandhandler("GT_ClrSts", sRtn);

            sRtn = gts.mc.GT_HomeInit();			        // 初始化自动回原点功能
            sRtn = gts.mc.GT_AxisOn(1);					    // 使能轴1
            sRtn = gts.mc.GT_AxisOn(2);					    // 使能轴2			
            sRtn = gts.mc.GT_Home(1, 200000, 10, 0.5, 2000);
            sRtn = gts.mc.GT_Home(2, 200000, 10, 0.5, 3000);
            await Task.Run(() =>
            {
                uint clk;
                int XAxisState, YAxisState;
                do
                {
                    // 读取1轴的状态
                    gts.mc.GT_GetSts(1, out XAxisState, 1, out clk);
                    //读取2轴的状态
                    gts.mc.GT_GetSts(1, out YAxisState, 1, out clk);
                } while (((XAxisState & 0x400) !=0)||((YAxisState& 0x400)!=0));// 等待XY轴规划停止
            });         
            sRtn = gts.mc.GT_ZeroPos(1, 2);
            sRtn = gts.mc.GT_PrfTrap(1);
            sRtn = gts.mc.GT_PrfTrap(2);//设置为点位模式
            Parameter.XMotor.AxisMode = Motor.Mode.AUTO;
            gts.mc.commandhandler("GT_PrfJog", sRtn);
        }

    }
}
