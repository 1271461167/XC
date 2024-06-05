using System;
using System.CodeDom;
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
        [DllImport("kernel32")]
        private static extern uint GetPrivateProfileSectionNames(IntPtr intPtr,uint size,string filepath);

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
        public static string[] GetAllSection()
        {
            uint Max_BUFFER = 32767;
            string[] sections = new string[0];
            IntPtr intPtr = Marshal.AllocCoTaskMem((int)Max_BUFFER * sizeof(char));
            uint byteReturned = GetPrivateProfileSectionNames(intPtr, Max_BUFFER, rootpath);
            if(byteReturned!=0)
            {
                string local=Marshal.PtrToStringAnsi(intPtr,(int)byteReturned).ToString();
                sections = local.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }
            Marshal.FreeCoTaskMem(intPtr);
            return sections;
        }

    }
}
