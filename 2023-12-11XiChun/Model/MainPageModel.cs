using _2023_12_11XiChun.Base;
using _2023_12_11XiChun.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_12_11XiChun.Model
{
    public class MainPageModel : NotifyBase
    {
        public MainPageModel()
        {
            
            StartMarkPort = MyApp.Default.StartMark;
            MarkFinishedPort = MyApp.Default.MarkFinished;
            LaserReadyInPort = MyApp.Default.ReadyIn;
            LaserReadyOutPort = MyApp.Default.ReadyOut;
            NGPort = MyApp.Default.NG;
            MarkingPort = MyApp.Default.Marking;
            MarkFinishedLevel = MyApp.Default.MarkFinishedLevel;
            NGLevel = MyApp.Default.NGLevel;
            ReadyInLevel = MyApp.Default.ReadyInLevel;
            ReadyOutLevel = MyApp.Default.ReadyOutLevel;
            MarkingLevel = MyApp.Default.MarkingLevel;
            MarkFinishedLow = !MyApp.Default.MarkFinishedLevel;
            NGLow = !MyApp.Default.NGLevel;
            ReadyInLow = !MyApp.Default.ReadyInLevel;
            ReadyOutLow = !MyApp.Default.ReadyOutLevel;
            MarkingLow = !MyApp.Default.MarkingLevel;
            MarkFinishedWidth = MyApp.Default.MarkFinishedWidth;
            NGWidth = MyApp.Default.NGWidth;
        }
        private int startmarkport;

        public int StartMarkPort
        {
            get { return startmarkport; }
            set { startmarkport = value; this.DoNotify(); }
        }
        private int markfinishedport;

        public int MarkFinishedPort
        {
            get { return markfinishedport; }
            set { markfinishedport = value; this.DoNotify(); }
        }
        private int laserreadyinport;

        public int LaserReadyInPort
        {
            get { return laserreadyinport; }
            set { laserreadyinport = value; this.DoNotify(); }
        }
        private int laserreadyoutport;

        public int LaserReadyOutPort
        {
            get { return laserreadyoutport; }
            set { laserreadyoutport = value; this.DoNotify(); }
        }
        private int ngport;

        public int NGPort
        {
            get { return ngport; }
            set { ngport = value; this.DoNotify(); }
        }
        private int markingort;

        public int MarkingPort
        {
            get { return markingort; }
            set { markingort = value; this.DoNotify(); }
        }
        private bool markfinishedlevel;

        public bool MarkFinishedLevel
        {
            get { return markfinishedlevel; }
            set { markfinishedlevel = value; this.DoNotify(); }
        }
        private bool nglevel;

        public bool NGLevel
        {
            get { return nglevel; }
            set { nglevel = value; this.DoNotify(); }
        }
        private bool readyinlevel;

        public bool ReadyInLevel
        {
            get { return readyinlevel; }
            set { readyinlevel = value; this.DoNotify(); }
        }
        private bool readyoutlevel;

        public bool ReadyOutLevel
        {
            get { return readyoutlevel; }
            set { readyoutlevel = value; this.DoNotify(); }
        }
        private bool markinglevel;

        public bool MarkingLevel
        {
            get { return markinglevel; }
            set { markinglevel = value; this.DoNotify(); }
        }
        private bool markfinishedlow;

        public bool MarkFinishedLow
        {
            get { return markfinishedlow; }
            set { markfinishedlow = value; this.DoNotify(); }
        }
        private bool nglow;

        public bool NGLow
        {
            get { return nglow; }
            set { nglow = value; this.DoNotify(); }
        }
        private bool readyinlow;

        public bool ReadyInLow
        {
            get { return readyinlow; }
            set { readyinlow = value; this.DoNotify(); }
        }
        private bool readyoutlow;

        public bool ReadyOutLow
        {
            get { return readyoutlow; }
            set { readyoutlow = value; this.DoNotify(); }
        }
        private bool markinglow;

        public bool MarkingLow
        {
            get { return markinglow; }
            set { markinglow = value; this.DoNotify(); }
        }
        private int markfinishedwidth;

        public int MarkFinishedWidth
        {
            get { return markfinishedwidth; }
            set { markfinishedwidth = value; this.DoNotify(); }
        }
        private int ngwidth;

        public int NGWidth
        {
            get { return ngwidth; }
            set { ngwidth = value; this.DoNotify(); }
        }

        private AutoProcess process;

        public AutoProcess Process
        {
            get { return process; }
            set { process = value; this.DoNotify(); }
        }

        private int selectindex;

        public int SelectIndex
        {
            get { return selectindex; }
            set { selectindex = value;this.DoNotify(); }
        }

    }
}
