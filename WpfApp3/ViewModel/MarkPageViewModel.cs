using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Media;
using WpfApp3.Common;
using WpfApp3.Common.CSV;
using WpfApp3.Common.LMC;
using WpfApp3.Common.LOG;
using WpfApp3.Model;
using WpfApp3.Model.PageModel;
using WpfApp3.Pub_Sub;
using WpfApp3.Pub_Sub.Events;
using WpfApp3.View;

namespace WpfApp3.ViewModel
{
    public class MarkPageViewModel:NotifyBase
    {
        private static string[] m_strAllEntNameList;
        private static int m_nAllEntCount;
        IMarkController _markController = LMC.GetInstance();
        private static MarkPageModel _markModel=new MarkPageModel();
        private static bool ini=false;
        public Point WaitPoint {  get; set; }=new Point();
        private TimeSpan processingtime;

        public TimeSpan ProcessingTime
        {
            get { return processingtime; }
            set { processingtime = value;this.DoNotify(); }
        }

        private static string _kind = "";

        public string Kind
        {
            get { return _kind; }
            set 
            {                 
                _kind = value;
                this.DoNotify();
                if (value == "") { return; }
                if (_markModel.keyValuePairs.ContainsKey(_kind.ToUpper()))
                {
                    ID = _markModel.keyValuePairs[_kind.ToUpper()];
                }
                else
                {
                    _markModel.keyValuePairs.Add(_kind.ToUpper(), 0);
                    ID = 0;
                }
            }
        }

        private static int id;

        public int ID
        {
            get { return id; }
            set 
            { 
                id = value;
                this.DoNotify();
                _markModel.keyValuePairs[Kind.ToUpper()]= id;
            }
        }
        private static int count;

        public int Count
        {
            get { return count; }
            set { count = value;this.DoNotify(); }
        }
        private Pub pub=new Pub();
        public ObservableCollection<ProductData> Products { get; set; } = new ObservableCollection<ProductData>();
        public ObservableCollection<Point> WorkPoints { get; set; } = new ObservableCollection<Point>();
        private static ImageSource _image;
        public ImageSource PriviewImage
        {
            get { return _image; }
            set { _image = value;this.DoNotify(); }
        }
        public CommandBase LoadFileCommand { get; set; }=new CommandBase();
        public CommandBase RefreshCommand { get; set; }= new CommandBase();  
        public CommandBase MarkCommand { get; set; } = new CommandBase();
        public CommandBase MoveCommand { get; set; }=new CommandBase();
        public CommandBase AxisIsChecked {  get; set; } = new CommandBase();
        public CommandBase AxisUnChecked { get; set; } = new CommandBase();
        private int workcount;
        public int WorkCount
        {
            get { return workcount; }
            set 
            { 
                workcount = value;
                this.DoNotify();
                WorkPoints.Clear();
                for (int i=0;i<workcount;i++)
                {
                    WorkPoints.Add(new Point(0, 0));
                }

            }
        }
        public CommandBase ZeroYieldCommand { get; set; } = new CommandBase();
        public CommandBase StartIOWindow { get; set; } = new CommandBase();
        public CommandBase LoadCommand { get; set; } = new CommandBase();
        public CommandBase SettingsCommand { get; set; } = new CommandBase();

        public MarkPageViewModel() 
        {           
            LoadCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            LoadCommand.DoExecute = new Action<object>((obj) =>
            {
                Loaded();
            });
            LoadFileCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            LoadFileCommand.DoExecute = new Action<object>((obj) =>
            {
                LoadEdzFile(obj);
            });
            RefreshCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            RefreshCommand.DoExecute = new Action<object>((obj) =>
            {
                RefreshPreview(obj);
            });
            MarkCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            MarkCommand.DoExecute = new Action<object>((obj) =>
            {
                Mark();
            });
            MoveCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            MoveCommand.DoExecute = new Action<object>((obj) =>
            {
                Point point = (Point)obj;
            });
            AxisIsChecked.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            AxisIsChecked.DoExecute = new Action<object>((obj) =>
            {
                MarkPage markPage = (MarkPage)obj;
                markPage.workbox.IsEnabled = true;
                markPage.waitbox.IsEnabled = true;
            });
            AxisUnChecked.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            AxisUnChecked.DoExecute = new Action<object>((obj) =>
            {
                MarkPage markPage = (MarkPage)obj;
                markPage.workbox.IsEnabled = false;
                markPage.waitbox.IsEnabled = false;
            });
            ZeroYieldCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            ZeroYieldCommand.DoExecute = new Action<object>((obj) =>
            {
                Count = 0;
            });
            StartIOWindow.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            StartIOWindow.DoExecute = new Action<object>((obj) =>
            {
                IOParameter iOParameter = new IOParameter();
                iOParameter.Show();
            });
            SettingsCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            SettingsCommand.DoExecute = new Action<object>((obj) =>
            {
                MarkingSettingsPage settings = new MarkingSettingsPage();
                settings.ShowDialog();
            });
        }

        private void Loaded()
        {
            if (!ini)
            {
                _markController.Initialize(System.Environment.CurrentDirectory, true);
                Log.Suc("打标卡初始化成功");
                ini = true;
            }
            pub.subscribe("CSV", RecordCSV);
            _markModel.TodayProductDatas?.ForEach(data => { Products.Add(data); });
        }

        private void LoadEdzFile(object obj)
        {
            System.Windows.Controls.Image image = obj as System.Windows.Controls.Image;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "EZD|*.ezd";
            Nullable<bool> result=dlg.ShowDialog();
            if (result==true)
            {
                string filename = dlg.FileName;
                _markController.LoadEzdFile(filename);
                PriviewImage = _markController.GetCurPreviewImage((int)image.Width, (int)image.Height);
                m_nAllEntCount=_markController.GetEntityCount();
                m_strAllEntNameList=new string[m_nAllEntCount];
                for(int i=0;i<m_nAllEntCount;i++)
                {
                    string str = i.ToString();
                    _markController.SetEntityNameByIndex(i,str);
                    m_strAllEntNameList[i] = _markController.GetEntityNameByIndex(i);
                }
            }            
        }
        private void RefreshPreview(object obj)
        {
            System.Windows.Controls.Image image = obj as System.Windows.Controls.Image;
            PriviewImage = _markController.GetCurPreviewImage((int)image.Width, (int)image.Height);
        }
        private void RecordCSV(object sender,EventArgs e)
        {
            CSVEvents cSV=e as CSVEvents;
            CSVHelper.WriteCsv(CSVHelper._toString<ProductData>(cSV.ProductData));
        }
        private void Mark()
        {
            //int nMarkLoop = 0, nFreq = 0, nStartTC = 0, nLaserOffTC = 0, nEndTC = 0, nPolyTC = 0, nJumpPosTC = 0, nJumpDistTC = 0, nPulseNum = 0;
            //double dMarkSpeed = 0, dPowerRatio = 0, dCurrent = 0, dQPulseWidth = 0, dJumpSpeed = 0, dEndComp = 0, dAccDist = 0, dPointTime = 0, dFlySpeed = 0;
            //bool bPulsePointMode = false;
            //if(m_nAllEntCount==0)
            //{
            //    return;
            //}
            //if (Kind != "")
            //{
            //    if (Products.Count >= 100)
            //    {
            //        Products.RemoveAt(0);
            //        _markModel.TodayProductDatas.RemoveAt(0);
            //    }
            //    Stopwatch stopwatch = Stopwatch.StartNew();
            //    stopwatch.Start();
            //    _markController.Mark(false);
            //    stopwatch.Stop();
            //    ProcessingTime = stopwatch.Elapsed;
            //    ProductData product=new ProductData();
            //    int tem= _markController.GetPenNumberFromEnt(m_strAllEntNameList[0]);
            //    _markController.GetPenParam(tem, ref nMarkLoop, ref dMarkSpeed, ref dPowerRatio, ref dCurrent, ref nFreq, ref dQPulseWidth, ref nStartTC, ref nLaserOffTC, ref nEndTC, ref nPolyTC, ref dJumpSpeed, ref nJumpPosTC, ref nJumpDistTC, ref dEndComp, ref dAccDist, ref dPointTime, ref bPulsePointMode, ref nPulseNum, ref dFlySpeed);
            //    ProductData data= new ProductData() { IsPass = true, Power = dPowerRatio, ProductionID = Kind.ToUpper() + "-" + DateTime.Now.ToString("yyyy-MM-dd").Replace("-", "") + "-" + ID, ProcessTime = ProcessingTime, Time = DateTime.Now.ToString(), Type = Kind.ToUpper(), ZHeight = 30 };             
            //    Products.Add(data);
            //    pub.publish("CSV", new CSVEvents(data));
            //    ID++;
            //    Count++;
            //}
        }
    }
}
