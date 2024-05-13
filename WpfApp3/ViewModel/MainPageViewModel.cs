using LiveCharts;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp3.Access;
using WpfApp3.Common;
using WpfApp3.Model;

namespace WpfApp3.ViewModel
{
    public class MainPageViewModel : NotifyBase
    {

        private int _pageIndex = 0;
        private int _pageSize = 5;
        private int _currentPage = 1;
        public int TargetPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public MainPageModel model = new MainPageModel();
        public ObservableCollection<string> Labels { get; set; }
        public ObservableCollection<ProductData> Products { get; set; } = new ObservableCollection<ProductData>();
        public SeriesCollection ProcessNumberSeries { get; set; }
        public SeriesCollection PassRateSeries { get; set; }
        public CommandBase PageUpCommand { get; set; } = new CommandBase();
        public CommandBase PageDownCommand { get; set; } = new CommandBase();
        public CommandBase TurnToPageCommand { get; set; }= new CommandBase();
        public CommandBase SelectCommand { get; set; }=new CommandBase();
        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; this.DoNotify(); }
        }
        public MainPageViewModel()
        {
            PageCount = (int)Math.Ceiling((double)LocalDataAccess.GetInstance().GetRecordCount("product") / _pageSize);
            model.ProductionDatas = LocalDataAccess.GetInstance().GetProductions();
            SeriesInit();
            turnToPage();                                 
            PageUpCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            PageUpCommand.DoExecute = new Action<object>((obj) =>
            {
                if (_pageIndex - _pageSize>=0)
                {
                    CurrentPage -= 1;
                    _pageIndex-=_pageSize;
                }                    
                else
                {
                    _pageIndex=0;
                    CurrentPage = 1;
                }
                turnToPage();
            });
            PageDownCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            PageDownCommand.DoExecute = new Action<object>((obj) =>
            {
                _pageIndex += _pageSize;
                CurrentPage += 1;
                turnToPage();
            });
            TurnToPageCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            TurnToPageCommand.DoExecute = new Action<object>((obj) =>
            {
                if(TargetPage>0&&TargetPage<=PageCount)
                {
                    CurrentPage = TargetPage;
                    _pageIndex = (CurrentPage-1) * _pageSize;
                    turnToPage();
                }
            });
            SelectCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            SelectCommand.DoExecute = new Action<object>((obj) =>
            {
                TextBox box=obj as TextBox;
                MySqlParameter[] sp = new MySqlParameter[]
           {
                new MySqlParameter("@productname",box.Text.Trim())
           };
                Products.Clear();
                LocalDataAccess.GetInstance().SelectProduct(sp).ForEach(product => Products.Add(product));
            });
        }
        private void SeriesInit()
        {
            Labels = new ObservableCollection<string>();
            List<List<int>> productionDatas = new List<List<int>>();
            List<List<double>> passrates = new List<List<double>>();
            List<string> Types = new List<string>();
            List<string> strings = new List<string>();
            List<string> types = new List<string>();
            foreach (var data in model.ProductionDatas)
            {
                strings.Add(data.Time);
                types.Add(data.Type);
            }
            strings.Distinct().ToList().ForEach(x => Labels.Add(x));//去除重复项
            types.Distinct().ToList().ForEach(x => Types.Add(x));//同上
            strings.Clear();
            types.Clear();
            for (int i = 0; i < Types.Count; i++)
            {
                productionDatas.Add(new List<int>());
                passrates.Add(new List<double>());
            }
            for (int i = 0; i < model.ProductionDatas.Count; i++)
            {
                for (int j = 0; j < Types.Count; j++)
                {
                    if (model.ProductionDatas[i].Type == Types[j])
                    {
                        productionDatas[j].Add(model.ProductionDatas[i].DayProcessNumber);
                        passrates[j].Add(model.ProductionDatas[i].PassRate);
                    }
                }
            }
            ProcessNumberSeries = new SeriesCollection();
            PassRateSeries = new SeriesCollection();
            for (int i = 0; i < Types.Count; i++)
            {
                LineSeries line = new LineSeries();
                line.Values = new ChartValues<int>(productionDatas[i]);
                line.Title = Types[i];
                ProcessNumberSeries.Add(line);
            }
            for (int i = 0; i < Types.Count; i++)
            {
                LineSeries line = new LineSeries();
                line.Values = new ChartValues<double>(passrates[i]);
                line.Title = Types[i];
                PassRateSeries.Add(line);
            }

        }
        private void turnToPage()
        {
            Products.Clear();
            MySqlParameter[] sp = new MySqlParameter[]
            {
                new MySqlParameter("@start",_pageIndex),
                new MySqlParameter("@count",_pageSize)
            };
            model.ProductDatas = LocalDataAccess.GetInstance().GetProducts(sp);
            if (model.ProductDatas.Count > 0)
            {
                model.ProductDatas.ForEach(product => Products.Add(product));
            }
            else
            {
                Products.Clear();
                _pageIndex -= _pageSize;
                CurrentPage -= 1;
                if (_pageIndex >= 0)
                    turnToPage();
                else
                {
                    _pageIndex = 0;
                    CurrentPage = 1;
                }
            }
        }
    }
}
