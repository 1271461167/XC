using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp3.Common
{
    public interface IMarkController
    {
        #region 设备
        /// <summary>
        /// 初始化函数库
        /// </summary>
        /// <param name="PathName">strEzCadPathezcad2.exe所处的目录的全路径名称</param>
        /// <param name="bTestMode">是否是测试模式.0或者1,0表示其他模式,1为测试模式</param>
        /// <returns></returns>
        void Initialize(string PathName, bool bTestMode);
        /// <summary>
        /// 释放函数库
        /// </summary>
        /// <returns></returns>
        void Close();
        #endregion
        #region 加工
        /// <summary>
        /// 加工数据库所有对象
        /// </summary>
        /// <param name="Fly">是否飞行标刻</param>
        /// <returns></returns>
        int Mark(bool Fly);
        /// <summary>
        /// 加工指定对象
        /// </summary>
        /// <param name="EntName"> 对象名称</param>
        /// <returns></returns>
        int MarkEntity(string EntName);
        /// <summary>
        /// 红光预览所有数据
        /// </summary>
        /// <returns></returns>
        int RedMark();
        /// <summary>
        /// 红光预览所有数据轮廓
        /// </summary>
        /// <returns></returns>
        int RedMarkContour();
        /// <summary>
        /// 红光预览指定对象
        /// </summary>
        /// <param name="EntName">对象名称</param>
        /// <param name="bContour">是否显示轮廓，0或者1,1显示轮廓,0显示范围</param>
        /// <returns></returns>
        int RedLightMarkByEnt(string EntName, bool bContour);
        #endregion
        #region 文件
        /// <summary>
        /// 载入ezd文件到当前数据库里面,并清除旧的数据库
        /// </summary>
        /// <param name="FileName">文件路径</param>
        /// <returns></returns>
        void LoadEzdFile(string FileName);
        /// <summary>
        /// 保存当前数据到指定文件
        /// </summary>
        /// <param name="strFileName">保存文件路径</param>
        /// <returns></returns>
        int SaveEntLibToFile(string strFileName);
        /// <summary>
        /// 得到当前数据库里面所有数据的预览图片
        /// </summary>
        /// <param name="bmpwidth">获取图像的宽</param>
        /// <param name="bmpheight">获取图像的高</param>
        /// <returns></returns>
        ImageSource GetCurPreviewImage(int bmpwidth, int bmpheight);
        /// <summary>
        /// 得到当前数据库里面指定对象的预览图片
        /// </summary>
        /// <param name="Entname">对象名称</param>
        /// <param name="bmpwidth">获取图像的宽</param>
        /// <param name="bmpheight">获取图像的高</param>
        /// <returns></returns>
        Image GetCurPreviewImageByName(string Entname, int bmpwidth, int bmpheight);
        #endregion
        #region 对象
        /// <summary>
        /// 得到指定对象的最大最小坐标
        /// </summary>
        /// <param name="strEntName">对象名称</param>
        /// <param name="dMinx">最小x坐标</param>
        /// <param name="dMiny">最小y坐标</param>
        /// <param name="dMaxx">最大x坐标</param>
        /// <param name="dMaxy">最大y坐标</param>
        /// <param name="dz">z坐标</param>
        /// <returns></returns>
        int GetEntSize(string strEntName, ref double dMinx, ref double dMiny, ref double dMaxx, ref double dMaxy, ref double dz);
        /// <summary>
        /// 移动数据库中指定名称的对象
        /// </summary>
        /// <param name="strEntName">对象名称</param>
        /// <param name="dMovex">x方向移动距离</param>
        /// <param name="dMovey">y方向移动距离</param>
        /// <returns></returns>
        int MoveEnt(string strEntName, double dMovex, double dMovey);
        /// <summary>
        /// 对数据库中指定名称的对象进行比例变换
        /// 变换前所有构成对象的点到变换中心距离按变换比例变换，对应为变换后的图形坐标
        /// </summary>
        /// <param name="strEntName">对象名称</param>
        /// <param name="dCenx">dCenx,dCeny变换中心的坐标</param>
        /// <param name="dCeny">dCenx,dCeny变换中心的坐标</param>
        /// <param name="dScaleX">dScaleX x方向变换比例</param>
        /// <param name="dScaleY">dScaleY y方向变换比例</param>
        /// <returns></returns>
        int ScaleEnt(string strEntName, double dCenx, double dCeny, double dScaleX, double dScaleY);
        /// <summary>
        /// 对数据库中指定名称的对象进行镜像变换
        /// </summary>
        /// <param name="strEntName">对象名称</param>
        /// <param name="dCenx">dCenx,dCeny镜像中心的坐标</param>
        /// <param name="dCeny">dCenx,dCeny镜像中心的坐标</param>
        /// <param name="bMirrorX">bMirrorX= true X镜像</param>
        /// <param name="bMirrorY">bMirrorY= true Y镜像</param>
        /// <returns></returns>
        int MirrorEnt(string strEntName, double dCenx, double dCeny, bool bMirrorX, bool bMirrorY);
        /// <summary>
        /// 对数据库中指定名称的对象进行旋转变换
        /// </summary>
        /// <param name="strEntName">对象名称</param>
        /// <param name="dCenx">dCenx旋转中心的x坐标</param>
        /// <param name="dCeny">dCenx旋转中心的x坐标</param>
        /// <param name="dAngle">dAngle为旋转角度,单位为弧度值</param>
        /// <returns></returns>
        int RotateEnt(string strEntName, double dCenx, double dCeny, double dAngle);
        /// <summary>
        /// 复制指定名称对象，并命名
        /// </summary>
        /// <param name="strEntName">原对象名称</param>
        /// <param name="strNewEntName">新对象名称</param>
        /// <returns></returns>
        int CopyEnt(string strEntName, string strNewEntName);
        /// <summary>
        /// 得到对象总数
        /// </summary>
        /// <returns></returns>
        ushort GetEntityCount();
        /// <summary>
        ///  得到指定索引号的对象的名称。
        /// </summary>
        /// <param name="nEntityIndex">索引号</param>
        /// <param name="entname">名称</param>
        /// <returns></returns>
        int lmc1_GetEntityNameByIndex(int nEntityIndex, StringBuilder entname);
        /// <summary>
        /// 设定指定索引号的对象的名称
        /// </summary>
        /// <param name="nEntityIndex">索引号</param>
        /// <param name="entname">名称</param>
        /// <returns></returns>
        int SetEntityNameByIndex(int nEntityIndex, string entname);
        /// <summary>
        /// 重命名指定名称对象
        /// </summary>
        /// <param name="strEntName">原名称</param>
        /// <param name="strNewEntName">新名称</param>
        /// <returns></returns>
        int ChangeEntName(string strEntName, string strNewEntName);
        #endregion
        #region 端口
        /// <summary>
        /// 读当前输入端口
        /// </summary>
        /// <param name="data">
        /// data 为当前输入端口的值,
        /// Bit0是In0的状态,Bit0=1表示In0为高电平,Bit0=0表示In0为低电平
        /// Bit1是In1的状态,Bit1=1表示In1为高电平,Bit1=0表示In1为低电平
        /// ........
        /// Bit15是In15的状态,Bit15=1表示In15为高电平,Bit15=0表示In15为低电平
        /// </param>
        /// <returns></returns>
        void ReadPort(ref ushort data);
        /// <summary>
        /// 设置当前输出端口输出
        /// </summary>
        /// <param name="data">
        ///data 为当前输入端口的值,
        /// Bit0是In0的状态,Bit0=1表示In0为高电平,Bit0=0表示In0为低电平
        /// Bit1是In1的状态,Bit1=1表示In1为高电平,Bit1=0表示In1为低电平
        /// ........
        /// Bit15是In15的状态,Bit15=1表示In15为高电平,Bit15=0表示In15为低电平</param>
        /// <returns></returns>
        int WritePort(ushort data);
        /// <summary>
        /// 获取当前设备输出口状态值
        /// </summary>
        /// <param name="data">
        /// data 为当前输入端口的值,
        /// Bit0是In0的状态,Bit0=1表示In0为高电平,Bit0=0表示In0为低电平
        /// Bit1是In1的状态,Bit1=1表示In1为高电平,Bit1=0表示In1为低电平
        /// ........
        /// Bit15是In15的状态,Bit15=1表示In15为高电平,Bit15=0表示In15为低电平</param>
        /// </param>
        /// <returns></returns>
        void GetOutPort(ref ushort data);
        #endregion
        #region 文本
        /// <summary>
        /// 更改数据库中指定名称的文本对象的内容为指定文本
        /// </summary>
        /// <param name="EntName">对象名称</param>
        /// <param name="NewText">新文本</param>
        /// <returns></returns>
        int ChangeTextByName(string EntName, string NewText);
        /// <summary>
        /// 得到指定对象的文本
        /// </summary>
        /// <param name="strEntName">对象名称</param>
        /// <param name="Text">文本内容</param>
        /// <returns></returns>
        int GetTextByName(string strEntName, StringBuilder Text);
        #endregion
        #region 笔号
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPenNo">要设置的笔号0-255</param>
        /// <param name="nMarkLoop">标刻次数</param>
        /// <param name="dMarkSpeed">加工速度 mm/s或者inch/mm,取决于markdll.dll的当前单位</param>
        /// <param name="dPowerRatio">功率百分比 0-100%</param>
        /// <param name="dCurrent">电流A</param>
        /// <param name="nFreq">频率Hz</param>
        /// <param name="dQPulseWidth">Q脉冲宽度 us</param>
        /// <param name="nStartTC">开光延时 us</param>
        /// <param name="nLaserOffTC">关光延时 us</param>
        /// <param name="nEndTC">结束延时 us</param>
        /// <param name="nPolyTC">多边形拐角延时us</param>
        /// <param name="dJumpSpeed">跳转速度 mm/s或者inch/mm,取决于markdll.dll的当前单位</param>
        /// <param name="nJumpPosTC">跳转位置延时 us</param>
        /// <param name="nJumpDistTC">跳转距离延时 us</param>
        /// <param name="dEndComp">末点补偿距离 mm或者inch,取决于markdll.dll的当前单位</param>
        /// <param name="dAccDist">加速距离 mm或者inch</param>
        /// <param name="dPointTime">打点时间ms</param>
        /// <param name="bPulsePointMode">打点模式 true使能</param>
        /// <param name="nPulseNum">打点个数</param>
        /// <param name="dFlySpeed">流水线速度 mm/s或者inch/mm</param>
        /// <returns></returns>
        int GetPenParam(int nPenNo,
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
        /// 设置指定笔号的参数
        /// </summary>
        /// <param name="nPenNo">要设置的笔号0-255</param>
        /// <param name="nMarkLoop">标刻次数</param>
        /// <param name="dMarkSpeed">加工速度 mm/s或者inch/mm,取决于markdll.dll的当前单位</param>
        /// <param name="dPowerRatio">功率百分比 0-100%</param>
        /// <param name="dCurrent">电流A</param>
        /// <param name="nFreq">频率Hz</param>
        /// <param name="dQPulseWidth">Q脉冲宽度 us</param>
        /// <param name="nStartTC">开光延时 us</param>
        /// <param name="nLaserOffTC">关光延时 us</param>
        /// <param name="nEndTC">结束延时 us</param>
        /// <param name="nPolyTC">多边形拐角延时us</param>
        /// <param name="dJumpSpeed">跳转速度 mm/s或者inch/mm,取决于markdll.dll的当前单位</param>
        /// <param name="nJumpPosTC">跳转位置延时 us</param>
        /// <param name="nJumpDistTC">跳转距离延时 us</param>
        /// <param name="dEndComp">末点补偿距离 mm或者inch,取决于markdll.dll的当前单位</param>
        /// <param name="dAccDist">加速距离 mm或者inch</param>
        /// <param name="dPointTime">打点时间ms</param>
        /// <param name="bPulsePointMode">打点模式 true使能</param>
        /// <param name="nPulseNum">打点个数</param>
        /// <param name="dFlySpeed">流水线速度 mm/s或者inch/mm</param>
        /// <returns></returns>
        int SetPenParam(int nPenNo,
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
        /// <summary>
        /// 设置笔号是否标刻
        /// </summary>
        /// <param name="nPenNo">笔号</param>
        /// <param name="bDisableMark">是否标刻</param>
        /// <returns></returns>
        int SetPenDisableState(int nPenNo, bool bDisableMark);
        /// <summary>
        /// 获取笔号是否标刻
        /// </summary>
        /// <param name="nPenNo">笔号</param>
        /// <param name="bDisableMark">是否标刻</param>
        /// <returns></returns>
        int GetPenDisableState(int nPenNo, ref bool bDisableMark);
        /// <summary>
        /// 从笔名称来获取笔号
        /// </summary>
        /// <param name="strEntName">笔名</param>
        /// <returns></returns>
        int GetPenNumberFromName(string strEntName);
        /// <summary>
        /// 获取指定对象的笔号
        /// </summary>
        /// <param name="strEntName">对象名称</param>
        /// <returns></returns>
        int GetPenNumberFromEnt(string strEntName);
        /// <summary>
        /// 设置指定对象所有子对象的笔号.
        /// </summary>
        /// <param name="strEntName">对象名称</param>
        /// <param name="nPenNo">要设置的笔号</param>
        void SetEntAllChildPen(string strEntName, int nPenNo);


        #endregion
        #region 添加删除对象
        /// <summary>
        /// 清除数据库里所有对象
        /// </summary>
        /// <returns></returns>
        int ClearLibAllEntity();
        /// <summary>
        /// 删除指定名称对象
        /// </summary>
        /// <param name="strEntName">对象名称</param>
        /// <returns></returns>
        int DeleteEnt(string strEntName);

        #endregion
    }
}
