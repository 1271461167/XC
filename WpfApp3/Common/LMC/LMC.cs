using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp3.Common.LMC
{
    public class LMC : IMarkController
    {
        private static readonly object _lock = new object();
        private LMC()
        {
        }
        private static LMC instance=null;
        public static LMC GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                        instance = new LMC();
                }
            }
            return instance;
        }
        public void CommandHandler(int nRts)
        {
            if (nRts == 0) return;
            else
            {
                throw new Exception("JCZ:  " + JczLmc.GetErrorText(nRts));
                //Log.Error("JCZ:  " + JczLmc.GetErrorText(nRts));
            }

        }
        public int ChangeEntName(string strEntName, string strNewEntName)
        {
            throw new NotImplementedException();
        }

        public int ChangeTextByName(string EntName, string NewText)
        {
            throw new NotImplementedException();
        }

        public int ClearLibAllEntity()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public int CopyEnt(string strEntName, string strNewEntName)
        {
            throw new NotImplementedException();
        }

        public int DeleteEnt(string strEntName)
        {
            throw new NotImplementedException();
        }
        public ImageSource GetCurPreviewImage(int bmpwidth, int bmpheight)
        {
            Bitmap bmp = new Bitmap(JczLmc.GetCurPreviewImage(bmpwidth, bmpheight));           
            IntPtr hBitmap = bmp.GetHbitmap();
            ImageSource img = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero,Int32Rect.Empty,BitmapSizeOptions.FromEmptyOptions());
            if(!JczLmc.DeleteObject(hBitmap))
            {
                throw new Exception("内存释放错误");
            }
            return img;
        }

        public Image GetCurPreviewImageByName(string Entname, int bmpwidth, int bmpheight)
        {
            throw new NotImplementedException();
        }

        public ushort GetEntityCount()
        {
            throw new NotImplementedException();
        }

        public int GetEntSize(string strEntName, ref double dMinx, ref double dMiny, ref double dMaxx, ref double dMaxy, ref double dz)
        {
            throw new NotImplementedException();
        }

        public int GetOutPort(ref ushort data)
        {
            throw new NotImplementedException();
        }

        public int GetPenDisableState(int nPenNo, ref bool bDisableMark)
        {
            throw new NotImplementedException();
        }

        public int GetPenNumberFromEnt(string strEntName)
        {
            throw new NotImplementedException();
        }

        public int GetPenNumberFromName(string strEntName)
        {
            throw new NotImplementedException();
        }

        public int GetPenParam(int nPenNo, ref int nMarkLoop, ref double dMarkSpeed, ref double dPowerRatio, ref double dCurrent, ref int nFreq, ref double dQPulseWidth, ref int nStartTC, ref int nLaserOffTC, ref int nEndTC, ref int nPolyTC, ref double dJumpSpeed, ref int nJumpPosTC, ref int nJumpDistTC, ref double dEndComp, ref double dAccDist, ref double dPointTime, ref bool bPulsePointMode, ref int nPulseNum, ref double dFlySpeed)
        {
            throw new NotImplementedException();
        }

        public int GetTextByName(string strEntName, StringBuilder Text)
        {
            throw new NotImplementedException();
        }

        public void Initialize(string PathName, bool bTestMode)
        {
            CommandHandler(JczLmc.Initialize(PathName, bTestMode));
        }

        public int lmc1_GetEntityNameByIndex(int nEntityIndex, StringBuilder entname)
        {
            throw new NotImplementedException();
        }

        public void LoadEzdFile(string FileName)
        {
            JczLmc.LoadEzdFile(FileName);
        }

        public int Mark(bool Fly)
        {
            throw new NotImplementedException();
        }

        public int MarkEntity(string EntName)
        {
            throw new NotImplementedException();
        }

        public int MirrorEnt(string strEntName, double dCenx, double dCeny, bool bMirrorX, bool bMirrorY)
        {
            throw new NotImplementedException();
        }

        public int MoveEnt(string strEntName, double dMovex, double dMovey)
        {
            throw new NotImplementedException();
        }

        public int ReadPort(ref ushort data)
        {
            throw new NotImplementedException();
        }

        public int RedLightMarkByEnt(string EntName, bool bContour)
        {
            throw new NotImplementedException();
        }

        public int RedMark()
        {
            throw new NotImplementedException();
        }

        public int RedMarkContour()
        {
            throw new NotImplementedException();
        }

        public int RotateEnt(string strEntName, double dCenx, double dCeny, double dAngle)
        {
            throw new NotImplementedException();
        }

        public int SaveEntLibToFile(string strFileName)
        {
            throw new NotImplementedException();
        }

        public int ScaleEnt(string strEntName, double dCenx, double dCeny, double dScaleX, double dScaleY)
        {
            throw new NotImplementedException();
        }

        public void SetEntAllChildPen(string strEntName, int nPenNo)
        {
            throw new NotImplementedException();
        }

        public int SetEntityNameByIndex(int nEntityIndex, string entname)
        {
            throw new NotImplementedException();
        }

        public int SetPenDisableState(int nPenNo, bool bDisableMark)
        {
            throw new NotImplementedException();
        }

        public int SetPenParam(int nPenNo, int nMarkLoop, double dMarkSpeed, double dPowerRatio, double dCurrent, int nFreq, double dQPulseWidth, int nStartTC, int nLaserOffTC, int nEndTC, int nPolyTC, double dJumpSpeed, int nJumpPosTC, int nJumpDistTC, double dEndComp, double dAccDist, double dPointTime, bool bPulsePointMode, int nPulseNum, double dFlySpeed)
        {
            throw new NotImplementedException();
        }

        public int WritePort(ushort data)
        {
            throw new NotImplementedException();
        }
    }
}
