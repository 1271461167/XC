using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp3.Common;

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
        public MainWindowViewModel()
        {
            NavChangedCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            NavChangedCommand.DoExecute = new Action<object>((obj) => { DoNavChanged(obj); });
            DoNavChanged("MainPage");           
        }

        private void DoNavChanged(object obj)
        {
            Type type = Type.GetType("WpfApp3.View." + obj.ToString());                 //获取对象类型                    
            ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
            MainContent = (FrameworkElement)constructor.Invoke(null);                   //返回该对象一个实例
        }

    }
}
