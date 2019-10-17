using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.CustomControl
{
    public static class ServiceExtend
    {
        public static bool IsAppService(Type type)
        {
            return typeof(IService).IsAssignableFrom(type);
        }
    }
}
