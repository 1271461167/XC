using Motor_Test.Common.GTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Motor_Test.Common.Motor
{
    public class GTSIni
    {
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
        public static void InitCard(string str)
        {
            CommandHandler(mc.GT_Open(0,1));
            CommandHandler(mc.GT_Reset());
            CommandHandler(mc.GT_LoadConfig(str));
            CommandHandler(mc.GT_ClrSts(1,4));

        }
    }
}
