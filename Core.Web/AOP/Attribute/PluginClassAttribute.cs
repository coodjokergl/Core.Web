using System;

namespace Core.Web.AOP.Attribute
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,Inherited = true)]
    public class PluginClassAttribute:System.Attribute
    {
        /// <summary>
        /// 源类型
        /// </summary>
        public Type FromType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public PluginClassAttribute(Type type)
        {
            FromType = type;
        }
    }
}
