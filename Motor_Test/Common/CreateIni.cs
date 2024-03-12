using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.Common
{
    public class CreateIni
    {
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section,string key,string defaultvalue,string filepath);
        [DllImport("kernel32")]
        private static extern bool GetPrivateProfileString(string section, string key, string defaultvalue,StringBuilder stringBuilder,int size,string filepath);

        private static string rootpath = ".\\Motor.ini";
        public static void WriteIni(string section, string key, string defaultvalue)
        {
            WritePrivateProfileString(section, key, defaultvalue, rootpath);
        }
    }
}
