using Motor_Test.Common;
using Motor_Test.Common.GTS;
using Motor_Test.Common.JCZ;
using Motor_Test.Common.Motor;
using Motor_Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Motor_Test.ViewModel
{
    public class MainWindowViewModel:CommandAndNotifyBase
    {
        public static MainWindowModel ViewModel { get; set; } = new MainWindowModel();
        public CommandAndNotifyBase NavChangedCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase WindowLoadedCommand { get; set; }= new CommandAndNotifyBase();
        public CommandAndNotifyBase ClearLogCommand { get; set; }=new CommandAndNotifyBase();
        public MainWindowViewModel()
        {
            NavChangedCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            NavChangedCommand.DoExecute = new Action<object>((obj) => { DoNavChanged(obj); });
            WindowLoadedCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            WindowLoadedCommand.DoExecute = new Action<object>((obj) => { WindowLoaded(obj); });
            ClearLogCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            ClearLogCommand.DoExecute = new Action<object>((obj) => { ClearLog(); });
        }

        private void ClearLog()
        {
            Log.Clear();
        }

        private void WindowLoaded(object obj)
        {
            RichTextBox richTextBox = (RichTextBox)obj;
            Log.SetTextControl(richTextBox);
            Log.Suc("软件打开成功");
            InitCard();
        }

        private void InitCard()
        {
            GTSIni.InitCard("GTS800.cfg");
            JczIni.InitCard();
        }

        private void DoNavChanged(object obj)
        {
            Type type = Type.GetType("Motor_Test.View." + obj.ToString());      //获取对象类型                    
            ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
            ViewModel.MainContent = (FrameworkElement)constructor.Invoke(null); //返回该对象一个实例
        }
    }
}
