using Motor_Test.Common;
using Motor_Test.Global;
using Motor_Test.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Motor_Test.ViewModel
{
    public class MotorPageViewModel:CommandAndNotifyBase
    {
        public MotorArrayModel arrayModel { get; set; }= new MotorArrayModel();
        public JogMotorModel jogMotorModel { get; set; } = new JogMotorModel {Pul= MotorSettings.Motor_Setting[0].Puls };
        public MotorStsModel stsModel { get; set; }=MotorStsModel.GetInstance();
        public MotorSettingsModel settingsModel { get; set; }=new MotorSettingsModel();
        public MotorPageViewModel()
        {
            settingsModel.Axis = 0;
            settingsModel.Pul = MotorSettings.Motor_Setting[0].Puls;
            settingsModel.Band = MotorSettings.Motor_Setting[0].Band;
            settingsModel.Time = MotorSettings.Motor_Setting[0].Time;
                       
        }

    }
}
