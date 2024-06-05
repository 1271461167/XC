using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.Common.Ini;
using WpfApp3.Model;

namespace WpfApp3.Common.Global
{
    public static class MarkingSettings
    {
        public static List<IOSettings> INS {  get; set; }= new List<IOSettings>();
        public static List<IOSettings> OUTS { get; set; } = new List<IOSettings>();
        static MarkingSettings()
        {
            Refresh();
        }
        public static void Refresh()
        {
            INS.Clear();
            OUTS.Clear();
            string[] strings = CreateIni.GetAllSection();
            foreach (string s in strings)
            {
                if (s.Substring(0, 2) == "输入")
                {
                    IOSettings settings = new IOSettings();
                    settings.Name = s.Substring(2, s.Length - 2);
                    settings.Index = int.Parse(CreateIni.ReadIni(s, "Index", ""));
                    INS.Add(settings);
                }
            }
            foreach (string s in strings)
            {
                if (s.Substring(0, 2) == "输出")
                {
                    IOSettings settings = new IOSettings();
                    settings.Name = s.Substring(2, s.Length - 2);
                    settings.Index = int.Parse(CreateIni.ReadIni(s, "Index", ""));
                    settings.Level = bool.Parse(CreateIni.ReadIni(s, "Level", ""));
                    settings.Width = int.Parse(CreateIni.ReadIni(s, "Width", ""));
                    OUTS.Add(settings);
                }
            }
        }
    }

}

