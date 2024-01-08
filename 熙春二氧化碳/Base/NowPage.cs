using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_12_11XiChun.Base
{
    public class NowPage
    {
        public enum nowPage {HomePL,ParameterPL,MotorPL,MarkPL}
        private static NowPage instance=null;
        private NowPage() { }
        private nowPage nowpage;

        public nowPage PageNow
        {
            get { return nowpage; }
            set { nowpage = value; }
        }
        public static NowPage GetInstance()
        {
            if (instance == null)
            {
                instance = new NowPage();
            }
            return instance;
        }

    }
}
