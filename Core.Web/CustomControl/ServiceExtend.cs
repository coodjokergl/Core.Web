using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Web.AOP;

namespace Core.Web.CustomControl
{
    public static class ServiceExtend
    {
        public static bool IsAppService(this Type type)
        {
            return typeof(IService).IsAssignableFrom(type);
        }
    }
}
