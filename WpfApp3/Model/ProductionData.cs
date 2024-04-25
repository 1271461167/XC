using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Model
{
    public class ProductionData
    {
        public string Type { get; set; }            //产品类型
        public int DayProcessNumber { get; set; }   //加工数量
        public double PassRate { get; set; }        //合格率
        public string Time { get; set; }            //日期
    }
}
