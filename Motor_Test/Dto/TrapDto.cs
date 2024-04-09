using Motor_Test.Common.GTS;
using Motor_Test.Common;
using Motor_Test.Model;
using System;
using System.Threading.Tasks;
using Mapster;
using System.Windows.Controls;

namespace Motor_Test.Dto
{
    public class TrapDto
    {
        private readonly TrapModel _model;
        private IRunController gTS = GTS.GetGTS();
        public CommandAndNotifyBase PrfCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase SelectChangedCommand { get; set; } = new CommandAndNotifyBase();
        public TrapDto(TrapModel model) 
        {
            _model = model;
            _model.Adapt(this);
            Pul = int.Parse(CreateIni.ReadIni("Axis" + Axis.ToString(), "Puls", ""));
            PrfCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            PrfCommand.DoExecute = new Action<object>((obj) => { PrfRun(obj); });
            SelectChangedCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            SelectChangedCommand.DoExecute = new Action<object>((obj) => { SelectChanged(); });
        }

        private void SelectChanged()
        {
            Pul = int.Parse(CreateIni.ReadIni("Axis" + Axis.ToString(), "Puls", ""));
        }

        public short Axis { get; set; }
        public double Vel { get; set; }
        public double Acc { get; set; }
        public double Dec { get; set; }
        public int Pul { get; set; }
        public short SmoothTime { get; set; }
        public int Position { get; set; }
        public int Count { get; set; }
        public void ApplyChanges() => this.Adapt(this._model);
        public void DiscardChanges() => _model.Adapt(this);
        private void TrapRun()
        {
            ApplyChanges();
            gTS.Trap(short.Parse((Axis + 1).ToString()),_model);
        }

        private async void RoundTrap(Button btn)
        {
            for (int i = 0; i < this.Count; i++)
            {
                ApplyChanges();
                gTS.Trap(short.Parse((Axis + 1).ToString()), _model);
                await Task.Run(async () =>
                {
                    int AxisState;
                    //do
                    //{
                    //    gTS.GetSts(short.Parse((Axis + 1).ToString()), out AxisState);
                    //} while (((AxisState & 0x400) != 0) || ((AxisState & 0x800) == 0));
                    await Task.Delay(1000);
                });

                gTS.Trap(short.Parse((Axis + 1).ToString()), new TrapModel(_model) {Position=0 });
                await Task.Run(async () =>
                {
                    int AxisState;
                    //do
                    //{
                    //    gTS.GetSts(short.Parse((Axis + 1).ToString()), out AxisState);
                    //} while (((AxisState & 0x400) != 0) || ((AxisState & 0x800) == 0));
                    await Task.Delay(1000);
                });
            }
            btn.IsEnabled = true;
        }
        private void PrfRun(object obj)
        {
            Button btn = (Button)obj;
            btn.IsEnabled = false;
            if (this.Count == 0)
            {
                TrapRun();
            }
            RoundTrap(btn);
        }
    }
}
