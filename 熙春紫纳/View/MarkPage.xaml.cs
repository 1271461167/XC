using _2023_12_11XiChun.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2023_12_11XiChun.View
{
    /// <summary>
    /// MarkPage.xaml 的交互逻辑
    /// </summary>
    public partial class MarkPage : Page
    {
        public MarkPage()
        {
            InitializeComponent();
            this.DataContext = new MarkPageViewModel();
            //List<string> list = new List<string>();
            //list.Add("1");
            //list.Add("2");
            //list.Add("3");
            //this.my.ItemsSource = list;
        }
    }
}
