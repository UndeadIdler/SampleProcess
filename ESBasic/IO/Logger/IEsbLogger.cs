using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ESBasic.Logger
{
    /// <summary>
    /// ILogger 用于记录日志信息。通常可以通过ESFramework.Common.AdvancedFunction.SetProperty方法来简化组件的日志记录器
    ///			的装配。
    /// 注意，所有需要日志记录的组件请使用名为"EsbLogger"的属性注入。
    /// zhuweisky
    /// </summary>
    public interface IEsbLogger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorType"></param>
        /// <param name="msg"></param>
        /// <param name="location"></param>
        /// <param name="level"></param>
        void Log(string errorType, string msg, int location, ErrorLevel level);
        /// <summary>
        /// 
        /// </summary>
        bool Enabled { set; }
        /// <summary>
        /// 
        /// </summary>
        int LogLevel { set; } //只有等级大于等于该值的错误才会被记录 (0 - 5)
    }

    /// <summary>
    /// 
    /// </summary>
    [EnumDescription("异常/错误严重级别")]
    public enum ErrorLevel
    {
        [EnumDescription("致命的", 4)]
        Fatal,
        [EnumDescription("高", 3)]
        High,
        [EnumDescription("普通", 2)]
        Standard,
        [EnumDescription("低", 1)]
        Low,
        [EnumDescription("发送消息", -1)]
        SendMes,
        [EnumDescription("接收消息", -2)]
        RevMes
    }


    #region EmptyLogger
    /// <summary>
    /// 
    /// </summary>
    public class EmptyEsbLogger : IEsbLogger
    {
        public static event CbSimpleStrInt OnShowLog;

        #region ILogger 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorType"></param>
        /// <param name="msg"></param>
        /// <param name="location"></param>
        /// <param name="level"></param>
        public void Log(string errorType, string msg, int location, ErrorLevel level)
        {
            string flag = null;
            switch (level)
            {
                case ErrorLevel.Fatal:
                    flag = "Fatal";
                    break;
                case ErrorLevel.High:
                    flag = "High";
                    break;
                case ErrorLevel.Standard:
                    flag = "Standard";
                    break;
                case ErrorLevel.Low:
                    flag = "Low";
                    break;
                case ErrorLevel.SendMes:
                    flag = "SendMes";
                    break;
                case ErrorLevel.RevMes:
                    flag = "RevMes";
                    break;
                default:
                    flag = string.Empty;
                    return;
            }

            DateTime dt = DateTime.Now;

            string log = "Log\\" + flag + dt.ToString("yyyy-MM-dd") + ".log";

            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Log"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Log");
                }
                StreamWriter sw = new StreamWriter(log, true);
                try
                {
                    if (OnShowLog != null)
                    {
                        OnShowLog(DateTime.Now.ToString() + " [" + flag + "] " + errorType + "   " + location + "   [" + msg + "]", location);
                    }

                    sw.WriteLine("{0}\r\n{1}\r\n{2}", "[" + dt.ToString("HH:mm:ss fff") + "]" + "---" + DateTime.Now.Millisecond.ToString(), errorType + "--------" + location, msg);
                    sw.Flush();
                }
                catch
                {

                }
                finally
                {
                    sw.Close();
                }
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled
        {
            set
            {
                // TODO:  添加 EmptyEsbLogger.Enabled setter 实现
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LogLevel
        {
            set
            {
                // TODO:  添加 EmptyEsbLogger.LogLevel setter 实现
            }
        }

        #endregion
    }
    #endregion
}
