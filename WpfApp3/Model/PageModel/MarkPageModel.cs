using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Model.PageModel
{
    public class MarkPageModel
    {
        public List<ProductData> TodayProductDatas { get; set; }
        public Dictionary<string,int> keyValuePairs { get; set; }= new Dictionary<string,int>();
    }
}
