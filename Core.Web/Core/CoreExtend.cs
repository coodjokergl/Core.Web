using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Core.Web.Core
{
    public static class CoreExtend
    {
        public static IMvcBuilder AddMyMvc(this IMvcBuilder builder)
        {
            var feature = new ControllerFeature();
            builder.PartManager.PopulateFeature(feature);

            foreach (var controller in feature.Controllers.Select(c => c.AsType()))
            {
                builder.Services.TryAddTransient(controller, controller);
            }

            //注入自己的激活器
            builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, CoreControllerActivator>());           

            return builder;
        }
    }
}
