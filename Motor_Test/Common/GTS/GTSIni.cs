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
        public static void InitCard(string str)
        {
            GtsHandler.CommandHandler(mc.GT_Open(0,1));
            GtsHandler.CommandHandler(mc.GT_Reset());
            GtsHandler.CommandHandler(mc.GT_LoadConfig(str));
            GtsHandler.CommandHandler(mc.GT_ClrSts(1,4));
        }
    }
}
