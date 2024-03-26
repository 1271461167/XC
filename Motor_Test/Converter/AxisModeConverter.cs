using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Motor_Test.Converter
{
    public class AxisModeConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int mode = (int)value;
            switch (mode)
            {
                case 0:
                    return "点位";
                case 1:
                    return "JOG";
                case 2:
                    return "PT";
                case 3:
                    return "电子齿轮";
                case 4:
                    return "Follow";
                case 5:
                    return "插补";
                case 6:
                    return "Pvt";
                default: 
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
