using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.Web.Core;

namespace Core.Web.AOP
{
    internal static class ServiceProvider
    {
        public static T CreateInstance<T>() where T:class,IService
        {
            ProxyGenerator generator = new ProxyGenerator();
            return generator.CreateClassProxy<T>(new PluginInterceptor());
        }

        public static object CreateInstance(Type type)
        {
            ProxyGenerator generator = new ProxyGenerator();
            return generator.CreateClassProxy(type,new PluginInterceptor());
        }
    }
}
