using Motor_Test.Common.JZC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Common.JCZ
{
    public class JczHandler
    {
        public static void CommandHandler(int nRts)
        {
            if (nRts == 0) return;
            else 
            {
                Log.Error("JCZ:  "+JczLmc.GetErrorText(nRts));
            }
            
        }
    }
}
