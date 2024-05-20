using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.Model;

namespace WpfApp3.Pub_Sub.Events
{
    public class CSVEvents:EventArgs
    {
        public ProductData ProductData { get; set; }
        public CSVEvents(ProductData productData)
        {
            ProductData = productData;
        }

    }
}
