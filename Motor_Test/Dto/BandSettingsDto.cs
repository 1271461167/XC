using Mapster;
using Motor_Test.Common;
using Motor_Test.Common.GTS;
using Motor_Test.Model;
using System;

namespace Motor_Test.Dto
{
    public class BandSettingsDto : CommandAndNotifyBase
    {
        private IRunController _runController = GTS.GetGTS();
        private readonly BandSettings _model;

        private int band;

        public int Band
        {
            get { return band; }
            set { band = value; this.DoNotify(); }
        }

        private int time;

        public int Time
        {
            get { return time; }
            set { time = value; this.DoNotify(); }
        }

        private int pul;

        public int Pul
        {
            get { return pul; }
            set { pul = value; this.DoNotify(); }
        }

        public short Axis { get; set; }
        public CommandAndNotifyBase ApplyCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase SaveCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase ReadFromCardCommand { get; set; } = new CommandAndNotifyBase();
        public CommandAndNotifyBase ReadFromIniCommand { get; set; } = new CommandAndNotifyBase();

        public BandSettingsDto(BandSettings band)
        {
            _model = band;
            _model.Adapt(this);
            //ReadFromIni();
            //Apply();
            //ReadFromCard();
            //Save();
            ApplyCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            ApplyCommand.DoExecute = new Action<object>((obj) => { Apply(); });
            SaveCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            SaveCommand.DoExecute = new Action<object>((obj) => { Save(); });
            ReadFromCardCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            ReadFromCardCommand.DoExecute = new Action<object>((obj) => { ReadFromCard(); });
            ReadFromIniCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            ReadFromIniCommand.DoExecute = new Action<object>((obj) => { ReadFromIni(); });

        }

        private void ReadFromIni()
        {
            Pul = int.Parse(CreateIni.ReadIni("Axis" + Axis.ToString(), "Puls", ""));
            Band = int.Parse(CreateIni.ReadIni("Axis" + Axis.ToString(), "Band", ""));
            Time = int.Parse(CreateIni.ReadIni("Axis" + Axis.ToString(), "Time", ""));
        }

        private void ReadFromCard()
        {
            int _band, _time;
            _runController.GetAxisBand(short.Parse((Axis + 1).ToString()), out _band, out _time);
            Time = _time;
            Band = _band;
        }

        private void Save()
        {
            CreateIni.WriteIni("Axis" + Axis.ToString(), "Puls", Pul.ToString());
            CreateIni.WriteIni("Axis" + Axis.ToString(), "Band", Band.ToString());
            CreateIni.WriteIni("Axis" + Axis.ToString(), "Time", Time.ToString());
        }

        private void Apply()
        {
            ApplyChanges();
            _runController.SetAxisBand(short.Parse((Axis + 1).ToString()), _model.Band, _model.Time);
        }

        public void ApplyChanges() => this.Adapt(this._model);
        public void DiscardChanges() => _model.Adapt(this);
    }
}
