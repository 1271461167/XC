using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Threading;
using WpfApp3.Common;
using WpfApp3.Common.LMC;

namespace WpfApp3.ViewModel
{
    public class IOPageViewModel
    {
        IMarkController _markController = LMC.GetInstance();
        public CommandBase CloseCommand { get; set; } = new CommandBase();
        public CancellationTokenSource CancellationTokenSource { get; set; } = new CancellationTokenSource();
        private ushort _in;
        public ushort IN
        {
            get { return _in; }
            set 
            { 
                if (_in != value)
                {
                    _in = value;
                    for(int i=0;i<8;i++)
                    {
                        INS[i] = (value&(1<<i))!=0 ? true : false;
                    }
                }

            }
        }
        private ushort _out;
        public ushort OUT
        {
            get { return _out; }
            set
            {
                if (_out != value)
                {
                    _out = value;
                    for (int i = 0; i < 8; i++)
                    {
                        OUTS[i] = (value & (1 << i)) != 0 ? true : false;
                    }
                }

            }
        }

        public ObservableCollection<bool> INS { get; set; } = new ObservableCollection<bool>() { false, false, false, false, false, false, false, false };
        public ObservableCollection<bool> OUTS { get; set; } = new ObservableCollection<bool>() { false, false, false, false, false, false, false, false };

        public IOPageViewModel()
        {
            Task.Run(() => { GetIOState(CancellationTokenSource.Token); }, CancellationTokenSource.Token);
            CloseCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            CloseCommand.DoExecute = new Action<object>((obj) =>
            {
                CancellationTokenSource?.Cancel();
                CancellationTokenSource.Dispose();
                CancellationTokenSource = null;
            });
        }


        private void GetIOState(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                ushort ins = 0;
                ushort outs = 0;
                _markController.ReadPort(ref ins);
                _markController.GetOutPort(ref outs);
                IN = ins;
                OUT = outs;
                
            }

        }

    }
}
