using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Core.Web.Core
{
    public class test : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
        }
    }
}
