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
        private readonly JogModel _model;
        public CommandAndNotifyBase JogPDownCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase JogPUpCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase JogNDownCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase JogNUpCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase SelectChangedCommand { get; set; } = new CommandAndNotifyBase();
        public JogDto(JogModel model)
        {
            _model = model;
            _model.Adapt(this);
            Pul = int.Parse(CreateIni.ReadIni("Axis" + Axis.ToString(), "Puls", ""));
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
            Pul = int.Parse(CreateIni.ReadIni("Axis" + Axis.ToString(), "Puls", ""));
        }

        private void JogNDown()
        {
            double Vel_Tem = Vel * Pul / 1000.0;
            double AccTime = Vel_Tem / Acc;
            double DecTime = Vel_Tem / Dec;
            AccTime.Adapt(_model.Acc);
            DecTime.Adapt(_model.Dec);
            Vel = -Vel;
            Vel.Adapt(_model.Vel);
            RunController.Jog(short.Parse((Axis + 1).ToString()), _model);
        }

        private void JogNUp()
        {
            RunController.Stop(short.Parse((Axis + 1).ToString()));
        }

        private void JogPUp()
        {
            RunController.Stop(short.Parse((Axis + 1).ToString()));
        }

        private void JogPDown()
        {
            double Vel_Tem = Vel * Pul / 1000.0;
            double AccTime = Vel_Tem / Acc;
            double DecTime = Vel_Tem / Dec;
            AccTime.Adapt(_model.Acc);
            DecTime.Adapt(_model.Dec);
            Vel.Adapt(_model.Vel);
            RunController.Jog(short.Parse((Axis + 1).ToString()),_model);
        }

        public short Axis { get; set; }
        public double Vel { get; set; }
        public double Acc { get; set; }
        public double Dec { get; set; }
        public int Pul { get; set; }
        public void ApplyChanges() =>this.Adapt(this._model);
        public void DiscardChanges()=>_model.Adapt(this);
    }
}
