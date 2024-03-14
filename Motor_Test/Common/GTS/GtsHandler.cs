namespace Motor_Test.Common.GTS
{
    public static class GtsHandler
    {
        public static void CommandHandler(short nRts)
        {
            switch (nRts)
            {
                case 0:
                    break;
                case 1:
                    Log.Error("GooGol:  "+"指令执行错误");
                    break;
                case 2:
                    Log.Error("GooGol:  " + "license 不支持");
                    break;
                case 7:
                    Log.Error("GooGol:  " + "指令参数错误");
                    break;
                case 8:
                    Log.Error("GooGol:  " + "不支持该指令");
                    break;
                case -1:
                case -2:
                case -3:
                case -4:
                case -5:
                    Log.Error("GooGol:  " + "主机和运动控制器通讯失败");
                    break;
                case -7:                   
                    Log.Error("GooGol:  " + "运动控制器没有响应");
                    break;
                case -8:                   
                    Log.Error("GooGol:  " + "多线程资源忙");
                    break;
                default:                   
                    Log.Error("GooGol:  " + "未知错误");
                    break;

            }

        }
    }
}
