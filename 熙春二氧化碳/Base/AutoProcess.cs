using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _2023_12_11XiChun.Base
{
    public class AutoProcess:NotifyBase
    {

        private int delaytime;
        public int DelayTime
        {
            get { return delaytime; }
            set
            {
                delaytime = value;
                this.DoNotify();
            }
        }
        private double x_velocity;

        public double XVelocity
        {
            get { return x_velocity; }
            set { x_velocity = value;this.DoNotify(); }
        }
        private double y_velocity;

        public double YVelocity
        {
            get { return y_velocity; }
            set { y_velocity = value;this.DoNotify(); }
        }
        private int movecount;

        public int MoveCount
        {
            get { return movecount; }
            set 
            { 
                movecount = value;
                this.DoNotify();
                moveposition.Clear();
                for(int i=0;i<value;i++)
                {
                    moveposition.Add(new AxisPosition {ID=i+1});                  
                }                
            }
        }
        private ObservableCollection<AxisPosition> moveposition=new ObservableCollection<AxisPosition>();

        public ObservableCollection<AxisPosition> MovePosition
        {
            get { return moveposition; }
            set { moveposition = value; }
        }

        private int m_nAllEntCount;

        public int AllEntCount
        {
            get { return m_nAllEntCount; }
            set
            {
                m_nAllEntCount = value;
                rec.Clear();
                for (int i = 0; i < value; i++)
                {
                    rec.Add(new Rec_Data());
                }

            }
        }

        private string[] m_strAllEntNameList;

        public string[] AllEntNameList
        {
            get { return m_strAllEntNameList; }
            set { m_strAllEntNameList = value; }
        }

        private List<Rec_Data> rec = new List<Rec_Data>();

        public List<Rec_Data> Rec
        {
            get { return rec; }
            set { rec = value; }
        }
    }
}
