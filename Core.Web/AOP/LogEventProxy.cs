using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.AOP
{
    /// <summary>
    /// 日志代理事件
    /// </summary>
    public static class LogEventProxy
    {
        /// <summary>
        /// 日志代理事件
        /// </summary>
        public static event Action<string> LogProxy;

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log">日志内容</param>
        public static void FireLogRecord(string log)
        {
            System.Threading.Volatile.Read(ref LogProxy)?.Invoke(log);
        }
    }
}
