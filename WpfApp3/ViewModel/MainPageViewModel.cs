using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfApp3.Model;

namespace WpfApp3.ViewModel
{
    public class MainPageViewModel
    {
        public MainPageModel model = new MainPageModel();
        public ObservableCollection<string> Labels { get; set; }
        public SeriesCollection ProcessNumberSeries { get; set; }
        public MainPageViewModel()
        {
            //model.ProcessNumberList.Add(DateTime.Today,1000);
            //model.ProcessNumberList.Add(DateTime.Today + TimeSpan.FromDays(1), 2000);
            //ProcessNumberSeries = new SeriesCollection()
            //{
            //    new LineSeries
            //    {
            //        Values=new ChartValues<int>{model.ProcessNumberList[DateTime.Today],model.ProcessNumberList[DateTime.Today + TimeSpan.FromDays(1)] },
            //        Fill=new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FECC71")),
            //        Stroke=new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FECC71")),                  
            //    }
            //};
            //Labels = new List<string>();
            //Labels.Add(DateTime.Today.ToString("yyyy-MM-dd"));
            //Labels.Add((DateTime.Today + TimeSpan.FromDays(1)).ToString("yyyy-MM-dd"));
            model.ProductionDatas.Add(new ProductionData() { DayProcessNumber = 1000, PassRate = 90, Time = (DateTime.Today - TimeSpan.FromDays(1)).ToString("yyyy-MM-dd"), Type = "001" });
            model.ProductionDatas.Add(new ProductionData() { DayProcessNumber = 4000, PassRate = 95, Time = (DateTime.Today - TimeSpan.FromDays(1)).ToString("yyyy-MM-dd"), Type = "002" });
            model.ProductionDatas.Add(new ProductionData() { DayProcessNumber = 1500, PassRate = 90, Time = DateTime.Today.ToString("yyyy-MM-dd"), Type = "001" });
            model.ProductionDatas.Add(new ProductionData() { DayProcessNumber = 3500, PassRate = 95, Time = DateTime.Today.ToString("yyyy-MM-dd"), Type = "002" });
            model.ProductionDatas.Add(new ProductionData() { DayProcessNumber = 2000, PassRate = 90, Time = (DateTime.Today + TimeSpan.FromDays(1)).ToString("yyyy-MM-dd"), Type = "001" });
            model.ProductionDatas.Add(new ProductionData() { DayProcessNumber = 6000, PassRate = 95, Time = (DateTime.Today + TimeSpan.FromDays(1)).ToString("yyyy-MM-dd"), Type = "002" });
            Labels = new ObservableCollection<string>();
            List<int> Counts1 = new List<int>();
            List<int> Counts2 = new List<int>();
            string str = "";
            for (int i = 0;i<model.ProductionDatas.Count;i++)
            {
                if (model.ProductionDatas[i].Time!=str)
                {
                    str = model.ProductionDatas[i].Time;
                    Labels.Add(str);
                    Counts1.Add(model.ProductionDatas[i].DayProcessNumber);
                    Counts2.Add(model.ProductionDatas[i + 1].DayProcessNumber);
                }
            }
            ProcessNumberSeries = new SeriesCollection()
            {
                new LineSeries
                {
                    Values=new ChartValues<int>(Counts1),                   
                    Stroke=new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FECC71")),
                    Title="001"
                },
                new LineSeries
                {
                    Values=new ChartValues<int>(Counts2),                  
                    Stroke=new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7ADA95")),
                    Title="002"
                },
            };

        }
    }
}
