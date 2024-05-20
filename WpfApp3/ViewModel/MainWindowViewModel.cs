using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.Common;
using WpfApp3.Common.LOG;

namespace WpfApp3.ViewModel
{
    public class MainWindowViewModel:NotifyBase
    {
        private FrameworkElement _mainContent;

        public FrameworkElement MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; this.DoNotify(); }
        }
        public CommandBase NavChangedCommand { get; set; } = new CommandBase();
        public CommandBase WindowLoadedCommand { get; set; }= new CommandBase();
        public CommandBase ClearLogCommand { get; set; }=new CommandBase();
        public MainWindowViewModel()
        {
            NavChangedCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            NavChangedCommand.DoExecute = new Action<object>((obj) => { DoNavChanged(obj); });
            WindowLoadedCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            WindowLoadedCommand.DoExecute = new Action<object>((obj) => { WindowLoaded(obj); });
            ClearLogCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            ClearLogCommand.DoExecute = new Action<object>((obj) => { Log.Clear(); });
            DoNavChanged("MainPage");           
        }

        private void DoNavChanged(object obj)
        {
            Type type = Type.GetType("WpfApp3.View." + obj.ToString());                 //获取对象类型                    
            ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
            MainContent = (FrameworkElement)constructor.Invoke(null);                   //返回该对象一个实例
        }
        private void WindowLoaded(object obj)
        {
            RichTextBox richTextBox = (RichTextBox)obj;
            Log.SetTextControl(richTextBox);
            Log.Suc("软件打开成功");
        }

    }
}
