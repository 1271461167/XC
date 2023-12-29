using _2023_12_11XiChun.Base;
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
        public static MotorPageModel MotorModel { get; set; } = new MotorPageModel();
        public CommandBase XEnableCommand { get; set; } = new CommandBase();//X轴使能
        public CommandBase XLimitCommand { get; set; } = new CommandBase();//X轴限位开关
        public CommandBase XClrAlarmCommand { get; set; } = new CommandBase();//X轴清报警
        public CommandBase XJogNDownCommand { get; set; } = new CommandBase();//X轴JOG-按下 
        public CommandBase XJogNUpCommand { get; set; } = new CommandBase();//X轴JOG-抬起
        public CommandBase XJogPDownCommand { get; set; } = new CommandBase();//X轴JOG-按下 
        public CommandBase XJogPUpCommand { get; set; } = new CommandBase();//X轴JOG-抬起
        public CommandBase XModeCommand { get; set; } = new CommandBase();//X轴手自动切换
        public CommandBase XAutoCommand { get; set; } = new CommandBase();//X轴自动模式运行
        public CommandBase XAutoStopCommand { get; set; } = new CommandBase();//X轴自动模式停止

        public CommandBase XAutoGoHomeCommand { get; set; } = new CommandBase();//X轴自动回原点
        public CommandBase YEnableCommand { get; set; } = new CommandBase();//Y轴使能
        public CommandBase YLimitCommand { get; set; } = new CommandBase();//Y轴限位开关
        public CommandBase YClrAlarmCommand { get; set; } = new CommandBase();//Y轴清报警
        public CommandBase YJogNDownCommand { get; set; } = new CommandBase();//Y轴JOG-按下 
        public CommandBase YJogNUpCommand { get; set; } = new CommandBase();//Y轴JOG-抬起
        public CommandBase YJogPDownCommand { get; set; } = new CommandBase();//Y轴JOG-按下 
        public CommandBase YJogPUpCommand { get; set; } = new CommandBase();//Y轴JOG-抬起
        public CommandBase YModeCommand { get; set; } = new CommandBase();//Y轴手自动切换
        public CommandBase YAutoCommand { get; set; } = new CommandBase();//Y轴自动模式运行
        public CommandBase YAutoStopCommand { get; set; } = new CommandBase();//Y轴自动模式停止
        public CommandBase YAutoGoHomeCommand { get; set; } = new CommandBase();//Y轴自动回原点
        public ParameterModel Parameter { get; set; } = ParameterModel.GetParameter();

        private static gts.mc.TJogPrm jog;
        private static gts.mc.TTrapPrm trap;
        private static gts.mc.THomePrm home;
        private static gts.mc.THomeStatus homeStatus;
        public MotorPageViewModel()
        {
            if (_timer == null)
            {
                InitCard();
                _timer = new System.Timers.Timer();
                _timer.AutoReset = true;
                _timer.Interval = 200;
                _timer.Elapsed += OnTmrTrg;
                _timer.Start();
            }
            NowPage now = NowPage.GetInstance();
            now.PageNow = NowPage.nowPage.MotorPL;
            #region X轴
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
            XJogPDownCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            XJogPDownCommand.DoExecute = new Action<object>((obj) => { XJogPDown(); });
            XJogPUpCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            XJogPUpCommand.DoExecute = new Action<object>((obj) => { XJogPUp(); });
            XAutoGoHomeCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            XAutoGoHomeCommand.DoExecute = new Action<object>((obj) => { XGoHome(); });
            #endregion

            #region Y轴
            YEnableCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            YEnableCommand.DoExecute = new Action<object>((obj) => { YEnable(obj); });
            YLimitCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            YLimitCommand.DoExecute = new Action<object>((obj) => { YLimit(obj); });
            YClrAlarmCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            YClrAlarmCommand.DoExecute = new Action<object>((obj) => { YClrAlarm(); });
            YJogNDownCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            YJogNDownCommand.DoExecute = new Action<object>((obj) => { YJogNDown(); });
            YJogNUpCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            YJogNUpCommand.DoExecute = new Action<object>((obj) => { YJogNUp(); });
            YModeCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            YModeCommand.DoExecute = new Action<object>((obj) => { YModeChange(obj); });
            YAutoCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            YAutoCommand.DoExecute = new Action<object>((obj) => { YAutoRun(); });
            YAutoStopCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            YAutoStopCommand.DoExecute = new Action<object>((obj) => { gts.mc.GT_Stop(1 << 1, 0); });
            YJogPDownCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            YJogPDownCommand.DoExecute = new Action<object>((obj) => { YJogPDown(); });
            YJogPUpCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            YJogPUpCommand.DoExecute = new Action<object>((obj) => { YJogPUp(); });
            YAutoGoHomeCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            YAutoGoHomeCommand.DoExecute = new Action<object>((obj) => { YGoHome(); });
            #endregion
        }

        private async void YGoHome()
        {
            home.mode = gts.mc.HOME_MODE_HOME_INDEX;
            home.moveDir = 1;
            home.indexDir = 1;
            home.edge = 0;
            home.velHigh = 2.5;
            home.velLow = 0.5;
            home.acc = 0.1;
            home.dec = 0.1;
            home.searchHomeDistance = 150000;
            home.searchIndexDistance = 15000;
            home.escapeStep = 1000;
            sRtn = gts.mc.GT_GoHome(2, ref home);

            await Task.Run(() =>
            {
                do
                {
                    // 读取2轴的状态
                    sRtn = gts.mc.GT_GetHomeStatus(2, out homeStatus);
                    if (sRtn != 0)
                    {
                        MotorModel.Message = "GetHome2 Fail:" + sRtn.ToString();
                        return;
                    }
                } while (homeStatus.run != 0);// 等待XY轴规划停止
            });
            await Task.Delay(200);
            sRtn = gts.mc.GT_ZeroPos(2, 1);
            if (sRtn != 0)
            {
                MotorModel.Message = "SetZero2 Fail:" + sRtn.ToString();
                return;
            }
        }

        private async void XGoHome()
        {
            home.mode = gts.mc.HOME_MODE_HOME_INDEX;
            home.moveDir = 1;
            home.indexDir = 1;
            home.edge = 0;
            home.velHigh = 2.5;
            home.velLow = 0.5;
            home.acc = 0.1;
            home.dec = 0.1;
            home.searchHomeDistance = 150000;
            home.searchIndexDistance = 15000;
            home.escapeStep = 1000;
            sRtn = gts.mc.GT_GoHome(1, ref home);

            await Task.Run(() =>
            {
                do
                {
                    // 读取2轴的状态
                    sRtn = gts.mc.GT_GetHomeStatus(1, out homeStatus);
                    if (sRtn != 0)
                    {
                        MotorModel.Message = "GetHome1 Fail:" + sRtn.ToString();
                        return;
                    }
                } while (homeStatus.run != 0);// 等待XY轴规划停止
            });
            await Task.Delay(200);
            sRtn = gts.mc.GT_ZeroPos(1, 1);
            if (sRtn != 0)
            {
                MotorModel.Message = "SetZero1 Fail:" + sRtn.ToString();
                return;
            }
        }

        private void YJogPUp()
        {
            sRtn = gts.mc.GT_Stop(1 << 1, 0);
            if (sRtn != 0)
            {
                MotorModel.Message = "Stop2 Fail:" + sRtn.ToString();
                return;
            }
        }

        private void YJogPDown()
        {
            double vel = Parameter.YMotor.RefVelocity * Parameter.XMotor.Pulse / 1000;
            jog.acc = 0.1;
            jog.dec = 0.1;

            sRtn = gts.mc.GT_SetJogPrm(2, ref jog);//设置jog运动参数
            if (sRtn != 0)
            {
                MotorModel.Message = "SetJogPrm2 Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_SetVel(2, -vel);//设置目标速度
            if (sRtn != 0)
            {
                MotorModel.Message = "SetVel2 Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_Update(1 << 1);//更新轴运动
            if (sRtn != 0)
            {
                MotorModel.Message = "Run2 Fail:" + sRtn.ToString();
                return;
            }
        }

        private void YAutoRun()
        {
            double vel = Parameter.YMotor.RefVelocity * Parameter.YMotor.Pulse / 1000;
            int pos = int.Parse((Parameter.YMotor.RefPosition * Parameter.YMotor.Pulse).ToString());
            trap.acc = 0.5;
            trap.dec = 0.5;
            trap.smoothTime = 25;
            trap.velStart = 0;
            sRtn = gts.mc.GT_SetTrapPrm(2, ref trap);//设置点位运动参数
            if (sRtn != 0)
            {
                MotorModel.Message = "SetTrapPrm2 Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_SetVel(2, vel);//设置目标速度
            if (sRtn != 0)
            {
                MotorModel.Message = "SetVel2 Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_SetPos(2, pos);//设置目标位置
            if (sRtn != 0)
            {
                MotorModel.Message = "SetPos2 Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_Update(1 << 1);//更新轴运动
            if (sRtn != 0)
            {
                MotorModel.Message = "TrapRun2 Fail:" + sRtn.ToString();
                return;
            }
        }

        private void YModeChange(object obj)
        {
            Button button = (Button)obj;
            if (Parameter.YMotor.AxisMode == Motor.Mode.AUTO)
            {
                Parameter.YMotor.AxisMode = Motor.Mode.JOG;
                button.Content = "点动";
                sRtn = gts.mc.GT_PrfJog(2);//设置为jog模式
                if (sRtn != 0)
                {
                    MotorModel.Message = "PrfJOG2 Fail:" + sRtn.ToString();
                    return;
                }
            }
            else
            {
                Parameter.YMotor.AxisMode = Motor.Mode.AUTO;
                button.Content = "自动";
                sRtn = gts.mc.GT_PrfTrap(2);
                if (sRtn != 0)
                {
                    MotorModel.Message = "PrfTrap2 Fail:" + sRtn.ToString();
                    return;
                }
            }
        }

        private void YJogNUp()
        {
            sRtn = gts.mc.GT_Stop(1 << 1, 0);
            if (sRtn != 0)
            {
                MotorModel.Message = "Stop2 Fail:" + sRtn.ToString();
                return;
            }
        }

        private void YJogNDown()
        {
            double vel = Parameter.YMotor.RefVelocity * Parameter.YMotor.Pulse / 1000;
            jog.acc = 0.1;
            jog.dec = 0.1;

            sRtn = gts.mc.GT_SetJogPrm(2, ref jog);//设置jog运动参数
            if (sRtn != 0)
            {
                MotorModel.Message = "SetJogPrm2 Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_SetVel(2, vel);//设置目标速度
            if (sRtn != 0)
            {
                MotorModel.Message = "SetVel2 Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_Update(1 << 1);//更新轴运动
            if (sRtn != 0)
            {
                MotorModel.Message = "Run2 Fail:" + sRtn.ToString();
                return;
            }
        }

        private void YClrAlarm()
        {
            gts.mc.GT_ClrSts(1, 4);
        }

        private async void YLimit(object obj)
        {
            Button button = obj as Button;
            if (!Parameter.YMotor.Limit)
            {
                gts.mc.GT_LmtsOn(2, -1);//限位开
                await Task.Delay(300);
                if (Parameter.YMotor.Limit)
                {
                    button.Content = "限位";
                }
            }
            else
            {
                gts.mc.GT_LmtsOff(2, -1);//限位关
                await Task.Delay(300);
                if (!Parameter.YMotor.Limit)
                {
                    button.Content = "无限位";
                }
            }
        }

        private async void YEnable(object obj)
        {
            Button button = obj as Button;
            if (!Parameter.YMotor.Enable)
            {
                sRtn = gts.mc.GT_AxisOn(2);//上伺服
                if (sRtn != 0)
                {
                    MotorModel.Message = "Enable2 Fail:" + sRtn.ToString();
                    return;
                }
                await Task.Delay(300);
                if (Parameter.YMotor.Enable)
                {
                    button.Content = "断使能";
                }
                //Parameter.XMotor.Enable = true;
            }
            else
            {
                sRtn = gts.mc.GT_AxisOff(2);//下伺服
                if (sRtn != 0)
                {
                    MotorModel.Message = "DisEnable2 Fail:" + sRtn.ToString();
                    return;
                }
                await Task.Delay(300);
                if (!Parameter.YMotor.Enable)
                {
                    button.Content = "使能";
                }
                //Parameter.XMotor.Enable = false;
            }
        }

        private void XJogPUp()
        {
            sRtn = gts.mc.GT_Stop(1, 0);
            if (sRtn != 0)
            {
                MotorModel.Message = "Stop Fail:" + sRtn.ToString();
                return;
            }
        }

        private void XJogPDown()
        {
            double vel = Parameter.XMotor.RefVelocity * Parameter.YMotor.Pulse / 1000;
            jog.acc = 0.1;
            jog.dec = 0.1;

            sRtn = gts.mc.GT_SetJogPrm(1, ref jog);//设置jog运动参数
            if (sRtn != 0)
            {
                MotorModel.Message = "SetJogPrm Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_SetVel(1, -vel);//设置目标速度
            if (sRtn != 0)
            {
                MotorModel.Message = "SetVel Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_Update(1);//更新轴运动
            if (sRtn != 0)
            {
                MotorModel.Message = "Run Fail:" + sRtn.ToString();
                return;
            }
        }

        private void XAutoRun()
        {
            double vel = Parameter.XMotor.RefVelocity * Parameter.XMotor.Pulse / 1000;
            int pos = int.Parse((Parameter.XMotor.RefPosition * Parameter.XMotor.Pulse).ToString());
            trap.acc = 0.5;
            trap.dec = 0.5;
            trap.smoothTime = 25;
            trap.velStart = 0;
            sRtn = gts.mc.GT_SetTrapPrm(1, ref trap);//设置点位运动参数
            if (sRtn != 0)
            {
                MotorModel.Message = "SetTrapPrm Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_SetVel(1, vel);//设置目标速度
            if (sRtn != 0)
            {
                MotorModel.Message = "SetVel Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_SetPos(1, pos);//设置目标位置
            if (sRtn != 0)
            {
                MotorModel.Message = "SetPos Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_Update(1);//更新轴运动
            if (sRtn != 0)
            {
                MotorModel.Message = "TrapRun Fail:" + sRtn.ToString();
                return;
            }
        }

        private void XModeChange(object obj)
        {
            Button button = (Button)obj;
            if (Parameter.XMotor.AxisMode == Motor.Mode.AUTO)
            {
                Parameter.XMotor.AxisMode = Motor.Mode.JOG;
                button.Content = "点动";
                sRtn = gts.mc.GT_PrfJog(1);//设置为jog模式
                if (sRtn != 0)
                {
                    MotorModel.Message = "PrfJOG Fail:" + sRtn.ToString();
                    return;
                }
            }
            else
            {
                Parameter.XMotor.AxisMode = Motor.Mode.AUTO;
                button.Content = "自动";
                sRtn = gts.mc.GT_PrfTrap(1);
                if (sRtn != 0)
                {
                    MotorModel.Message = "PrfTrap Fail:" + sRtn.ToString();
                    return;
                }
            }
        }

        private void OnTmrTrg(object sender, ElapsedEventArgs e)
        {
            uint clk;
            int XAxisState, YAxisState;
            double dRealPos, dRealVel;
            sRtn = gts.mc.GT_GetSts(1, out XAxisState, 1, out clk);
            if (sRtn != 0)
            {
                MotorModel.Message = "GetSts1 Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_GetSts(2, out YAxisState, 1, out clk);
            if (sRtn != 0)
            {
                MotorModel.Message = "GetSts2 Fail:" + sRtn.ToString();
                return;
            }
            Parameter.XMotor.Alarm = ((XAxisState & 0x02) != 0) ? true : false;
            Parameter.XMotor.Enable = ((XAxisState & 0x0200) != 0) ? true : false;
            Parameter.XMotor.OPL = ((XAxisState & 0x020) != 0) ? true : false;
            Parameter.XMotor.ONL = ((XAxisState & 0x040) != 0) ? true : false;
            Parameter.XMotor.RunOver = ((XAxisState & 0x0800) != 0) ? true : false;

            Parameter.YMotor.Alarm = ((YAxisState & 0x02) != 0) ? true : false;
            Parameter.YMotor.Enable = ((YAxisState & 0x0200) != 0) ? true : false;
            Parameter.YMotor.OPL = ((YAxisState & 0x020) != 0) ? true : false;
            Parameter.YMotor.ONL = ((YAxisState & 0x040) != 0) ? true : false;
            Parameter.YMotor.RunOver = ((YAxisState & 0x0800) != 0) ? true : false;

            sRtn = gts.mc.GT_GetEncPos(1, out dRealPos, 1, out clk);
            if (sRtn != 0)
            {
                MotorModel.Message = "GetRealPos1 Fail:" + sRtn.ToString();
                return;
            }
            Parameter.XMotor.RealPosition = dRealPos / Parameter.XMotor.Pulse;
            sRtn = gts.mc.GT_GetEncVel(1, out dRealVel, 1, out clk);
            if (sRtn != 0)
            {
                MotorModel.Message = "GetRealV1 Fail:" + sRtn.ToString();
                return;
            }
            Parameter.XMotor.RealVelocity = dRealVel * 1000 / Parameter.XMotor.Pulse;

            sRtn = gts.mc.GT_GetEncPos(2, out dRealPos, 1, out clk);
            if (sRtn != 0)
            {
                MotorModel.Message = "GetRealPos2 Fail:" + sRtn.ToString();
                return;
            }
            Parameter.YMotor.RealPosition = dRealPos / Parameter.YMotor.Pulse;
            sRtn = gts.mc.GT_GetEncVel(2, out dRealVel, 1, out clk);
            if (sRtn != 0)
            {
                MotorModel.Message = "GetRealV2 Fail:" + sRtn.ToString();
                return;
            }
            Parameter.YMotor.RealVelocity = dRealVel * 1000 / Parameter.XMotor.Pulse;
        }

        private void XJogNDown()
        {
            double vel = Parameter.XMotor.RefVelocity * Parameter.XMotor.Pulse / 1000;
            jog.acc = 0.1;
            jog.dec = 0.1;

            sRtn = gts.mc.GT_SetJogPrm(1, ref jog);//设置jog运动参数
            if (sRtn != 0)
            {
                MotorModel.Message = "SetJogPrm Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_SetVel(1, vel);//设置目标速度
            if (sRtn != 0)
            {
                MotorModel.Message = "SetVel Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_Update(1);//更新轴运动
            if (sRtn != 0)
            {
                MotorModel.Message = "Run Fail:" + sRtn.ToString();
                return;
            }
        }

        private void XJogNUp()
        {
            sRtn = gts.mc.GT_Stop(1, 0);
            if (sRtn != 0)
            {
                MotorModel.Message = "Stop Fail:" + sRtn.ToString();
                return;
            }
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
                sRtn = gts.mc.GT_AxisOn(1);//上伺服
                if (sRtn != 0)
                {
                    MotorModel.Message = "Enable Fail:" + sRtn.ToString();
                    return;
                }
                await Task.Delay(300);
                if (Parameter.XMotor.Enable)
                {
                    button.Content = "断使能";
                }
                //Parameter.XMotor.Enable = true;
            }
            else
            {
                sRtn = gts.mc.GT_AxisOff(1);//下伺服
                if (sRtn != 0)
                {
                    MotorModel.Message = "DisEnable Fail:" + sRtn.ToString();
                    return;
                }
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
        private void InitCard()
        {
            sRtn = gts.mc.GT_Open(0, 1);//打开运动控制卡
            if (sRtn != 0)
            {
                MotorModel.Message = "Open Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_Reset();   //复位控制卡
            if (sRtn != 0)
            {
                MotorModel.Message = "Reset Fail:" + sRtn.ToString();
                return;
            }

            sRtn = gts.mc.GT_LoadConfig("GTS800.cfg");//加载配置文件
            if (sRtn != 0)
            {
                MotorModel.Message = "LoadFile Fail:" + sRtn.ToString();
                return;
            }

            sRtn = gts.mc.GT_ClrSts(1, 4);//清除各轴报警和限位
            if (sRtn != 0)
            {
                MotorModel.Message = "ClrSts Fail:" + sRtn.ToString();
                return;
            }

            //sRtn = gts.mc.GT_HomeInit();			        // 初始化自动回原点功能
            //sRtn = gts.mc.GT_AxisOn(1);					    // 使能轴1
            //sRtn = gts.mc.GT_AxisOn(2);					    // 使能轴2			
            //sRtn = gts.mc.GT_Home(1, 200000, 10, 0.5, 0);
            //sRtn = gts.mc.GT_Home(2, 200000, 10, 0.5, 3000);
            //await Task.Run(() =>
            //{
            //    uint clk;
            //    int XAxisState, YAxisState;
            //    do
            //    {
            //        // 读取1轴的状态
            //        gts.mc.GT_GetSts(1, out XAxisState, 1, out clk);
            //        //读取2轴的状态
            //        gts.mc.GT_GetSts(1, out YAxisState, 1, out clk);
            //    } while (((XAxisState & 0x400) !=0)||((YAxisState& 0x400)!=0));// 等待XY轴规划停止
            //});         
            sRtn = gts.mc.GT_ZeroPos(1, 2);
            if (sRtn != 0)
            {
                MotorModel.Message = "ZeroPos Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_PrfTrap(1);
            if (sRtn != 0)
            {
                MotorModel.Message = "PrfTrap1 Fail:" + sRtn.ToString();
                return;
            }
            sRtn = gts.mc.GT_PrfTrap(2);//设置为点位模式
            if (sRtn != 0)
            {
                MotorModel.Message = "PrfTrap2 Fail:" + sRtn.ToString();
                return;
            }
            Parameter.XMotor.AxisMode = Motor.Mode.AUTO;
            Parameter.YMotor.AxisMode = Motor.Mode.AUTO;
        }

    }
}
