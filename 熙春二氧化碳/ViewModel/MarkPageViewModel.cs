using _2023_12_11XiChun.Base;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using _2023_12_11XiChun.Model;
using System.Drawing;
using System.Threading;
using System.Timers;
using System.Collections.ObjectModel;
using _2023_12_11XiChun.View;

namespace _2023_12_11XiChun.ViewModel
{
    public delegate void Change(int a);
    public class MarkPageViewModel : CommandBase
    {
        
        public static Change ch;

        public static Dictionary<string,AutoProcess> keys = new Dictionary<string,AutoProcess>();
        private static bool IsInit = false;
        public static System.Timers.Timer _timer = null;
        public static AutoProcess Process { get; set; } = new AutoProcess();
        public ObservableCollection<bool> bools { get; set; } = new ObservableCollection<bool>();
        public ParameterModel Parameter { get; set; } = ParameterModel.GetParameter();
        private static CancellationTokenSource RedMarkCancellationTokenSource = null;
        public CommandBase LoadCommand { get; set; } = new CommandBase();
        public CommandBase RefreshCommand { get; set; } = new CommandBase();
        public CommandBase MarkCommand { get; set; } = new CommandBase();
        public CommandBase RedMarkCommand { get; set; } = new CommandBase();
        public CommandBase CloseRedMarkCommand { get; set; } = new CommandBase();
        public CommandBase CloseCardCommand { get; set; } = new CommandBase();
        public CommandBase SetPortCommand { get; set; } = new CommandBase();
        public CommandBase BlindLoadCommand { get; set; }= new CommandBase();
        public static MarkPageModel markPageModel { get; set; } = new MarkPageModel();
        public MarkPageViewModel()
        {
            if (_timer == null)
            {
                Init();
                _timer = new System.Timers.Timer();
                _timer.AutoReset = true;
                _timer.Interval = 200;
                _timer.Elapsed += OnTmr;
                _timer.Start();
            }
            NowPage now = NowPage.GetInstance();
            now.PageNow = NowPage.nowPage.MarkPL;
            SetPortCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            SetPortCommand.DoExecute = new Action<object>((obj) => { SetPort(obj); });
            LoadCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            LoadCommand.DoExecute = new Action<object>((obj) => { LoadTemplate(obj); });
            RefreshCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            RefreshCommand.DoExecute = new Action<object>((obj) => { Refresh(obj); });
            MarkCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            MarkCommand.DoExecute = new Action<object>((obj) => { Mark(); });
            RedMarkCommand.DoCanExecute = new Func<object, bool>((obj) => { return RedEnable(); });
            RedMarkCommand.DoExecute = new Action<object>((obj) =>
            {
                RedMarkCancellationTokenSource = new CancellationTokenSource();
                Task task = new Task(() => { RedMarkFunction(RedMarkCancellationTokenSource.Token); }, RedMarkCancellationTokenSource.Token, TaskCreationOptions.LongRunning);
                task.Start();
            });
            CloseRedMarkCommand.DoCanExecute = new Func<object, bool>((obj) => { return CloseRedEnable(); });
            CloseRedMarkCommand.DoExecute = new Action<object>((obj) => { CloseRedMarkFunction(); });
            CloseCardCommand.DoCanExecute = new Func<object, bool>((obj) => { return CanClose(); });
            CloseCardCommand.DoExecute = new Action<object>((obj) => { CloseCard(); });
            BlindLoadCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            BlindLoadCommand.DoExecute = new Action<object>((obj) => { BlindLoad(); });
        }

        private void BlindLoad()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Parameter.Bprocess.Path = dlg.FileNames;
            }
        }

        private void SetPort(object obj)
        {
            MarkPage markPage=obj as MarkPage;
            ushort uData = 0;
            int nErr1 = JczLmc.GetOutPort(ref uData);
            int nCurData = (int)uData;
            int nAimData = 1 << markPage.iocom.SelectedIndex;
            if (markPage.ishigh.IsChecked==true)
            {
                nAimData |= nCurData;
            }
            else if(markPage.ishigh.IsChecked == false)
            {
                nAimData = (~nAimData) & nCurData;
            }
            else
            {
                return;
            }
            int nErr2 = JczLmc.WritePort((ushort)nAimData);
        }

        private void OnTmr(object sender, ElapsedEventArgs e)
        {
            ushort data = 0;
            int nErr = JczLmc.ReadPort(ref data);            
            System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>
            {                
                Parameter.IN[0] = ((data & 0x01) != 0) ? true : false;
                Parameter.IN[1] = ((data & 0x02) != 0) ? true : false;
                Parameter.IN[2] = ((data & 0x04) != 0) ? true : false;
                Parameter.IN[3] = ((data & 0x08) != 0) ? true : false;
                Parameter.IN[4] = ((data & 0x010) != 0) ? true : false;
                Parameter.IN[5] = ((data & 0x020) != 0) ? true : false;
                Parameter.IN[6] = ((data & 0x040) != 0) ? true : false;
                Parameter.IN[7] = ((data & 0x080) != 0) ? true : false;
            }));
            nErr = JczLmc.GetOutPort(ref data);
            System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                Parameter.OUT[0] = ((data & 0x01) != 0) ? true : false;
                Parameter.OUT[1] = ((data & 0x02) != 0) ? true : false;
                Parameter.OUT[2] = ((data & 0x04) != 0) ? true : false;
                Parameter.OUT[3] = ((data & 0x08) != 0) ? true : false;
                Parameter.OUT[4] = ((data & 0x010) != 0) ? true : false;
                Parameter.OUT[5] = ((data & 0x020) != 0) ? true : false;
                Parameter.OUT[6] = ((data & 0x040) != 0) ? true : false;
                Parameter.OUT[7] = ((data & 0x080) != 0) ? true : false;
            }));

        }

        private bool CanClose()
        {
            if (IsInit == true)
            {
                return true;
            }
            else { return false; }
        }


        private void CloseCard()
        {
            int nErr = JczLmc.Close();
            if (nErr != 0)
            {
                markPageModel.Message = "关闭板卡失败";
            }
            IsInit = false;
        }

        private bool CloseRedEnable()
        {
            if (RedMarkCancellationTokenSource == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool RedEnable()
        {
            if (RedMarkCancellationTokenSource == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CloseRedMarkFunction()
        {
            RedMarkCancellationTokenSource?.Cancel();
            RedMarkCancellationTokenSource = null;
            markPageModel.Message = "红光关闭";
        }

        private void RedMarkFunction(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                int nErr = JczLmc.RedMark();
                if (nErr != 0)
                {

                    Thread.Sleep(1000);
                    markPageModel.Message = "红光失败:" + JczLmc.GetErrorText(nErr);

                }
            }
        }

        private void Mark()
        {
            Task.Run(() =>
            {
                int nErr = JczLmc.Mark(false);
                if (nErr != 0)
                {
                    markPageModel.Message = JczLmc.GetErrorText(nErr);
                }
            });
        }

        private void Refresh(object obj)
        {
            System.Windows.Controls.Image image = obj as System.Windows.Controls.Image;
            Bitmap bitmap = new Bitmap(JczLmc.GetCurPreviewImage((int)image.Width, (int)image.Height));
            markPageModel.MyImage = BitMapToImageSource.Bitmap2Imagesource(bitmap);
        }

        private void LoadTemplate(object obj)
        {
            System.Windows.Controls.Image image = obj as System.Windows.Controls.Image;
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                markPageModel.FileName = dlg.FileName;                              
                int nErr = JczLmc.LoadEzdFile(dlg.FileName);
                Bitmap bitmap = new Bitmap(JczLmc.GetCurPreviewImage((int)image.Width, (int)image.Height));
                markPageModel.MyImage = BitMapToImageSource.Bitmap2Imagesource(bitmap);
            }
        }

        private void Init()
        {
            int nErr = JczLmc.Initialize(Application.StartupPath, false);
            if (nErr == 0)
            {
                markPageModel.Message = "初始化成功";
            }
            IsInit = true;
        }
    }
}
