using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Web.AOP;
using Core.Web.CustomControl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;

namespace Core.Web.Core
{
    public class CoreControllerActivator:DefaultControllerActivator,IControllerActivator
    {
        public CoreControllerActivator(ITypeActivatorCache typeActivatorCache) : base(typeActivatorCache)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object Create(ControllerContext context)
        {
            var appContext = new AppContext();
            if (AppContextHelper.Context == null)
            {
                AppContextHelper.Context = new Stack<IAppContext>();
            }
            AppContextHelper.Context.Push(appContext);

            if (context.ActionDescriptor.ControllerTypeInfo.DeclaringType.IsAppService())
            {
                //自定义工厂实现激活器
                return ServiceProvider.CreateInstance(context.ActionDescriptor.ControllerTypeInfo.DeclaringType);
            }
            else
            {
                return base.Create(context);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="controller"></param>
        public override void Release(ControllerContext context, object controller)
        {
            //释放上下文资源
            AppContextHelper.Context.Pop();
        }
    }
}
