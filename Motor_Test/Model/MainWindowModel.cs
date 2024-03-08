using Motor_Test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Motor_Test.Model
{
    public class MainWindowModel:CommandAndNotifyBase
    {
        private FrameworkElement _mainContent;

        public FrameworkElement MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; this.DoNotify(); }
        }
    }
}
