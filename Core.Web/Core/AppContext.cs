using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Core
{
    /// <summary>
    /// 上下文
    /// </summary>
    public interface IAppContext
    {
        string UserName { get; }
    }

    public class AppContext:IDisposable,IAppContext
    {

        public void Dispose()
        {
            //资源释放
        }

        public string UserName { get; set; }
    }

    public static class AppContextHelper 
    {
        /// <summary>
        /// 上下文
        /// </summary>
        [ThreadStatic]
        public static Stack<IAppContext> Context ;
    }
    
}
