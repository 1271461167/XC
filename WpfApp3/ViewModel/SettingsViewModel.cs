using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.Common;
using WpfApp3.Common.Ini;
using WpfApp3.Model;
using WpfApp3.View;

namespace WpfApp3.ViewModel
{
    public class SettingsViewModel
    {
        public CommandBase SaveCommand { get; set; }= new CommandBase();
        public ObservableCollection<IOSettings> IN_Settings { get; set; }=new ObservableCollection<IOSettings>();
        public ObservableCollection<IOSettings> OUT_Settings { get; set; }=new ObservableCollection<IOSettings>();
        public SettingsViewModel() 
        {
            IN_Settings.Add(new IOSettings() {Index=7,Name="触发信号：" });
            IN_Settings.Add(new IOSettings() { Index = 6, Name = "清报警：" });
            OUT_Settings.Add(new IOSettings() { Index = 5, Name = "完成信号：", Level = true, Width = 100 });
            SaveCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            SaveCommand.DoExecute = new Action<object>((obj) =>
            {
                CreateIni.WriteIni("",);
            });
        }

    }
}
