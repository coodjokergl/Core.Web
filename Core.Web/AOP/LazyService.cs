using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.AOP
{
    public sealed class LazyService<T> where T : class ,IService 
    {
        private T _instance;

        /// <summary>
        /// 
        /// </summary>
        public T Instance => GetInstance();

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        private T GetInstance()
        {
            if (_instance == null)
            {
                lock (this)
                {
                    if (_instance == null)
                    {
                        _instance = ServiceProvider.CreateInstance<T>();
                    }
                }
            }
            return _instance;
        } 
    }
}
