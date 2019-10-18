using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Core.Web.AOP.Attribute;

namespace Core.Web.AOP
{
    /// <summary>
    /// 插件工厂
    /// </summary>
    internal static class PluginFactory
    {
        static PluginFactory()
        {
            InitFactory();
        }

        public static bool IsCustomize { get; private set; } = false;

        /// <summary>
        /// 初始化工厂
        /// </summary>
        static void InitFactory()
        {
            lock (PluginMenmberTable.SyncRoot)
            {
                PluginMenmberTable.Clear();
                var st = new Stopwatch();
                st.Start();

                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(q => q.GetCustomAttribute<PluginAssemblyAttribute>() != null))
                {
                    foreach (var plugType in assembly.GetExportedTypes().Where(q => q.IsClass &&
                                                                                  !q.IsAbstract
                                                                                  && q.GetCustomAttribute<PluginClassAttribute>() != null))
                    {
                        var plugAttr = plugType.GetCustomAttribute<PluginClassAttribute>();

                        var member = new PluginClassMember()
                        {
                            FromType = plugAttr.FromType,
                            TargetType = plugType,
                            PluginMethod = new Dictionary<string, MethodInfo>()
                        };

                        foreach (var method in plugType.GetMethods().Where(q=>q.IsPublic).Select(q=>new
                        {
                            Method = q,
                            PluginAttr = q.GetCustomAttribute<PluginMethodAttribute>()
                        }).Where(q=>q.PluginAttr != null))
                        {
                            //收集扩展方法
                            member.PluginMethod[method.PluginAttr.MethodName] = method.Method;
                        }

                        PluginMenmberTable.Add(member.FromType,member);
                    }
                }

                st.Stop();

                IsCustomize = PluginMenmberTable.Count > 0;
                LogEventProxy.FireLogRecord($"加载AOP配置耗时：{st.Elapsed.Milliseconds}");
            }
        }
        /// <summary>
        /// 不作任何事 只为初始化
        /// </summary>
        public static void Init()
        {

        }

        private static readonly Hashtable PluginMenmberTable = new Hashtable();
        

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PluginClassMember GetPluginMember(Type type)
        {
            return PluginMenmberTable[type] as PluginClassMember;
        }
    }

    /// <summary>
    /// 扩展方法成员
    /// </summary>
    internal class PluginClassMember
    {
        /// <summary>
        /// 来源类型
        /// </summary>
        internal Type FromType { get; set; }

        /// <summary>
        /// 目标类型
        /// </summary>
        internal Type TargetType { get; set; }

        /// <summary>
        /// 插件方法
        /// </summary>
        internal Dictionary<string, MethodInfo> PluginMethod { get; set; }
    }
}
