using LiveCharts;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WpfApp3.Access;
using WpfApp3.Common;
using WpfApp3.Model;

namespace WpfApp3.ViewModel
{
    public class MainPageViewModel
    {

        private int _pageIndex = 0;
        private int _pageSize = 4;
        public MainPageModel model = new MainPageModel();
        public ObservableCollection<string> Labels { get; set; }
        public ObservableCollection<ProductData> Products {  get; set; }=new ObservableCollection<ProductData>();
        public SeriesCollection ProcessNumberSeries { get; set; }
        public SeriesCollection PassRateSeries { get; set; }
        public CommandBase PageUpCommand { get; set; }= new CommandBase();
        public CommandBase PageDownCommand { get; set; } = new CommandBase();
        public MainPageViewModel()
        {         
            turnToPage();
            model.ProductionDatas = LocalDataAccess.GetInstance().GetProductions();
            SeriesInit();
            PageUpCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            PageUpCommand.DoExecute = new Action<object>((obj) =>
            {
                _pageIndex = _pageIndex - _pageSize >= 0 ? _pageIndex - _pageSize : 0;
                turnToPage();
            });
            PageDownCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            PageDownCommand.DoExecute = new Action<object>((obj) =>
            {
                _pageIndex += _pageSize;
                turnToPage();                
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
            strings.Distinct().ToList().ForEach(x => Labels.Add(x));
            types.Distinct().ToList().ForEach(x => Types.Add(x));
            strings.Clear();
            types.Clear();
            for (int i = 0; i < Types.Count; i++)
            {
                productionDatas.Add(new List<int>());
                passrates.Add(new List<double>());
            }
            for(int i=0;i<model.ProductionDatas.Count;i++)
            {
                for(int j=0;j<Types.Count;j++)
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
            for(int i=0;i<Types.Count;i++)
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
                turnToPage();
            }
        }
    }
}
