using Mapster;
using Motor_Test.Common;
using Motor_Test.Common.GTS;
using Motor_Test.Global;
using Motor_Test.Model;
using System;

namespace Motor_Test.Dto
{
    public class JogDto:CommandAndNotifyBase
    {
        private IRunController RunController = GTS.GetGTS();
        private readonly MotorModel _model;
        public CommandAndNotifyBase JogPDownCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase JogPUpCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase JogNDownCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase JogNUpCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase SelectChangedCommand { get; set; } = new CommandAndNotifyBase();
        public JogDto(MotorModel model)
        {
            _model = model;
            _model.Adapt(this);

            JogPDownCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            JogPDownCommand.DoExecute = new Action<object>((obj) => { JogPDown(); });
            JogPUpCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            JogPUpCommand.DoExecute = new Action<object>((obj) => { JogPUp(); });
            JogNUpCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            JogNUpCommand.DoExecute = new Action<object>((obj) => { JogNUp(); });
            JogNDownCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            JogNDownCommand.DoExecute = new Action<object>((obj) => { JogNDown(); });
            SelectChangedCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            SelectChangedCommand.DoExecute = new Action<object>((obj) => { SelectChanged(); });
        }

        private void SelectChanged()
        {
            this.Pul = MotorSettings.Motor_Setting[this.Axis].Puls;
        }

        private void JogNDown()
        {
            double Vel_Tem = Vel * Pul / 1000.0;
            double acc = Vel_Tem / AccTime;
            double dec = Vel_Tem / DecTime;
            RunController.Jog(short.Parse((Axis + 1).ToString()), new JogPrm() {Acc=acc,Dec=dec,Vel=Vel_Tem });
        }

        private void JogNUp()
        {
            RunController.Stop(this.Axis);
        }

        private void JogPUp()
        {
            RunController.Stop(this.Axis);
        }

        private void JogPDown()
        {
            double Vel_Tem = Vel * Pul / 1000.0;
            double Acc = Vel_Tem / AccTime;
            double Dec = Vel_Tem / DecTime;
            RunController.Jog(short.Parse((Axis + 1).ToString()),new JogPrm());
        }

        public short Axis { get; set; }
        public double Vel { get; set; }
        public double AccTime { get; set; }
        public double DecTime { get; set; }
        public int Pul { get; set; }
        public void ApplyChanges() =>this.Adapt(this._model);
        public void DiscardChanges()=>_model.Adapt(this);
    }
}
