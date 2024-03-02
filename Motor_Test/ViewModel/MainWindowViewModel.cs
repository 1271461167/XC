using Motor_Test.Common;
using Motor_Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Motor_Test.ViewModel
{
    public class MainWindowViewModel:CommandBase
    {
        public static MainWindowModel ViewModel { get; set; } = new MainWindowModel();
        public CommandBase NavChangedCommand { get; set; } = new CommandBase();
        public MainWindowViewModel()
        {
            NavChangedCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            NavChangedCommand.DoExecute = new Action<object>((obj) => { DoNavChanged(obj); });          
        }
        private void DoNavChanged(object obj)
        {
            Type type = Type.GetType("Motor_Test.View." + obj.ToString());                //获取对象类型                    
            ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
            ViewModel.MainContent = (FrameworkElement)constructor.Invoke(null); //返回该对象一个实例
        }
    }
}
