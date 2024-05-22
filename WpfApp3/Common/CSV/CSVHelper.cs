using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp3.Common.CSV
{
    public static class CSVHelper
    {
        public static void WriteCsv(string result)
        {
            string path = Environment.CurrentDirectory+"//"+ "CSV";//保存路径
            string fileName = path+"//"+ DateTime.Now.ToString("yyyy-MM-dd") + ".csv";//文件名
            string Datedate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//年月日小时分钟秒
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(fileName))
            {
                StreamWriter sw = new StreamWriter(fileName, true, Encoding.UTF8);
                string str1 = "ID" + "," + "类型" + "," + "功率" + "," + "Z轴高度"+","+"加工时间"+","+"加工日期"+","+"是否合格" + "\t\n";
                sw.Write(str1);
                sw.Close();
            }
            StreamWriter swl = new StreamWriter(fileName, true, Encoding.UTF8);
            string str = result + "\t\n";
            swl.Write(str);
            swl.Close();
        }
        public static void ReadCsv(string path, out List<string> data)
        {
            StreamReader sr;
            data = new List<string>();
            try
            {
                using (sr = new StreamReader(path, Encoding.GetEncoding("GB2312")))
                {
                    string str = "";
                    while ((str = sr.ReadLine()) != null)
                    {
                        data.Add(str);
                    }
                }
            }
            catch (Exception ex)
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (process.ProcessName.ToUpper().Equals("EXCEL"))
                        process.Kill();
                }
                GC.Collect();
                Thread.Sleep(10);
                Console.WriteLine(ex.StackTrace);
                using (sr = new StreamReader(path, Encoding.GetEncoding("GB2312")))
                {
                    string str = "";
                    while ((str = sr.ReadLine()) != null)
                    {
                        data.Add(str);
                    }
                }
            }

        }
        public static string _toString<T>(T a)
        {
            string str = string.Empty;
            if(a==null)
            {
                return str;
            }
            System.Reflection.PropertyInfo[] properties= a.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (properties.Length<=0)
            {
                return str;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                //string name = item.Name;
                object value = item.GetValue(a, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    str += string.Format("{0},",value);
                }
                else
                {
                    _toString(value);
                }
            }
            return str.TrimEnd(',');
        }
    }
}
