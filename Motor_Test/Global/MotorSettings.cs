using Motor_Test.Common;
using Motor_Test.Common.Motor;
using Motor_Test.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Motor_Test.Global
{
    public static class MotorSettings
    {
        static MotorSettings()
        {
            for (int i = 0;i<Motor_Setting.Length;i++)
            {
                Motor_Setting[i].Puls = int.Parse(CreateIni.ReadIni("Axis"+i.ToString(), "Puls", ""));
                Motor_Setting[i].Band = int.Parse(CreateIni.ReadIni("Axis"+i.ToString(), "Band", ""));
                Motor_Setting[i].Time = int.Parse(CreateIni.ReadIni("Axis"+i.ToString(), "Time", ""));
            }           
        }
        public static MotorSet[] Motor_Setting { get; set; } = new MotorSet[] { new MotorSet (), new MotorSet () };
    }
}
