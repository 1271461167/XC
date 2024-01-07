using _2023_12_11XiChun.Base;
using _2023_12_11XiChun.Model;
using _2023_12_11XiChun.Settings;
using _2023_12_11XiChun.View;
using System;
using System.Diagnostics;
using System.Windows.Controls;

namespace _2023_12_11XiChun.ViewModel
{
    public class MainPageViewModel : CommandBase
    {
        public static ParameterModel Parameter { get; set; } = ParameterModel.GetParameter();       
        public static MainPageModel mainPage { get; set; } = new MainPageModel { Process = Parameter.Process[0] };       
        public CommandBase SaveCommand { get; set; } = new CommandBase();
        public CommandBase CancelSaveCommand { get; set; } = new CommandBase();
        public CommandBase SelectChangedCommand { get; set; }=new CommandBase();
        public MainPageViewModel()
        {   
            NowPage now = NowPage.GetInstance();
            now.PageNow = NowPage.nowPage.ParameterPL;
            SaveCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            SaveCommand.DoExecute = new Action<object>((obj) => { SaveSettings(); });
            CancelSaveCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            CancelSaveCommand.DoExecute = new Action<object>((obj) => { CancelSaveSettings(); });
            SelectChangedCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            SelectChangedCommand.DoExecute = new Action<object>((obj) => { SelectChanged(obj); });
        }

        private void SelectChanged(object obj)
        {
            ComboBox box=obj as ComboBox;
            mainPage.Process = Parameter.Process[box.SelectedIndex];
            mainPage.SelectIndex = box.SelectedIndex;
            mainPage.Process.AllEntNameList = new string[mainPage.Process.AllEntCount];
            for (int i = 0; i < mainPage.Process.AllEntCount; i++)
            {
                string str = ((i + 1) * 100).ToString();
                JczLmc.SetEntityNameByIndex(i, str);
                mainPage.Process.AllEntNameList[i] = JczLmc.GetEntityNameByIndex(i);
            }
            HomePageViewModel.Process = mainPage.Process;
        }

        private void CancelSaveSettings()
        {
            Parameter.CancelSave();
            mainPage.StartMarkPort = MyApp.Default.StartMark;
            mainPage.MarkFinishedPort = MyApp.Default.MarkFinished;
            mainPage.MarkingPort = MyApp.Default.Marking;
            mainPage.LaserReadyInPort = MyApp.Default.ReadyIn;
            mainPage.LaserReadyOutPort = MyApp.Default.ReadyOut;
            mainPage.NGPort = MyApp.Default.NG;
            //Parameter.Process1.XVelocity = MyApp.Default.XVelocity;
            //Parameter.Process1.YVelocity = MyApp.Default.YVelocity;
            //Parameter.Process1.ObjCount = MyApp.Default.ObjCount;
            //Parameter.Process1.MoveCount = MyApp.Default.MoveCount;
            //Parameter.Process1.DelayTime = MyApp.Default.DelayTime;
            Parameter.XMotor.Pulse = MyApp.Default.XPulse;
            Parameter.YMotor.Pulse = MyApp.Default.YPulse;
            mainPage.MarkFinishedLevel = MyApp.Default.MarkFinishedLevel;
            mainPage.NGLevel = MyApp.Default.NGLevel;
            mainPage.ReadyInLevel = MyApp.Default.ReadyInLevel;
            mainPage.ReadyOutLevel = MyApp.Default.ReadyOutLevel;
            mainPage.MarkingLevel = MyApp.Default.MarkingLevel;
            mainPage.MarkFinishedWidth = MyApp.Default.MarkFinishedWidth;
            mainPage.NGWidth = MyApp.Default.NGWidth;
        }

        private void SaveSettings()
        {
            Parameter.Save();
            MyApp.Default.StartMark = mainPage.StartMarkPort;
            MyApp.Default.MarkFinished = mainPage.MarkFinishedPort;
            MyApp.Default.Marking = mainPage.MarkingPort;
            MyApp.Default.ReadyIn = mainPage.LaserReadyInPort;
            MyApp.Default.ReadyOut = mainPage.LaserReadyOutPort;
            MyApp.Default.NG = mainPage.NGPort;
            //MyApp.Default.XVelocity = Parameter.Process1.XVelocity;
            //MyApp.Default.YVelocity = Parameter.Process1.YVelocity;
            //MyApp.Default.ObjCount = Parameter.Process1.ObjCount;
            //MyApp.Default.DelayTime = Parameter.Process1.DelayTime;
            MyApp.Default.XPulse = Parameter.XMotor.Pulse;
            MyApp.Default.YPulse = Parameter.YMotor.Pulse;
            MyApp.Default.MarkFinishedLevel = mainPage.MarkFinishedLevel;
            MyApp.Default.NGLevel = mainPage.NGLevel;
            MyApp.Default.ReadyInLevel = mainPage.ReadyInLevel;
            MyApp.Default.ReadyOutLevel = mainPage.ReadyOutLevel;
            MyApp.Default.MarkingLevel = mainPage.MarkingLevel;
            MyApp.Default.MarkFinishedWidth = mainPage.MarkFinishedWidth;
            MyApp.Default.NGWidth = mainPage.NGWidth;
            //MyApp.Default.MoveCount=Parameter.Process1.MoveCount;
            MyApp.Default.Save();
        }
    }
}
