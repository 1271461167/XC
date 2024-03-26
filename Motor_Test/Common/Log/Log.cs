using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;
using System;

namespace Motor_Test.Common
{
    public static class Log
    {
        private const int MaxCount = 500;
        private static int Count = 0;
        private static RichTextBox textControl;
        private static InlineCollection inlines;
        //设置主控件
        public static void SetTextControl(RichTextBox _textBox)
        {
            textControl = _textBox;
            Paragraph graph = new Paragraph();
            inlines = graph.Inlines;
            textControl.Document.Blocks.Add(graph);
        }

        //输出黑色消息
        public static void Info(string format, params object[] args)
        {
            AppendText(Brushes.Black, format, args);
        }

        //输出绿色消息
        public static void Suc(string format, params object[] args)
        {
            AppendText(Brushes.DarkGreen, format, args);
        }

        //输出黄色消息
        public static void Warning(string format, params object[] args)
        {
            AppendText(Brushes.DarkOrange, format, args);
        }

        //输出红色消息
        public static void Error(string format, params object[] args)
        {
            AppendText(Brushes.Red, format, args);
        }

        //清除日志
        public static void Clear()
        {
            Count = 0;
            inlines.Clear();
            textControl.ScrollToEnd();
        }

        private static void AppendText(Brush color, string format, params object[] args)
        {
            textControl.BeginChange();
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            Count++;
            builder.Append(DateTime.Now);
            builder.Append("] : ");
            builder.Append(string.Format(format, (object[])args));
            builder.Append("\n");
            string str = builder.ToString();
            inlines.Add(new Run(str) { Foreground = color });
            if (inlines.Count > MaxCount)
            {
                inlines.Remove(inlines.FirstInline);
            }
            textControl.ScrollToEnd();
            textControl.EndChange();
        }
    }
}
