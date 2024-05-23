using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp3.ViewModel;

namespace WpfApp3.View
{
    /// <summary>
    /// MarkingSettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class MarkingSettingsPage : Window
    {
        public MarkingSettingsPage()
        {
            InitializeComponent();
            this.DataContext = new SettingsViewModel();
        }
    }
}
