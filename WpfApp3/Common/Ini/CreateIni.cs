using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Common.Ini
{
    public class CreateIni
    {
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string defaultvalue, string filepath);
        [DllImport("kernel32")]
        private static extern bool GetPrivateProfileString(string section, string key, string defaultvalue, StringBuilder stringBuilder, int size, string filepath);

        private static string rootpath = ".\\Marking.ini";
        public static void WriteIni(string section, string key, string defaultvalue)
        {
            WritePrivateProfileString(section, key, defaultvalue, rootpath);
        }
        public static string ReadIni(string section, string key, string defaultvalue)
        {
            StringBuilder stringBuilder = new StringBuilder(256);
            GetPrivateProfileString(section, key, defaultvalue, stringBuilder, stringBuilder.Capacity, rootpath);
            return stringBuilder.ToString();
        }

    }
}
