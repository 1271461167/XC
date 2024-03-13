using Motor_Test.Common.JZC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Common.JCZ
{
    public class JczIni
    {
        public static void InitCard()
        {
            JczLmc.Initialize(Environment.CurrentDirectory, false);
        }
    }
}
