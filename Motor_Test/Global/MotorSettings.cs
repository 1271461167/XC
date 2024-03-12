using Motor_Test.Common.Motor;
using Motor_Test.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Global
{
    public class MotorSettings
    {
        static MotorSettings()
        {
            int a = 0;
        }
        public static MotorSet[] Motor_Setting { get; set; } = new MotorSet[] { new MotorSet (), new MotorSet () };
    }
}
