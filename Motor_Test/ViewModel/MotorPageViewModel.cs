using Motor_Test.Common;
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
        public JogMotorModel jogMotorModel { get; set; } = new JogMotorModel();
        public MotorStsModel stsModel { get; set; }=new MotorStsModel();
        public MotorPageViewModel()
        {

        }

    }
}
