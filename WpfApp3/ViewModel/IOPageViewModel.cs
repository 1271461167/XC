using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using WpfApp3.Common;
using WpfApp3.Common.LMC;

namespace WpfApp3.ViewModel
{
    public class IOPageViewModel
    {
        IMarkController _markController = LMC.GetInstance();
        public ObservableCollection<bool> INS { get; set; } = new ObservableCollection<bool>() {false, false, false, false, false, false, false, false};
        public ObservableCollection<bool> OUTS { get; set; } = new ObservableCollection<bool>() {false,false ,false, false, false, false, false, false};
        public IOPageViewModel()
        {
            GetIOState();
        }
        private void GetIOState()
        {
            ushort outs = 0;
            ushort ins = 0;
            _markController.GetOutPort(ref outs);
            _markController.ReadPort(ref ins);
            for(int i=0;i<OUTS.Count;i++)
            {
                OUTS[i]=((outs&(0x01<<i))!=0)?true:false;
            }
            for (int i = 0; i < INS.Count; i++)
            {
                INS[i] = ((ins & (0x01 << i)) != 0) ? true : false;
            }
        }
    }
}
