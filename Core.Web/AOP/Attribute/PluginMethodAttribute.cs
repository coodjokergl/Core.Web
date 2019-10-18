using System;

namespace Core.Web.AOP.Attribute
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PluginMethodAttribute:System.Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string MethodName { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mathodName"></param>
        public PluginMethodAttribute(string mathodName)
        {
            MethodName = mathodName;
        }
    }
}
