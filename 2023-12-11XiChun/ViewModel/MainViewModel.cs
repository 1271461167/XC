using _2023_12_11XiChun.Base;
using _2023_12_11XiChun.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_12_11XiChun.ViewModel
{
    public class MainViewModel:CommandBase
    {
        public CommandBase CloseWindowCommand { get; set; }= new CommandBase();
        public CommandBase ChangePageCommand { get; set; } = new CommandBase();
        public static MainModel MainModel { get; set; }=new MainModel();
        
        public MainViewModel() 
        {
            NowPage now=NowPage.GetInstance();
            now.PageNow = NowPage.nowPage.HomePL;
            MainModel.PageName = "HomePage.xaml";
            this.CloseWindowCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            this.CloseWindowCommand.DoExecute = new Action<object>((obj) =>
            {
                MainWindow main = obj as MainWindow;
                main.Close();
            });
            this.ChangePageCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            this.ChangePageCommand.DoExecute = new Action<object>((obj) =>
            {
                MainModel.PageName = obj.ToString();
            });
        }
    }
}
