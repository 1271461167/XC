﻿using _2023_12_11XiChun.Base;
using _2023_12_11XiChun.Model;
using _2023_12_11XiChun.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2023_12_11XiChun.ViewModel
{
    public class HomePageViewModel : CommandBase
    {
        public static CancellationTokenSource WorkCancellationTokenSource;
        static int index = 0;
        public static bool isRun=false;
        byte[] buffer = new byte[100];
        public static AutoProcess Process { get; set; } = MainPageViewModel.mainPage.Process;
        public string IP { get; set; } = "127.0.0.1";
        public string Port { get; set; } = "8001";
        public static Socket MySocket { get; set; } = null;
        public ParameterModel Parameter { get; set; } = ParameterModel.GetParameter();
        public static HomePageModel homemodel { get; set; } = new HomePageModel();
        public CommandBase ConnectCommand { get; set; } = new CommandBase();//连接事件
        public CommandBase DisconnectCommand { get; set; } = new CommandBase();//断开连接事件
        public CommandBase RunCommand { get; set; } = new CommandBase();
        public CommandBase StopCommand { get; set; } = new CommandBase();

        public HomePageViewModel()
        {
            NowPage now = NowPage.GetInstance();
            now.PageNow = NowPage.nowPage.HomePL;
            ConnectCommand.DoCanExecute = new Func<object, bool>((obj) => { return CanConnect(); });
            ConnectCommand.DoExecute = new Action<object>((obj) => { Connect(); });
            DisconnectCommand.DoCanExecute = new Func<object, bool>((obj) => { return CanDisConnect(); });
            DisconnectCommand.DoExecute = new Action<object>((obj) => { Disconnect(); });
            RunCommand.DoCanExecute = new Func<object, bool>((obj) => { return CanRun(); });
            RunCommand.DoExecute = new Action<object>( (obj) =>
            {
                HomePage homePage = obj as HomePage;
                //await Task.Run(() => 
                //{
                //    AxisMove(1, Process.XVelocity, Process.MovePosition[index % Process.MoveCount].XPosition);
                //    AxisMove(2, Process.YVelocity, Process.MovePosition[index % Process.MoveCount].YPosition);
                //});
                MySocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), homePage);
                isRun = !isRun;
                WorkCancellationTokenSource = new CancellationTokenSource();
                Task task = new Task(() => { Work(WorkCancellationTokenSource.Token); }, WorkCancellationTokenSource.Token, TaskCreationOptions.LongRunning);
                task.Start();
            });
            StopCommand.DoCanExecute = new Func<object, bool>((obj) => { return CanStop(); });
            StopCommand.DoExecute = new Action<object>((obj) =>
            {
                isRun = !isRun;
                WorkCancellationTokenSource?.Cancel();
                WorkCancellationTokenSource?.Dispose();
            });
        }

        private bool CanStop()
        {
            if (!isRun)
            {
                return false;
            }
            else
            { return true; }
        }

        private bool CanRun()
        {
            if(!isRun)
            {
                return true;
            }
            else
            { return false; }
        }

        private void Work(CancellationToken token)
        {
            //HomePage homePage = obj as HomePage;
            //Process.AllEntNameList = new string[Process.AllEntCount];
            //for (int i = 0; i < Process.AllEntCount; i++)
            //{
            //    string str = ((i + 1) * 100).ToString();
            //    JczLmc.SetEntityNameByIndex(i, str);
            //    Process.AllEntNameList[i] = JczLmc.GetEntityNameByIndex(i);
            //}
            while (true)
            {
                if(token.IsCancellationRequested) { break; } 

                if (JczLmc.GetInPort(MainPageViewModel.mainPage.StartMarkPort) == false)
                {
                    Thread.Sleep(100);
                    if (JczLmc.GetInPort(MainPageViewModel.mainPage.StartMarkPort) == true)
                    {
                        JczLmc.SetOutPutPort(3,true);
                        Task[] tasks= new Task[2];
                        tasks[0] = Task.Run(() => { AxisMove(1, Process.XVelocity, Process.MovePosition[0].XPosition); });
                        tasks[1] = Task.Run(() => { AxisMove(2, Process.YVelocity, Process.MovePosition[0].YPosition); });
                        //AxisMove(1, Process.XVelocity, Process.MovePosition[0].XPosition);
                        //AxisMove(2, Process.YVelocity, Process.MovePosition[0].YPosition);
                        //int i = Process.MoveCount;
                        //while (i-- > 0)
                        //{
                        //    try
                        //    {
                        //        AxisMove(1, Process.MovePosition[Process.MoveCount - 1 - i].XPosition, Process.XVelocity);
                        //        AxisMove(2, Process.MovePosition[Process.MoveCount - 1 - i].YPosition, Process.YVelocity);
                        //        SendDataFuntion("T1");
                        //        Rec_Data(homePage);
                        //    }
                        //    catch (Exception e)
                        //    {
                        //        break;
                        //    }
                        //    Thread.Sleep(Process.DelayTime);
                        //    if (token.IsCancellationRequested)
                        //    { break; }
                        //}
                        //Task.Run(() =>
                        //{
                        //    JczLmc.SetOutPutPort(MainPageViewModel.mainPage.MarkFinishedPort, MainPageViewModel.mainPage.MarkFinishedLevel);
                        //    Thread.Sleep(MainPageViewModel.mainPage.MarkFinishedWidth);
                        //    JczLmc.SetOutPutPort(MainPageViewModel.mainPage.MarkFinishedPort, !MainPageViewModel.mainPage.MarkFinishedLevel);
                        //});
                        Task.WaitAll(tasks);
                        Thread.Sleep(100);
                        SendDataFuntion("T1");
                    }
                }
            }

        }

        private void SendDataFuntion(object obj)
        {
            string str = obj as string;
            byte[] send = Encoding.UTF8.GetBytes(str);
            try
            {
                MySocket.Send(send);
            }
            catch (Exception ex)
            {
                homemodel.ErrMessage = ex.ToString();
            }

        }
        private bool CanDisConnect()
        {
            if (MySocket == null)
            {
                return false;
            }
            return true;

        }

        private void Disconnect()
        {
            index = 0;
            MySocket?.Close();
            MySocket?.Dispose();
            MySocket = null;
        }

        private void Connect()
        {
            if (MySocket == null)
            {
                try
                {
                    IPAddress address = IPAddress.Parse(IP.Trim());
                    IPEndPoint endpoint = new IPEndPoint(address, Convert.ToInt32(Port.Trim()));
                    MySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    MySocket.Connect(endpoint);
                    homemodel.ErrMessage = "";//错误信息

                    Process.AllEntNameList = new string[Process.AllEntCount];
                    for (int i = 0; i < Process.AllEntCount; i++)
                    {
                        string str = ((i + 1) * 100).ToString();
                        JczLmc.SetEntityNameByIndex(i, str);
                        Process.AllEntNameList[i] = JczLmc.GetEntityNameByIndex(i);
                    }
                }
                catch (Exception e)
                {
                    MySocket = null;
                    homemodel.ErrMessage = e.ToString();
                }
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            // Thread.Sleep(5000);
            try
            {
                MySocket.EndReceive(ar);
            }
            catch (Exception)
            {
                return;
            }
            ar.AsyncWaitHandle.Close();
            HomePage homePage = (HomePage)ar.AsyncState;
            string str1 = Encoding.UTF8.GetString(buffer);
            if (str1 == "/0")
            { return; }
            List<string> list = new List<string>();
            list.AddRange(str1.Split(','));
            for (int i = 0; i < Process.AllEntCount; i++)                             //将对应数据放入数据模型中
            {
                if (list[5 * i + 2] == "0")
                {
                    try
                    {
                        Process.Rec[i].X_Offset = double.Parse(list[5 * i + 3]);
                        Process.Rec[i].Y_Offset = double.Parse(list[5 * i + 4]);
                        Process.Rec[i].Angle_Offset = double.Parse(list[5 * i + 5]);
                        ModelMove(i);
                        Thread.Sleep(20);
                        Mark(i);
                    }
                    catch (Exception e)
                    {
                        homemodel.ErrMessage = "打标失败";
                    }
                }
            }
            Array.Clear(buffer, 0, buffer.Length);
            list.Clear();
            Thread.Sleep(30);
            int width = homePage.Dispatcher.Invoke(new Func<int>(() => { return (int)homePage.image.Width; }));
            int height = homePage.Dispatcher.Invoke(new Func<int>(() => { return (int)homePage.image.Height; }));
            Refresh(width, height, homePage);
            index++;
            if (index % Process.MoveCount != 0)
            {
                Task[] tasks = new Task[2];
                tasks[0] = Task.Run(() => { AxisMove(1, Process.XVelocity, Process.MovePosition[index % Process.MoveCount].XPosition); });
                tasks[1] = Task.Run(() => { AxisMove(2, Process.YVelocity, Process.MovePosition[index % Process.MoveCount].YPosition); });
                //AxisMove(1, Process.XVelocity, -321.68);
                //AxisMove(2, Process.YVelocity, -173.732);
                Task.WaitAll(tasks);
                //AxisMove(1, Process.XVelocity, Process.MovePosition[index % Process.MoveCount].XPosition);
                //AxisMove(2, Process.YVelocity, Process.MovePosition[index % Process.MoveCount].YPosition);
                MySocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), homePage);
                Thread.Sleep(Process.DelayTime);
                SendDataFuntion("T1");
            }
            else
            {
                index = 0;
                Task[] tasks = new Task[2];
                tasks[0] = Task.Run(() => { AxisMove(1, Process.XVelocity, -321.68); });
                tasks[1] = Task.Run(() => { AxisMove(2, Process.YVelocity, -173.732); });
                //AxisMove(1, Process.XVelocity, -321.68);
                //AxisMove(2, Process.YVelocity, -173.732);
                Task.WaitAll(tasks);
                MySocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), homePage);
                Thread.Sleep(Process.DelayTime);
                Task.Run(() =>
                {
                    JczLmc.SetOutPutPort(MainPageViewModel.mainPage.MarkFinishedPort, MainPageViewModel.mainPage.MarkFinishedLevel);
                    JczLmc.SetOutPutPort(3,false);
                    Thread.Sleep(MainPageViewModel.mainPage.MarkFinishedWidth);
                    JczLmc.SetOutPutPort(MainPageViewModel.mainPage.MarkFinishedPort, !MainPageViewModel.mainPage.MarkFinishedLevel);
                });
            }

        }

        private bool CanConnect()
        {
            if (MySocket != null)
            {
                return false;
            }
            return true;
        }
        //public void Rec_Data(HomePage homePage)
        //{
        //    Socket myclient = MySocket;
        //    if (myclient != null)
        //    {
        //        List<string> list = new List<string>();
        //        byte[] rec = new byte[200];
        //        int length = -1;
        //        Array.Clear(rec, 0, rec.Length);
        //        try
        //        {
        //            length = myclient.Receive(rec);
        //        }
        //        catch (Exception e)
        //        {
        //            throw e;
        //        }

        //        if (length == 0)
        //        {
        //            throw new Exception("连接断开");
        //        }
        //        else
        //        {
        //            string sb = Encoding.UTF8.GetString(rec);               //将接收到的数据转成字符串
        //            list.AddRange(sb.Split(','));                           //将接收到的字符串依据","进行分割
        //            for (int i = 0; i < Process.AllEntCount; i++)                             //将对应数据放入数据模型中
        //            {
        //                if (list[5 * i + 2] == "0")
        //                {
        //                    try
        //                    {
        //                        Process.Rec[i].X_Offset = double.Parse(list[5 * i + 3]);
        //                        Process.Rec[i].Y_Offset = double.Parse(list[5 * i + 4]);
        //                        Process.Rec[i].Angle_Offset = double.Parse(list[5 * i + 5]);
        //                        ModelMove(i);
        //                        Mark(i);
        //                    }
        //                    catch (Exception e)
        //                    {

        //                    }
        //                }
        //            }
        //            list.Clear();
        //            try
        //            {
        //                int width = homePage.Dispatcher.Invoke(new Func<int>(() => { return (int)homePage.image.Width; }));
        //                int height = homePage.Dispatcher.Invoke(new Func<int>(() => { return (int)homePage.image.Height; }));
        //                Refresh(width, height, homePage);
        //            }
        //            catch (Exception e)
        //            {
        //                throw e;
        //            }

        //        }
        //    }

        //}

        private void Refresh(int width, int height, HomePage homePage)
        {
            Bitmap bitmap = new Bitmap(JczLmc.GetCurPreviewImage(width, height));
            homePage.Dispatcher.Invoke(new Action(() => { homemodel.MyImage = BitMapToImageSource.Bitmap2Imagesource(bitmap); }));
            JczLmc.LoadEzdFile(MarkPageViewModel.markPageModel.FileName);
            for (int i = 0; i < Process.AllEntCount; i++)
            {
                string str = ((i + 1) * 100).ToString();
                JczLmc.SetEntityNameByIndex(i, str);
                Process.AllEntNameList[i] = JczLmc.GetEntityNameByIndex(i);
            }
        }

        private void Mark(int index)
        {
            int nErr;
            try
            {
                nErr = JczLmc.MarkEntity(Process.AllEntNameList[index]);
                if (nErr != 0)
                {
                    throw new Exception("打标失败");
                }
            }
            catch (Exception)
            {
                throw new Exception("Mark打标失败");
            }
        }

        private void ModelMove(int index)
        {
            double Center_X, Center_Y;
            double Min_X = 0.0, Min_Y = 0.0, Max_X = 0.0, Max_Y = 0.0, dZ = 0.0;
            int nErr;
            try
            {
                nErr = JczLmc.GetEntSize(Process.AllEntNameList[index], ref Min_X, ref Min_Y, ref Max_X, ref Max_Y, ref dZ);
            }
            catch (Exception)
            {
                throw new Exception("GetEntSize失败");
            }
            if (nErr != 0)
            {
                throw new Exception("GetEntSize失败");
            }
            Center_X = (Min_X + Max_X) / 2.0;
            Center_Y = (Max_Y + Min_Y) / 2.0;
            try
            {
                nErr = JczLmc.MoveEnt(Process.AllEntNameList[index], Process.Rec[index].X_Offset - Center_X, Process.Rec[index].Y_Offset - Center_Y);
            }
            catch (Exception)
            {
                throw new Exception("MoveEnt失败");
            }

            if (nErr != 0)
            {
                throw new Exception("MoveEnt失败");
            }
            try
            {
                nErr = JczLmc.RotateEnt(Process.AllEntNameList[index], Process.Rec[index].X_Offset, Process.Rec[index].Y_Offset, Process.Rec[index].Angle_Offset);
            }
            catch (Exception)
            {
                throw new Exception("RotateEnt失败");
            }

            if (nErr != 0)
            {
                throw new Exception("RotateEnt失败");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="velocity">速度 单位mm/s</param>
        /// <param name="position">位置 单位mm</param>
        private void AxisMove(short axis, double velocity, double position)
        {
            gts.mc.TTrapPrm trap;
            int AxisState;
            uint clk;
            double vel = velocity * Parameter.XMotor.Pulse / 1000;
            int pos = int.Parse((position * Parameter.XMotor.Pulse).ToString());

            trap.acc = 0.5;
            trap.dec = 0.5;
            trap.smoothTime = 25;
            trap.velStart = 0;
            gts.mc.GT_SetTrapPrm(axis, ref trap);//设置点位运动参数
            gts.mc.GT_SetVel(axis, vel);//设置目标速度
            gts.mc.GT_SetPos(axis, pos);//设置目标位置
            gts.mc.GT_Update(axis);//更新轴运动
            do
            {
                gts.mc.GT_GetSts(axis, out AxisState, 1, out clk);

            } while ((AxisState & 0x400) != 0);
        }
    }
}
