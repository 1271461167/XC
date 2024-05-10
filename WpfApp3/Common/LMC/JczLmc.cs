using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;
using Color = System.Drawing.Color;

namespace WpfApp3.Common.LMC
{
    public class JczLmc
    {
        public static bool GetInPort(int index)
        {
            ushort uData = 0;
            int nErr = JczLmc.ReadPort(ref uData);
            int nbit = 1 << index;//目标位值预置为高
            nbit &= (int)uData;
            if (nbit == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public static void SetOutPutPort(int index, bool level)
        {
            ushort uData = 0;
            int nErr1 = JczLmc.GetOutPort(ref uData);
            int nCurData = (int)uData;
            int nAimData = 1 << index;
            if (level)
            {
                nAimData |= nCurData;
            }
            else
            {
                nAimData = (~nAimData) & nCurData;
            }
            int nErr2 = JczLmc.WritePort((ushort)nAimData);
        }
        public static string GetErrorText(int nErr)
        {
            switch (nErr)
            {
                case 0: return "成功";
                case 1: return "发现EZCAD已经在运行";
                case 2: return "找不到EZCAD.CFG";
                case 3: return "打开LMC失败";
                case 4: return "没有LMC设备";
                case 5: return "lmc设备版本错误";
                case 6: return "找不到设备配置文件MarkCfg";
                case 7: return "有报警信号";
                case 8: return "用户中断";
                case 9: return "不明错误";
                case 10: return "超时";
                case 11: return "未初始化";
                case 12: return "读文件失败";
                case 13: return "窗口为空";
                case 14: return "找不到字体";
                case 15: return "笔号错误";
                case 16: return "指定对象不是文本对象";
                case 17: return "保存文件失败";
                case 18: return "保存文件失败找不到指定对象";
                case 19: return "当前状态不能执行此操作";
                case 20: return "参数输入错误";
                default:
                    break;
            }
            return "参数输入错误";
        }

        public static string GetErrorText(int nErr, bool English)
        {
            if (English)
            {
                switch (nErr)
                {
                    case 0: return "Success";
                    case 1: return " Now have a working EACAD";
                    case 2: return "No found EZCAD.CFG";
                    case 3: return "Open LMC faild";
                    case 4: return "No LMC Board";
                    case 5: return "Lmc vision Error";
                    case 6: return " No found MarkCfg in Plug ";
                    case 7: return "Error Signal";
                    case 8: return "User Stop";
                    case 9: return "unknown error";
                    case 10: return "out time";
                    case 11: return "No Initialization";
                    case 12: return "Read File Error";
                    case 13: return "full Windows";
                    case 14: return "No found font";
                    case 15: return "Pen error";
                    case 16: return "object is not text";
                    case 17: return "save file fail";
                    case 18: return "save file fail becouse same object is no found";
                    case 19: return "Now state can not work as command";
                    case 20: return "error Parameter";
                    default:
                        break;
                }
                return "error Parameter";
            }
            else
            {
                switch (nErr)
                {
                    case 0: return "成功";
                    case 1: return "发现EZCAD已经在运行";
                    case 2: return "找不到EZCAD.CFG";
                    case 3: return "打开LMC失败";
                    case 4: return "没有LMC设备";
                    case 5: return "lmc设备版本错误";
                    case 6: return "找不到设备配置文件MarkCfg";
                    case 7: return "有报警信号";
                    case 8: return "用户中断";
                    case 9: return "不明错误";
                    case 10: return "超时";
                    case 11: return "未初始化";
                    case 12: return "读文件失败";
                    case 13: return "窗口为空";
                    case 14: return "找不到字体";
                    case 15: return "笔号错误";
                    case 16: return "指定对象不是文本对象";
                    case 17: return "保存文件失败";
                    case 18: return "保存文件失败找不到指定对象";
                    case 19: return "当前状态不能执行此操作";
                    case 20: return "参数输入错误";
                    default:
                        break;
                }
                return "参数输入错误";
            }
        }

        #region 设备

        /// <summary>
        /// 初始化函数库
        /// </summary>
        /// <param name="ezcad2.exe所处的目录的全路径名称"></param>
        /// <param name="指是否是测试模式,(测试模式控制卡相关函数无法工作)"></param>
        /// <param name="指拥有用户输入焦点的窗口，用于检测用户暂停消息。"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_Initial", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int InitializeTotal(string PathName, bool bTestMode, IntPtr MailForm);

        /// <summary>
        /// 初始化函数库
        /// PathName 是MarkEzd.dll所在的目录
        /// </summary>     
        [DllImport("MarkEzd", EntryPoint = "lmc1_Initial2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Initialize(string PathName, bool bTestMode);

        /// <summary>
        /// 释放函数库
        /// </summary>     
        [DllImport("MarkEzd", EntryPoint = "lmc1_Close", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Close();

        /// <summary>
        /// 得到设备参数配置对话框  
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetDevCfg", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDevCfg();

        /// <summary>
        /// 得到设备参数配置对话框+扩展轴  
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetDevCfg2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDevCfg2(bool bAxisShow0, bool bAxisShow1);

        /// <summary>
        /// 设置数据库的所有对象的旋转参数,不影响数据的显示,只是加工时才对对象进行旋转
        /// dMoveX x方向移动距离
        /// dMoveY y方向移动距离
        /// dCenterX旋转中心的x坐标
        /// dCenterY旋转中心的y坐标      
        /// dRotateAng为旋转角度,单位为弧度值
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetRotateMoveParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetRotateMoveParam(double dMoveX, double dMoveY, double dCenterX, double dCenterY, double dRotateAng);

        #endregion

        #region 加工

        /// <summary>
        /// 标刻当前数据库里面所有数据一次
        /// Fly 表示进行飞动标刻
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_Mark", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mark(bool Fly);

        /// <summary>
        /// 标刻指定对象一次
        /// EntName 指定对象的名字
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_MarkEntity", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int MarkEntity(string EntName);

        /// <summary>
        ///根据输入信号飞行标刻
        /// </summary>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_MarkFlyByStartSignal", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int MarkFlyByStartSignal();

        /// <summary>
        /// 飞行标刻指定对象一次
        /// EntName 指定对象的名字
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_MarkEntityFly", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int MarkEntityFly(string EntName);

        //加工直线
        [DllImport("MarkEzd", EntryPoint = "lmc1_MarkLine", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int MarkLine(double X1, double Y1, double X2, double Y2, int Pen);

        /// <summary>
        /// 标刻点
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Delay"></param>
        /// <param name="Pen"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_MarkPoint", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int MarkPoint(double X, double Y, double Delay, int Pen);

        /// <summary>
        /// 加工点对象
        /// </summary>
        /// <param name="nPointNum">点个数</param>
        /// <param name="ptbuf">ptBuf[][2]为每个点的跳转速度延时时间单位为us</param>
        /// <param name="dJumpSpeed">跳转速度</param>
        /// <param name="dLaserOnTimeMs">出光时间单位毫秒最小0.01MS</param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_MarkPointBuf2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int MarkPointBuf2(int nPointNum, [MarshalAs(UnmanagedType.LPArray)] double[,] ptbuf, double dJumpSpeed, double dLaserOnTimeMs);

        [DllImport("MarkEzd", EntryPoint = "lmc1_IsMarking", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool IsMarking();

        /// <summary>
        /// 强制停止标刻  
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_StopMark", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int StopMark();

        /// <summary>
        /// 红光预览当前数据库里面所有数据一次
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_RedLightMark", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int RedMark();

        /// <summary>
        /// 红光预览当前数据库里面所有数据轮廓一次
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_RedLightMarkContour", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int RedMarkContour();

        /// <summary>
        /// 红光预览当前数据库里面指定对象
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_RedLightMarkByEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int RedLightMarkByEnt(string EntName, bool bContour);

        /// <summary>
        /// 获取流水线速度
        /// </summary>
        /// <param name="FlySpeed"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetFlySpeed", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetFlySpeed(ref double FlySpeed);


        /// <summary>
        /// 控制振镜直接运动到指定坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_GotoPos", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GotoPos(double x, double y);



        /// <summary>
        /// 获取当前振镜的命令坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetCurCoor", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetCurCoor(ref double x, ref double y);

        #endregion

        #region 文件

        /// <summary>
        /// 载入ezd文件到当前数据库里面,并清除旧的数据库
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_LoadEzdFile", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int LoadEzdFile(string FileName);

        /// <summary>
        /// 保存当前数据到指定文件
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SaveEntLibToFile", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SaveEntLibToFile(string strFileName);

        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);

        [DllImport("MarkEzd", EntryPoint = "lmc1_GetPrevBitmap2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetCurPrevBitmap(int bmpwidth, int bmpheight);
        /// <summary>
        /// 得到当前数据库里面数据的预览图片
        /// </summary>  
        public static Image GetCurPreviewImage(int bmpwidth, int bmpheight)
        {
            IntPtr pBmp = GetCurPrevBitmap(bmpwidth, bmpheight);
            Image img = Image.FromHbitmap(pBmp);
            DeleteObject(pBmp);
            return img;
        }

        [DllImport("MarkEzd", EntryPoint = "lmc1_GetPrevBitmapByName2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetPrevBitmapByName(string EntName, int bmpwidth, int bmpheight);
        /// <summary>
        /// 得到当前数据库里面指定对象的预览图片
        /// </summary>
        public static Image GetCurPreviewImageByName(string Entname, int bmpwidth, int bmpheight)
        {
            IntPtr pBmp = GetPrevBitmapByName(Entname, bmpwidth, bmpheight);
            Image img = Image.FromHbitmap(pBmp);
            DeleteObject(pBmp);
            return img;
        }

        #endregion

        #region 对象

        /// <summary>
        /// 得到指定对象的尺寸信息
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetEntSize", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEntSize(string strEntName, ref double dMinx, ref double dMiny, ref double dMaxx, ref double dMaxy, ref double dz);

        /// <summary>
        /// 移动数据库中指定名称的对象
        /// dMoveX x方向移动距离
        /// dMoveY y方向移动距离
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_MoveEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int MoveEnt(string strEntName, double dMovex, double dMovey);

        /// <summary>
        /// 对数据库中指定名称的对象进行比例变换
        /// 变换前所有构成对象的点到变换中心距离按变换比例变换，对应为变换后的图形坐标。
        /// dCenx,dCeny变换中心的坐标
        /// dScaleX x方向变换比例
        /// dScaleY y方向变换比例
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_ScaleEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ScaleEnt(string strEntName, double dCenx, double dCeny, double dScaleX, double dScaleY);

        /// <summary>
        /// 对数据库中指定名称的对象进行镜像变换
        /// dCenx,dCeny镜像中心的坐标
        /// bMirrorX= true X镜像 
        /// bMirrorY= true Y镜像
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_MirrorEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int MirrorEnt(string strEntName, double dCenx, double dCeny, bool bMirrorX, bool bMirrorY);


        /// <summary>
        /// 对数据库中指定名称的对象进行旋转变换
        /// dCenx旋转中心的x坐标
        /// dCeny旋转中心的y坐标      
        /// dAngle为旋转角度,单位为弧度值
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_RotateEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RotateEnt(string strEntName, double dCenx, double dCeny, double dAngle);

        ///<summary>
        /// 复制指定名称对象，并命名
        ///<summary>
        [DllImport("MarkEzd", EntryPoint = "lmc1_CopyEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CopyEnt(string strEntName, string strNewEntName);

        /// <summary>
        /// 得到对象总数
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetEntityCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern ushort GetEntityCount();


        [DllImport("MarkEzd", EntryPoint = "lmc1_GetEntityName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int lmc1_GetEntityNameByIndex(int nEntityIndex, StringBuilder entname);
        /// <summary>
        /// 得到指定索引号的对象的名称。
        /// </summary>     
        public static string GetEntityNameByIndex(int nEntityIndex)
        {
            StringBuilder str = new StringBuilder("", 255);
            lmc1_GetEntityNameByIndex(nEntityIndex, str);
            return str.ToString();
        }

        /// <summary>
        /// 设定指定索引号的对象的名称。
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetEntityName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetEntityNameByIndex(int nEntityIndex, string entname);

        ///<summary>
        /// 重命名指定名称对象
        ///<summary>
        [DllImport("MarkEzd", EntryPoint = "lmc1_ChangeEntName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ChangeEntName(string strEntName, string strNewEntName);


        /// <summary>
        /// 对数据库中指定名称的对象群组
        /// strEntName1被群组的对象1
        /// strEntName2被群组的对象2
        /// strGroupName群组后对象名      
        /// nGroupPen群组后对象笔号
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GroupEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GroupEnt(string strEntName1, string strEntName2, string strGroupName, int nGroupPen);

        /// <summary>
        /// 对数据库中指定名称的对象解散群组
        /// strEntName1被解散群组的对象
        /// </summary>
        [DllImport("MarkEzd", EntryPoint = "lmc1_UnGroupEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int UnGroupEnt(string strGroupName);

        [DllImport("MarkEzd", EntryPoint = "lmc1_GroupEnt2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int lmc1_GroupEnt2(string[] strEntName, int nEntCount, string strGroupName, int nGroupPen);

        /// <summary>
        /// 彻底解散群组对象为曲线
        /// </summary>
        /// <param name="GroupName"></param>
        /// <param name="nFlag"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_UnGroupEnt2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int UnGroupEnt2(string GroupName, int nFlag);

        /// <summary>
        /// 获取当前图片对象参数
        /// </summary> 
        /// <param name="strEntName">位图对象名称</param>
        /// <param name="strImageFileName">位图对象路径</param>
        /// <param name="nBmpAttrib">位图参数</param>
        /// <param name="nScanAttrib">扫描参数</param>
        /// <param name="dBrightness">亮度设置[-1,1]</param>
        /// <param name="dContrast">对比度设置[-1,1]</param>
        /// <param name="dPointTime">打点时间设置</param>
        /// <param name="nImportDpi">DPI</param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetBitmapEntParam2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetBitmapEntParam2(string strEntName,
                                                        StringBuilder strImageFileName,
                                                        ref int nBmpAttrib,
                                                        ref int nScanAttrib,
                                                        ref double dBrightness,
                                                        ref double dContrast,
                                                        ref double dPointTime,
                                                        ref int nImportDpi,
                                                        ref int bDisableMarkLowGrayPt,
                                                        ref int nMinLowGrayPt
                                                        );

        [DllImport("MarkEzd", EntryPoint = "lmc1_SetBitmapEntParam2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetBitmapEntParam(string strEntName,
                                                         string strImageFileName,
                                                         int nBmpAttrib,
                                                         int nScanAttrib,
                                                         double dBrightness,
                                                         double dContrast,
                                                         double dPointTime,
                                                         int nImportDpi,
                                                         bool bDisableMarkLowGrayPt,
                                                         int nMinLowGrayPt
                                                          );

        /// <summary>
        /// 将指定对象移动到特对对象前面
        /// </summary>
        /// <param name="nMoveEnt"></param>
        /// <param name="GoalEnt"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_MoveEntityBefore", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int MoveEntityBefore(int nMoveEnt, int GoalEnt);

        [DllImport("MarkEzd", EntryPoint = "lmc1_SetBitmapEntParam3", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetBitmapEntParam3(string strEntName,
                                                        double dDpiX,
                                                        double dDpiY,
                                                        [MarshalAs(UnmanagedType.LPArray)] byte[] bGrayScaleBuf);
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetBitmapEntParam3", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetBitmapEntParam3(string strEntName,
                                                          ref double dDpiX,
                                                          ref double dDpiY,
                                                          byte[] bGrayScaleBuf);

        /// <summary>
        /// 将指定对象移动到特定对象后面
        /// </summary>
        /// <param name="nMoveEnt"></param>
        /// <param name="GoalEnt"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_MoveEntityAfter", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int MoveEntityAfter(int nMoveEnt, int GoalEnt);


        /// <summary>
        /// 20140228 将所有对象顺序颠倒
        /// </summary>
        /// <param name="nMoveEnt"></param>
        /// <param name="GoalEnt"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_ReverseAllEntOrder", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReverseAllEntOrder();


        #endregion

        #region 端口

        /// <summary>
        /// 读当前输入端口
        /// data 为当前输入端口的值,
        /// Bit0是In0的状态,Bit0=1表示In0为高电平,Bit0=0表示In0为低电平
        /// Bit1是In1的状态,Bit1=1表示In1为高电平,Bit1=0表示In1为低电平
        /// ........
        /// Bit15是In15的状态,Bit15=1表示In15为高电平,Bit15=0表示In15为低电平
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_ReadPort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReadPort(ref ushort data);

        /// <summary>
        /// 设置当前输出端口输出
        /// data 为当前输口端口要设置的值,
        /// Bit0是Out0的状态,Bit0=1表示Out0为高电平,Bit0=0表示Out0为低电平
        /// Bit1是Out1的状态,Bit1=1表示Out1为高电平,Bit1=0表示Out1为低电平
        /// ........
        /// Bit15是Out15的状态,Bit15=1表示Out15为高电平,Bit15=0表示Out15为低电平
        /// </summary>  
        [DllImport("MarkEzd", EntryPoint = "lmc1_WritePort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int WritePort(ushort data);

        // 获取当前设备输出口状态值
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetOutPort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetOutPort(ref ushort data);

        //直接打开激光
        [DllImport("MarkEzd", EntryPoint = "lmc1_LaserOn", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int LaserOn(bool bOpen);

        #endregion

        #region 文本

        /// <summary>
        /// 更改数据库中指定名称的文本对象的内容为指定文本
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_ChangeTextByName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ChangeTextByName(string EntName, string NewText);

        ///<summary>
        ///得到指定对象的文本
        ///<summary>
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetTextByName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetTextByName(string strEntName, StringBuilder Text);

        /// <summary>
        /// 重置序列号对象
        /// </summary>
        /// <param name="TextName"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_TextResetSn", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int TextResetSn(string TextName);

        #region 字体

        public const uint LMC1_FONT_JSF = 1; //JczSingleLine字体
        public const uint LMC1_FONT_TTF = 2; //TrueType字体
        public const uint LMC1_FONT_DMF = 4; //DotMatrix字体
        public const uint LMC1_FONT_BCF = 8; //Barcode字体

        public struct FontRecord
        {
            public string fontname;//字体名称
            public uint fontattrib;//字体属性
        }

        /// <summary>
        /// 得到数据库中字体记录的总数
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetFontRecordCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetFontRecordCount(ref int fontCount);

        /// <summary>
        /// 得到系统中指定序号的字体记录的名称和属性
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetFontRecord", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetFontRecordByIndex(int fontIndex, StringBuilder fontName, ref uint fontAttrib);

        /// <summary>
        /// 得到系统中所有可以使用的字体名称和属性
        /// </summary> 
        public static bool GetAllFontRecord(ref FontRecord[] fonts)
        {
            int fontnum = 0;
            if (GetFontRecordCount(ref fontnum) != 0)
            {
                return false;
            }
            if (fontnum == 0)
            {
                return true;
            }
            fonts = new FontRecord[fontnum];
            StringBuilder str = new StringBuilder("", 255);
            uint fontAttrib = 0;
            for (int i = 0; i < fontnum; i++)
            {
                GetFontRecordByIndex(i, str, ref fontAttrib);
                fonts[i].fontname = str.ToString(); ;
                fonts[i].fontattrib = fontAttrib;
            }
            return true;
        }

        #endregion

        /// <summary>
        /// 获取字体的参数，不是针对对象的，而是针对字体
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetFontParam3", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetFontParam3(string strFontName,
                                                     ref double CharHeight,
                                                     ref double CharWidthRatio,
                                                     ref double CharAngle,
                                                     ref double CharSpace,
                                                     ref double LineSpace,
                                                     ref bool EqualCharWidth,
                                                     ref int nTextAlign,
                                                     ref bool bBold,
                                                     ref bool bItalic);

        /// <summary>
        /// 设置字体的参数，在下次添加文本的时候起效
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetFontParam3", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetFontParam3(string fontname,
                                                    double CharHeight,
                                                    double CharWidthRatio,
                                                    double CharAngle,
                                                    double CharSpace,
                                                    double LineSpace,
                                                    double spaceWidthRatio,
                                                    bool EqualCharWidth,
                                                    int nTextAlign,
                                                    bool bBold,
                                                    bool bItalic);



        /// <summary>
        /// 得到指定文本对象参数
        /// </summary>
        /// <param name="EntName"></param>对象名称
        /// <param name="FontName"></param>字体名称
        /// <param name="CharHeight"></param>字符高度
        /// <param name="CharWidthRatio"></param>字符宽度
        /// <param name="CharAngle"></param>字符倾角
        /// <param name="CharSpace"></param>字符间距
        /// <param name="LineSpace"></param>行间距lmc1_GetEzdFilePrevBitmap
        /// <param name="EqualCharWidth"></param>等字符宽度
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetTextEntParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetTextEntParam(string EntName,
                                                         StringBuilder FontName,
                                                      ref double CharHeight,
                                                      ref double CharWidthRatio,
                                                      ref double CharAngle,
                                                      ref double CharSpace,
                                                      ref double LineSpace,
                                                      ref bool EqualCharWidth);

        [DllImport("MarkEzd", EntryPoint = "lmc1_SetTextEntParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetTextEntParam(string EntName,
                                                        double CharHeight,
                                                        double CharWidthRatio,
                                                        double CharAngle,
                                                        double CharSpace,
                                                        double LineSpace,
                                                        bool EqualCharWidth);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="EntName"></param>
        /// <param name="FontName"></param>
        /// <param name="CharHeight"></param>
        /// <param name="CharWidthRatio"></param>
        /// <param name="CharAngle"></param>
        /// <param name="CharSpace"></param>
        /// <param name="LineSpace"></param>
        /// <param name="spaceWidthRatio"></param>
        /// <param name="EqualCharWidth"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetTextEntParam2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetTextEntParam2(string EntName,
                                                        StringBuilder FontName,
                                                     ref double CharHeight,
                                                     ref double CharWidthRatio,
                                                     ref double CharAngle,
                                                     ref double CharSpace,
                                                     ref double LineSpace,
                                                     ref double spaceWidthRatio,
                                                     ref bool EqualCharWidth);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="EntName"></param>
        /// <param name="fontname"></param>
        /// <param name="CharHeight"></param>
        /// <param name="CharWidthRatio"></param>
        /// <param name="CharAngle"></param>
        /// <param name="CharSpace"></param>
        /// <param name="LineSpace"></param>
        /// <param name="spaceWidthRatio"></param>
        /// <param name="EqualCharWidth"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetTextEntParam2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetTextEntParam2(string EntName,
                                                         string fontname,
                                                        double CharHeight,
                                                        double CharWidthRatio,
                                                        double CharAngle,
                                                        double CharSpace,
                                                        double LineSpace,
                                                        double spaceWidthRatio,
                                                        bool EqualCharWidth);


        [DllImport("MarkEzd", EntryPoint = "lmc1_GetTextEntParam4", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetTextEntParam4(string EntName,
                                                       StringBuilder FontName,
                                                       ref int nTextSpaceMode,
                                                       ref double dTextSpace,
                                                ref double CharHeight,
                                                ref double CharWidthRatio,
                                                ref double CharAngle,
                                                ref double CharSpace,
                                                ref double LineSpace,
                                                ref double dNullCharWidthRatio,
                                                ref int nTextAlign,
                                                ref bool bBold,
                                                ref bool bItalic);



        [DllImport("MarkEzd", EntryPoint = "lmc1_SetTextEntParam4", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetTextEntParam4(string EntName,
                                                   string fontname,
                                                   int nTextSpaceMode,
                                                  double dTextSpace,
                                                  double CharHeight,
                                                  double CharWidthRatio,
                                                  double CharAngle,
                                                  double CharSpace,
                                                  double LineSpace,
                                                  double spaceWidthRatio,
                                                    int nTextAlign,
                                                    bool bBold,
                                                    bool bItalic);

        [DllImport("MarkEzd", EntryPoint = "lmc1_GetCircleTextParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetCircleTextParam(string pEntName,
                                                  ref double dCenX,
                                                  ref double dCenY,
                                                  ref double dCenZ,
                                                  ref double dCirDiameter,
                                                  ref double dCirBaseAngle,
                                                  ref bool bCirEnableAngleLimit,
                                                  ref double dCirAngleLimit,
                                                  ref int nCirTextFlag);

        [DllImport("MarkEzd", EntryPoint = "lmc1_SetCircleTextParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetCircleTextParam(string pEntName,
                                                    double dCenX,
                                                    double dCenY,
                                                    double dCenZ,
                                                    double dCirDiameter,
                                                    double dCirBaseAngle,
                                                    bool bCirEnableAngleLimit,
                                                    double dCirAngleLimit,
                                                    int nCirTextFlag);
        #endregion

        #region 笔号

        /// <summary>
        /// 得到指定笔号的参数
        /// nPenNo要设置的笔号0-255
        /// nMarkLoop标刻次数
        /// dMarkSpeed 加工速度 mm/s或者inch/mm,取决于markdll.dll的当前单位
        /// dPowerRatio功率百分比 0-100%
        /// dCurrent电流A
        /// nFreq频率Hz
        /// nQPulseWidth Q脉冲宽度 us
        /// nStartTC 开光延时 us
        /// nLaserOffTC 关光延时 us
        /// nEndTC  结束延时 us
        /// nPolyTC 多边形拐角延时us
        /// dJumpSpeed 跳转速度 mm/s或者inch/mm,取决于markdll.dll的当前单位 
        /// nJumpPosTC 跳转位置延时 us
        /// nJumpDistTC 跳转距离延时 us
        /// dEndComp 末点补偿距离 mm或者inch,取决于markdll.dll的当前单位 
        /// dAccDist 加速距离 mm或者inch
        /// dPointTime 打点时间ms
        /// bPulsePointMode 打点模式 true使能
        /// nPulseNum 打点个数
        /// dFlySpeed 流水线速度 mm/s或者inch/mm
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetPenParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPenParam(int nPenNo,
                     ref int nMarkLoop,
                     ref double dMarkSpeed,
                     ref double dPowerRatio,
                     ref double dCurrent,
                     ref int nFreq,
                     ref double dQPulseWidth,
                     ref int nStartTC,
                     ref int nLaserOffTC,
                     ref int nEndTC,
                     ref int nPolyTC,
                     ref double dJumpSpeed,
                     ref int nJumpPosTC,
                     ref int nJumpDistTC,
                     ref double dEndComp,
                     ref double dAccDist,
                     ref double dPointTime,
                     ref bool bPulsePointMode,
                     ref int nPulseNum,
                     ref double dFlySpeed);

        /// <summary>
        /// 20111201 添加
        /// 得到指定笔号的参数
        /// nPenNo要设置的笔号0-255
        /// nMarkLoop标刻次数
        /// dMarkSpeed 加工速度 mm/s或者inch/mm,取决于markdll.dll的当前单位
        /// dPowerRatio功率百分比 0-100%
        /// dCurrent电流A
        /// nFreq频率Hz
        /// nQPulseWidth Q脉冲宽度 us
        /// nStartTC 开光延时 us
        /// nLaserOffTC 关光延时 us
        /// nEndTC  结束延时 us
        /// nPolyTC 多边形拐角延时us
        /// dJumpSpeed 跳转速度 mm/s或者inch/mm,取决于markdll.dll的当前单位 
        /// nJumpPosTC 跳转位置延时 us
        /// nJumpDistTC 跳转距离延时 us
        /// dEndComp 末点补偿距离 mm或者inch,取决于markdll.dll的当前单位 
        /// dAccDist 加速距离 mm或者inch
        /// dPointTime 打点时间ms
        /// bPulsePointMode 打点模式 true使能
        /// nPulseNum 打点个数
        /// dFlySpeed 流水线速度 mm/s或者inch/mm
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetPenParam2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPenParam2(int nPenNo,
                     ref int nMarkLoop,
                     ref double dMarkSpeed,
                     ref double dPowerRatio,
                     ref double dCurrent,
                     ref int nFreq,
                     ref double dQPulseWidth,
                     ref int nStartTC,
                     ref int nLaserOffTC,
                     ref int nEndTC,
                     ref int nPolyTC,
                     ref double dJumpSpeed,
                     ref int nJumpPosTC,
                     ref int nJumpDistTC,
                     ref double dPointTime,
                     ref int nSpiWave,
                     ref bool bWobbleMode,
                     ref double bWobbleDiameter,
                     ref double bWobbleDist);


        [DllImport("MarkEzd", EntryPoint = "lmc1_GetPenParam4", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPenParam4(int nPenNo,
                                                        StringBuilder pStrName,
                                                        ref int clr,
                                                        ref bool bDisableMark,
                                                        ref bool bUseDefParam,
                                                        ref int nMarkLoop,
                                                        ref double dMarkSpeed,
                                                        ref double dPowerRatio,
                                                        ref double dCurrent,
                                                        ref int nFreq,
                                                        ref double dQPulseWidth,
                                                        ref int nStartTC,
                                                        ref int nLaserOffTC,
                                                        ref int nEndTC,
                                                        ref int nPolyTC,
                                                        ref double dJumpSpeed,
                                                        ref int nMinJumpDelayTCUs,
                                                        ref int nMaxJumpDelayTCUs,
                                                        ref double dJumpLengthLimit,
                                                        ref double dPointTimeMs,
                                                        ref bool nSpiSpiContinueMode,
                                                        ref int nSpiWave,
                                                        ref int nYagMarkMode,
                                                        ref bool bPulsePointMode,
                                                        ref int nPulseNum,
                                                        ref bool bEnableACCMode,
                                                        ref double dEndComp,
                                                        ref double dAccDist,
                                                        ref double dBreakAngle,
                                                        ref bool bWobbleMode,
                                                        ref double bWobbleDiameter,
                                                        ref double bWobbleDist);


        /// <summary>
        /// 设置指定笔号的参数
        /// nPenNo要设置的笔号0-255
        /// nMarkLoop标刻次数
        /// dMarkSpeed 加工速度 mm/s或者inch/mm,取决于markdll.dll的当前单位
        /// dPowerRatio功率百分比 0-100%
        /// dCurrent电流A
        /// nFreq频率Hz
        /// nQPulseWidth Q脉冲宽度 us
        /// nStartTC 开光延时 us
        /// nLaserOffTC 关光延时 us
        /// nEndTC  结束延时 us
        /// nPolyTC 多边形拐角延时us
        /// dJumpSpeed 跳转速度 mm/s或者inch/mm,取决于markdll.dll的当前单位 
        /// nJumpPosTC 跳转位置延时 us
        /// nJumpDistTC 跳转距离延时 us
        /// dEndComp 末点补偿距离 mm或者inch,取决于markdll.dll的当前单位 
        /// dAccDist 加速距离 mm或者inch
        /// dPointTime 打点时间ms
        /// bPulsePointMode 打点模式 true使能
        /// nPulseNum 打点个数
        /// dFlySpeed 流水线速度 mm/s或者inch/mm
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetPenParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetPenParam(int nPenNo,
                             int nMarkLoop,
                             double dMarkSpeed,
                             double dPowerRatio,
                             double dCurrent,
                             int nFreq,
                             double dQPulseWidth,
                             int nStartTC,
                             int nLaserOffTC,
                             int nEndTC,
                             int nPolyTC,
                             double dJumpSpeed,
                             int nJumpPosTC,
                             int nJumpDistTC,
                             double dEndComp,
                             double dAccDist,
                             double dPointTime,
                             bool bPulsePointMode,
                             int nPulseNum,
                             double dFlySpeed);





        //////////////////////////////////////
        ///20110329添加更改设置笔号参数
        /////////////////////////////////////////
        /// <summary>
        /// 设置指定笔号的参数
        /// nPenNo要设置的笔号0-255
        /// nMarkLoop标刻次数
        /// dMarkSpeed 加工速度 mm/s或者inch/mm,取决于markdll.dll的当前单位
        /// dPowerRatio功率百分比 0-100%
        /// dCurrent电流A
        /// nFreq频率Hz
        /// nQPulseWidth Q脉冲宽度 us
        /// nStartTC 开光延时 us
        /// nLaserOffTC 关光延时 us
        /// nEndTC  结束延时 us
        /// nPolyTC 多边形拐角延时us
        /// dJumpSpeed 跳转速度 mm/s或者inch/mm,取决于markdll.dll的当前单位 
        /// nJumpPosTC 跳转位置延时 us
        /// nJumpDistTC 跳转距离延时 us
        /// nSpiWave SPI波形选择
        /// bWobbleMode 抖动模式
        /// bWobbleDiameter 抖动直径
        /// bWobbleDist 抖动间距
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetPenParam2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetPenParam2(int nPenNo,
                                                        int nMarkLoop,
                                                        double dMarkSpeed,
                                                        double dPowerRatio,
                                                        double dCurrent,
                                                        int nFreq,
                                                        double dQPulseWidth,
                                                        int nStartTC,
                                                        int nLaserOffTC,
                                                        int nEndTC,
                                                        int nPolyTC,
                                                        double dJumpSpeed,
                                                        int nJumpPosTC,
                                                        int nJumpDistTC,
                                                        double dPointTime,
                                                        int nSpiWave,
                                                        bool bWobbleMode,
                                                        double bWobbleDiameter,
                                                        double bWobbleDist);


        public static int ColorToCOLORREF(Color color)
        {
            return ((color.R | (color.G << 8)) | (color.B << 0x10));
        }

        public static Color COLORREFToColor(int colorRef)
        {
            byte[] _IntByte = BitConverter.GetBytes(colorRef);
            return Color.FromArgb(_IntByte[0], _IntByte[1], _IntByte[2]);
        }

        [DllImport("MarkEzd", EntryPoint = "lmc1_SetPenParam4", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetPenParam4(int nPenNo,//笔号
                                                            string pStrName,    // 名称
                                                            int clr,//颜色
                                                            bool bDisableMark,//使能加工
                                                            bool bUseDefParam,//使用默认参数
                                                            int nMarkLoop,//加工次数
                                                            double dMarkSpeed,//加工速度
                                                            double dPowerRatio,//功率 %
                                                            double dCurrent,//电流,A
                                                            int nFreq,//频率 HZ
                                                            double dQPulseWidth,//脉宽，yag us    ylpm ns
                                                            int nStartTC,//开光延时
                                                            int nLaserOffTC,//关光延时
                                                            int nEndTC,//结束延时
                                                            int nPolyTC,//拐角延时
                                                            double dJumpSpeed,//跳转速度
                                                            int nMinJumpDelayTCUs,//最小跳转延时
                                                            int nMaxJumpDelayTCUs,//最大跳转延时
                                                            double dJumpLengthLimit,//跳转距离阈值
                                                            double dPointTimeMs,//打点时间
                                                            bool nSpiSpiContinueMode,//SPI连续模式
                                                            int nSpiWave,//SPI波形编号
                                                            int nYagMarkMode,//YAG优化模式
                                                            bool bPulsePointMode,//脉冲打点模式
                                                            int nPulseNum,//脉冲打点脉冲数量
                                                            bool bEnableACCMode,//启用加减速优化
                                                            double dEndComp,//加速
                                                            double dAccDist,//减速
                                                            double dBreakAngle,//中断角度
                                                            bool bWobbleMode,//抖动模式
                                                            double bWobbleDiameter,//抖动直径
                                                            double bWobbleDist);//抖动线间距


        /// <summary>
        ///设置笔号是否标刻
        /// </summary>
        /// <param name="nPenNo"></param>
        /// <param name="bDisableMark"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetPenDisableState", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetPenDisableState(int nPenNo, bool bDisableMark);


        /// <summary>
        /// 获取笔号是否标刻
        /// </summary>
        /// <param name="nPenNo"></param>
        /// <param name="bDisableMark"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetPenDisableState", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPenDisableState(int nPenNo, ref bool bDisableMark);

        ///<summary>
        ///获取指定名称对象笔号
        ///<summary>
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetPenNumberFromName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPenNumberFromName(string strEntName);

        /// <summary>
        /// 获取对象笔号
        /// </summary>
        /// <param name="strEntName"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetPenNumberFromEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPenNumberFromEnt(string strEntName);

        ///<summary>
        ///对象应用笔号设置（针对矢量图文件）
        ///<summary>
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetEntAllChildPen", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void SetEntAllChildPen(string strEntName, int nPenNo);

        #endregion

        #region 填充
        public const int HATCHATTRIB_ALLCALC = 0x01;//全部对象整体计算
        public const int HATCHATTRIB_EDGE = 0x02;//绕边走一次
        public const int HATCHATTRIB_MINUP = 0x04;//最少起笔
        public const int HATCHATTRIB_BIDIR = 0x08;//双向填充
        public const int HATCHATTRIB_LOOP = 0x10;//环行填充
        public const int HATCHATTRIB_OUT = 0x20;//环行由内向外
        public const int HATCHATTRIB_AUTOROT = 0x40;//自动角度旋转
        public const int HATCHATTRIB_AVERAGELINE = 0x80;//自动分布填充线
        public const int HATCHATTRIB_CROSELINE = 0x400;//交叉填充

        /// <summary>
        /// 设置当前的填充参数,如果向数据库添加对象时使能填充,系统会用此函数设置的参数来填充
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetHatchParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetHatchParam(bool bEnableContour,
                          int bEnableHatch1,
                          int nPenNo1,
                          int nHatchAttrib1,
                          double dHatchEdgeDist1,
                          double dHatchLineDist1,
                          double dHatchStartOffset1,
                          double dHatchEndOffset1,
                          double dHatchAngle1,
                          int bEnableHatch2,
                          int nPenNo2,
                          int nHatchAttrib2,
                          double dHatchEdgeDist2,
                          double dHatchLineDist2,
                          double dHatchStartOffset2,
                          double dHatchEndOffset2,
                          double dHatchAngle2);



        /// <summary>
        /// 设置当前的填充参数2,如果向数据库添加对象时使能填充,系统会用此函数设置的参数来填充  20120911add  2.7.2
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetHatchParam2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetHatchParam2(bool bEnableContour,//使能轮廓本身
                                                          int nParamIndex,//填充参数序号值为1,2,3
                                                          int bEnableHatch,//使能填充
                                                          int nPenNo,//填充参数笔号
                                                          int nHatchType,//填充类型 0单向 1双向 2回形 3弓形 4弓形不反向
                                                          bool bHatchAllCalc,//是否全部对象作为整体一起计算
                                                          bool bHatchEdge,//绕边一次
                                                          bool bHatchAverageLine,//自动平均分布线
                                                          double dHatchAngle,//填充线角度 
                                                          double dHatchLineDist,//填充线间距
                                                          double dHatchEdgeDist,//填充线边距    
                                                          double dHatchStartOffset,//填充线起始偏移距离
                                                          double dHatchEndOffset,//填充线结束偏移距离
                                                          double dHatchLineReduction,//直线缩进
                                                          double dHatchLoopDist,//环间距
                                                          int nEdgeLoop,//环数
                                                          bool nHatchLoopRev,//环形反转
                                                          bool bHatchAutoRotate,//是否自动旋转角度
                                                          double dHatchRotateAngle//自动旋转角度   
                                                       );

        /// <summary>
        /// 设置当前的填充参数3,如果向数据库添加对象时使能填充,系统会用此函数设置的参数来填充  20170330add  2.14.9
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetHatchParam3", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetHatchParam3(bool bEnableContour,//使能轮廓本身
                                                          int nParamIndex,//填充参数序号值为1,2,3
                                                          int bEnableHatch,//使能填充
                                                          int nPenNo,//填充参数笔号
                                                          int nHatchType,//填充类型 0单向 1双向 2回形 3弓形 4弓形不反向
                                                          bool bHatchAllCalc,//是否全部对象作为整体一起计算
                                                          bool bHatchEdge,//绕边一次
                                                          bool bHatchAverageLine,//自动平均分布线
                                                          double dHatchAngle,//填充线角度 
                                                          double dHatchLineDist,//填充线间距
                                                          double dHatchEdgeDist,//填充线边距    
                                                          double dHatchStartOffset,//填充线起始偏移距离
                                                          double dHatchEndOffset,//填充线结束偏移距离
                                                          double dHatchLineReduction,//直线缩进
                                                          double dHatchLoopDist,//环间距
                                                          int nEdgeLoop,//环数
                                                          bool nHatchLoopRev,//环形反转
                                                          bool bHatchAutoRotate,//是否自动旋转角度
                                                          double dHatchRotateAngle,//自动旋转角度  
                                                          bool bHatchCross
                                                       );

        [DllImport("MarkEzd", EntryPoint = "lmc1_GetHatchParam3", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetHatchParam3(ref bool bEnableContour,
                                                         int nParamIndex,
                                                         ref int bEnableHatch,
                                                         ref int nPenNo,
                                                         ref int nHatchType,
                                                         ref bool bHatchAllCalc,
                                                         ref bool bHatchEdge,
                                                         ref bool bHatchAverageLine,
                                                         ref double dHatchAngle,
                                                         ref double dHatchLineDist,
                                                         ref double dHatchEdgeDist,
                                                         ref double dHatchStartOffset,
                                                         ref double dHatchEndOffset,
                                                         ref double dHatchLineReduction,//直线缩进
                                                         ref double dHatchLoopDist,//环间距
                                                         ref int nEdgeLoop,//环数
                                                         ref bool nHatchLoopRev,//环形反转
                                                         ref bool bHatchAutoRotate,//是否自动旋转角度
                                                         ref double dHatchRotateAngle,
                                                         ref bool nHatchCross);



        [DllImport("MarkEzd", EntryPoint = "lmc1_SetHatchEntParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int
            SetHatchEntParam(string HatchName,
                                                        bool bEnableContour,
                                                        int nParamIndex,
                                                        int bEnableHatch,
                                                        int nPenNo,
                                                        int nHatchType,
                                                        bool bHatchAllCalc,
                                                        bool bHatchEdge,
                                                        bool bHatchAverageLine,
                                                        double dHatchAngle,
                                                        double dHatchLineDist,
                                                        double dHatchEdgeDist,
                                                        double dHatchStartOffset,
                                                        double dHatchEndOffset,
                                                        double dHatchLineReduction,//直线缩进
                                                        double dHatchLoopDist,//环间距
                                                        int nEdgeLoop,//环数
                                                        bool nHatchLoopRev,//环形反转
                                                        bool bHatchAutoRotate,//是否自动旋转角度
                                                        double dHatchRotateAngle);

        [DllImport("MarkEzd", EntryPoint = "lmc1_GetHatchEntParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetHatchEntParam(string HatchName,
                         ref bool bEnableContour,
                         int nParamIndex,
                         ref int bEnableHatch,
                         ref int nPenNo,
                         ref int nHatchType,
                         ref bool bHatchAllCalc,
                         ref bool bHatchEdge,
                         ref bool bHatchAverageLine,
                         ref double dHatchAngle,
                         ref double dHatchLineDist,
                         ref double dHatchEdgeDist,
                         ref double dHatchStartOffset,
                         ref double dHatchEndOffset,
                         ref double dHatchLineReduction,//直线缩进
                         ref double dHatchLoopDist,//环间距
                         ref int nEdgeLoop,//环数
                         ref bool nHatchLoopRev,//环形反转
                         ref bool bHatchAutoRotate,//是否自动旋转角度
                         ref double dHatchRotateAngle);

        [DllImport("MarkEzd", EntryPoint = "lmc1_SetHatchEntParam2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int
            SetHatchEntParam2(string HatchName,
                            bool bEnableContour,
                            int nParamIndex,
                            int bEnableHatch,
                            bool bContourFirst,
                            int nPenNo,
                            int nHatchType,
                            bool bHatchAllCalc,
                            bool bHatchEdge,
                            bool bHatchAverageLine,
                            double dHatchAngle,
                            double dHatchLineDist,
                            double dHatchEdgeDist,
                            double dHatchStartOffset,
                            double dHatchEndOffset,
                            double dHatchLineReduction,//直线缩进
                            double dHatchLoopDist,//环间距
                            int nEdgeLoop,//环数
                            bool nHatchLoopRev,//环形反转
                            bool bHatchAutoRotate,//是否自动旋转角度
                            double dHatchRotateAngle,
                                bool bHatchCrossMode,
                                int dCycCount);

        [DllImport("MarkEzd", EntryPoint = "lmc1_GetHatchEntParam2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetHatchEntParam2(string HatchName,
                         ref bool bEnableContour,
                         int nParamIndex,
                         ref int bEnableHatch,
                         ref bool bContourFirst,
                         ref int nPenNo,
                         ref int nHatchType,
                         ref bool bHatchAllCalc,
                         ref bool bHatchEdge,
                         ref bool bHatchAverageLine,
                         ref double dHatchAngle,
                         ref double dHatchLineDist,
                         ref double dHatchEdgeDist,
                         ref double dHatchStartOffset,
                         ref double dHatchEndOffset,
                         ref double dHatchLineReduction,//直线缩进
                         ref double dHatchLoopDist,//环间距
                         ref int nEdgeLoop,//环数
                         ref bool nHatchLoopRev,//环形反转
                         ref bool bHatchAutoRotate,//是否自动旋转角度
                         ref double dHatchRotateAngle,
                         ref bool bHatchCrossMode,
                         ref int dCycCount);


        /// <summary>
        /// 对数据库中指定名称的对象填充
        /// strEntName被填充对象名      
        /// strHatchEntName填充后对象名
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_HatchEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int HatchEnt(string strEntName, string strHatchEntName);

        /// <summary>
        /// 对数据库中指定名称的对象删除填充
        /// strHatchEntName填充对象名
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_UnHatchEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int UnHatchEnt(string strHatchEntName);

        #endregion

        #region 添加删除对象

        /// <summary>
        /// 清除数据库里所有对象
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_ClearEntLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ClearLibAllEntity();

        ///<summary>
        /// 删除指定名称对象
        ///<summary>
        [DllImport("MarkEzd", EntryPoint = "lmc1_DeleteEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int DeleteEnt(string strEntName);

        /// <summary>
        /// 向数据库里添加文本
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_AddTextToLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddTextToLib(string text, string EntName, double dPosX, double dPosY, double dPosZ, int nAlign, double dTextRotateAngle, int nPenNo, int bHatchText);

        [DllImport("MarkEzd", EntryPoint = "lmc1_AddCircleTextToLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddCircleTextToLib(string pStr,
                                                    string pEntName,
                                                    double dCenX,
                                                    double dCenY,
                                                    double dCenZ,
                                                    int nPenNo,
                                                    int bHatchText,
                                                    double dCirDiameter,
                                                    double dCirBaseAngle,
                                                    bool bCirEnableAngleLimit,
                                                    double dCirAngleLimit,
                                                    int nCirTextFlag);



        /// <summary>
        /// 向数据库添加一条曲线对象
        /// 注意PtBuf必须为2维数组,且第一维为2,如 double[5,2],double[n,2],
        /// ptNum为PtBuf数组的第2维,如PtBuf为double[5,2]数组,则ptNum=5
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_AddCurveToLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddCurveToLib([MarshalAs(UnmanagedType.LPArray)] double[,] PtBuf, int ptNum, string strEntName, int nPenNo, int bHatch);

        /// <summary>
        /// 向数据库添加一组点对象
        /// 注意PtBuf必须为2维数组,且第一维为2,如 double[5,2],double[n,2],
        /// ptNum为PtBuf数组的第2维,如PtBuf为double[5,2]数组,则ptNum=5
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_AddPointToLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddPointToLib([MarshalAs(UnmanagedType.LPArray)] double[,] PtBuf, int ptNum, string strEntName, int nPenNo);

        /// <summary>
        /// 添加延时器到文件中
        /// </summary>
        /// <param name="dDelayMs">延时器持续时间</param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_AddDelayToLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddDelayToLib(double dDelayMs);

        /// <summary>
        /// 添加输出口到文件中
        /// </summary>
        /// <param name="nOutPutBit">输出口管脚</param>
        /// <param name="bHigh">是否高有效</param>
        /// <param name="bPulse">是否脉冲模式</param>
        /// <param name="dPulseTimeMs">脉冲持续世间</param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_AddWritePortToLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddWritePortToLib(int nOutPutBit, bool bHigh, bool bPulse, double dPulseTimeMs);

        /// <summary>
        /// 载入指定数据文件
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_AddFileToLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddFileToLib(string strFileName, string strEntName, double dPosX, double dPosY, double dPosZ, int nAlign, double dRatio, int nPenNo, int bHatchFile);

        #region 条码

        public enum BARCODETYPE
        {
            BARCODETYPE_39 = 0,
            BARCODETYPE_93 = 1,
            BARCODETYPE_128A = 2,
            BARCODETYPE_128B = 3,
            BARCODETYPE_128C = 4,
            BARCODETYPE_128OPT = 5,
            BARCODETYPE_EAN128A = 6,
            BARCODETYPE_EAN128B = 7,
            BARCODETYPE_EAN128C = 8,
            BARCODETYPE_EAN13 = 9,
            BARCODETYPE_EAN8 = 10,
            BARCODETYPE_UPCA = 11,
            BARCODETYPE_UPCE = 12,
            BARCODETYPE_25 = 13,
            BARCODETYPE_INTER25 = 14,
            BARCODETYPE_CODABAR = 15,
            BARCODETYPE_PDF417 = 16,
            BARCODETYPE_DATAMTX = 17,
            BARCODETYPE_USERDEF = 18,
            BARCODETYPE_QRCODE = 19,
            BARCODETYPE_MICROQRCODE = 20

        };

        public const ushort BARCODEATTRIB_CHECKNUM = 0x0004;//自验码
        public const ushort BARCODEATTRIB_REVERSE = 0x0008;//反转
        public const ushort BARCODEATTRIB_SHORTMODE = 0x0040;//二维码缩短模式
        public const ushort BARCODEATTRIB_DATAMTX_DOTMODE = 0x0080;//二维码为点模式
        public const ushort BARCODEATTRIB_DATAMTX_CIRCLEMODE = 0x0100;//二维码为圆模式
        public const ushort BARCODEATTRIB_DATAMTX_ENABLETILDE = 0x0200;//DataMatrix使能~
        public const ushort BARCODEATTRIB_RECTMODE = 0x0400;//二维码为矩形模式
        public const ushort BARCODEATTRIB_SHOWCHECKNUM = 0x0800;//显示校验码文字
        public const ushort BARCODEATTRIB_HUMANREAD = 0x1000;//显示人识别字符
        public const ushort BARCODEATTRIB_NOHATCHTEXT = 0x2000;//不填充字符
        public const ushort BARCODEATTRIB_BWREVERSE = 0x4000;//黑白反转
        public const ushort BARCODEATTRIB_2DBIDIR = 0x8000;//2维码双向排列

        public enum DATAMTX_SIZEMODE
        {
            DATAMTX_SIZEMODE_SMALLEST = 0,
            DATAMTX_SIZEMODE_10X10 = 1,
            DATAMTX_SIZEMODE_12X12 = 2,
            DATAMTX_SIZEMODE_14X14 = 3,
            DATAMTX_SIZEMODE_16X16 = 4,
            DATAMTX_SIZEMODE_18X18 = 5,
            DATAMTX_SIZEMODE_20X20 = 6,
            DATAMTX_SIZEMODE_22X22 = 7,
            DATAMTX_SIZEMODE_24X24 = 8,
            DATAMTX_SIZEMODE_26X26 = 9,
            DATAMTX_SIZEMODE_32X32 = 10,
            DATAMTX_SIZEMODE_36X36 = 11,
            DATAMTX_SIZEMODE_40X40 = 12,
            DATAMTX_SIZEMODE_44X44 = 13,
            DATAMTX_SIZEMODE_48X48 = 14,
            DATAMTX_SIZEMODE_52X52 = 15,
            DATAMTX_SIZEMODE_64X64 = 16,
            DATAMTX_SIZEMODE_72X72 = 17,
            DATAMTX_SIZEMODE_80X80 = 18,
            DATAMTX_SIZEMODE_88X88 = 19,
            DATAMTX_SIZEMODE_96X96 = 20,
            DATAMTX_SIZEMODE_104X104 = 21,
            DATAMTX_SIZEMODE_120X120 = 22,
            DATAMTX_SIZEMODE_132X132 = 23,
            DATAMTX_SIZEMODE_144X144 = 24,
            DATAMTX_SIZEMODE_8X18 = 25,
            DATAMTX_SIZEMODE_8X32 = 26,
            DATAMTX_SIZEMODE_12X26 = 27,
            DATAMTX_SIZEMODE_12X36 = 28,
            DATAMTX_SIZEMODE_16X36 = 29,
            DATAMTX_SIZEMODE_16X48 = 30,
        }

        /// <summary>
        /// 向数据库里添加条码文本
        /// 注意 double[] dBarWidthScale 和dSpaceWidthScale大小必须为4
        /// </summary> 

        [DllImport("MarkEzd", EntryPoint = "lmc1_AddBarCodeToLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddBarCodeToLib(string strText,
            string strEntName,
            double dPosX,
            double dPosY,
            double dPosZ,
            int nAlign,
            int nPenNo,
            int bHatchText,
            BARCODETYPE nBarcodeType,
            ushort wBarCodeAttrib,
            double dHeight,
            double dNarrowWidth,
            [MarshalAs(UnmanagedType.LPArray)] double[] dBarWidthScale,
            [MarshalAs(UnmanagedType.LPArray)] double[] dSpaceWidthScale,
              double dMidCharSpaceScale,
            double dQuietLeftScale,
            double dQuietMidScale,
            double dQuietRightScale,
            double dQuietTopScale,
            double dQuietBottomScale,
            int nRow,
            int nCol,
            int nCheckLevel,
           DATAMTX_SIZEMODE nSizeMode,
            double dTextHeight,
            double dTextWidth,
            double dTextOffsetX,
            double dTextOffsetY,
            double dTextSpace,
            double dDiameter,
            string TextFontName);


        [DllImport("MarkEzd", EntryPoint = "lmc1_GetBarcodeParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetBarcodeParam(string pEntName,
                                                    ref ushort wBarCodeAttrib,
                                                    ref int nSizeMode,
                                                    ref int nCheckLevel,
                                                    ref int nLangPage,
                                                    ref double dDiameter,
                                                    ref int nPointTimesN,
                                                    ref double dBiDirOffset);

        [DllImport("MarkEzd", EntryPoint = "lmc1_SetBarcodeParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetBarcodeParam(string pEntName,
                                                    ushort wBarCodeAttrib,
                                                    int nSizeMode,
                                                    int nCheckLevel,
                                                    int nLangPage,
                                                    double dDiameter,
                                                    int nPointTimesN,
                                                    double dBiDirOffset);


        #endregion

        #endregion

        #region 扩展轴

        /// <summary>
        /// 复位，使能扩展轴
        /// ***使用扩展轴之前必须使用先调用此函数来初始化扩展轴*******
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_Reset", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ResetAxis(bool bEnAxis1, bool bEnAxis2);

        /// <summary>
        /// 扩展轴移动到目标位置
        /// axis=0或1
        /// GoalPos目标位置,单位mm或inch
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_AxisMoveTo", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AxisMoveTo(int axis, double GoalPos);

        /// <summary>
        /// 扩展轴回原点(校正原点)
        /// axis=0或1
        /// GoalPos目标位置
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_AxisCorrectOrigin", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AxisGoHome(int axis);

        /// <summary>
        /// 得到扩展轴的当前坐标
        /// axis=0或1
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetAxisCoor", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern double GetAxisCoor(int axis);

        /// <summary>
        /// 扩展轴移动到脉冲目标位置
        /// axis=0或1
        /// nGoalPos目标位置,单位:脉冲
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_AxisMoveToPulse", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AxisMoveToPulse(int axis, int nGoalPos);

        /// <summary>
        /// 得到扩展轴的当前脉冲坐标
        /// axis=0或1
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetAxisCoorPulse", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetAxisCoorPulse(int axis);

        #endregion

        #region 硬件锁存

        [DllImport("MarkEzd", EntryPoint = "lmc1_EnableLockInputPort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableLockInputPort(bool bLowToHigh);

        [DllImport("MarkEzd", EntryPoint = "lmc1_ClearLockInputPort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ClearLockInputPort();

        [DllImport("MarkEzd", EntryPoint = "lmc1_ReadLockPort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadLockPort(ref ushort data);
        #endregion

    }
}
