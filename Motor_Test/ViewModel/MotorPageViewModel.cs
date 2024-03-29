using Motor_Test.Common;
using Motor_Test.Dto;
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
        public JogDto jogMotorModel { get; set; } = new JogDto(new JogModel());
        public MotorStsModel stsModel { get; set; }=MotorStsModel.GetInstance();
        public BandSettingsDto settingsModel { get; set; }=new BandSettingsDto(new BandSettings());
        public TrapDto prfMotorModel { get; set; }=new TrapDto(new TrapModel());
        public MotorPageViewModel()
        {

                       
        }

    }
}
