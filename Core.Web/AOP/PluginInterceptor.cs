using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.Web.Core;
using AppContext = Core.Web.Core.AppContext;

namespace Core.Web.AOP
{
    /// <summary>
    /// 插件拦截器
    /// </summary>
    internal class PluginInterceptor:IInterceptor
    {
        public PluginInterceptor()
        {
            Context = AppContextHelper.Context.Peek();
        }

        private ClassMemberInvoker _invoker;

        /// <summary>
        /// 运行上下文
        /// </summary>
        public IAppContext Context { get;}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            var pluginInovker = GetInvoker(invocation.TargetType);
            try
            {
                //执行原对象中的方法  
                pluginInovker.Invoke(invocation); 
            }
            catch (Exception e)
            {
                LogEventProxy.FireLogRecord(e.Message);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        private ClassMemberInvoker GetInvoker(Type sourceType)
        {
            if (_invoker != null) return _invoker;

            if (PluginFactory.IsCustomize)
            {
                //替换插件方式
                var plugInstance = PluginFactory.GetPluginMember(sourceType);
                _invoker = new ClassMemberInvoker(plugInstance);
            }
            else
            {
                _invoker = new ClassMemberInvoker(null);
            }

            return _invoker;
        }
    }

    internal class ClassMemberInvoker 
    {
        private readonly PluginClassMember _classMember;
        private readonly object _pluginInstance;
        public ClassMemberInvoker(PluginClassMember member)
        {
            _classMember = member;
            _pluginInstance = _classMember == null ? null : Activator.CreateInstance(_classMember.TargetType);
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        internal void Invoke(IInvocation invocation)
        {
            var st = new Stopwatch();
            st.Start();

            if (_pluginInstance != null && _classMember != null && _classMember.PluginMethod != null &&
                _classMember.PluginMethod.TryGetValue(invocation.MethodInvocationTarget.Name,
                    out MethodInfo methodInfo))
            {
                LogEventProxy.FireLogRecord($@"执行二开方法：{methodInfo.DeclaringType?.Name}{methodInfo.Name}");
                //插件 执行
                invocation.ReturnValue = methodInfo.Invoke(_pluginInstance, invocation.Arguments);
                st.Stop();
                LogEventProxy.FireLogRecord($@"执行二开方法：{methodInfo.DeclaringType?.Name}{methodInfo.Name} 耗时：{st.Elapsed.Milliseconds}ms");
            }
            else
            {
                //产品 执行
                invocation.Proceed();
                st.Stop();

                LogEventProxy.FireLogRecord($@"执行功能：{invocation.TargetType.Name}{invocation.MethodInvocationTarget.Name}    耗时：{st.Elapsed.Milliseconds}ms");
            }
        }
    }
}
