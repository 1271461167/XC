using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Model
{
    public class ProducData
    {
        public string ProductionID { get; set; }    //产品ID
        public string Type   { get; set; }          //产品类型
        public double Power {  get; set; }          //功率
        public double ZHeight {  get; set; }        //Z轴高度
        public TimeSpan ProcessTime {  get; set; }  //加工时间
        public string Time { get; set; }            //生产日期
        public bool IsPass {  get; set; }           //是否合格
    }
}
