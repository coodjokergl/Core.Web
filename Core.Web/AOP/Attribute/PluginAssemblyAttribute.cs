using System;

namespace Core.Web.AOP.Attribute
{
    /// <summary>
    /// 是否包含插件程序集
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class PluginAssemblyAttribute :System.Attribute
    {
    }
}
