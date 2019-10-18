using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;

namespace Core.Web.Core
{
    /// <summary>
    /// 指令
    /// </summary>
    public class CoreActionSelector:IActionSelector
    {
        public IReadOnlyList<ActionDescriptor> SelectCandidates(RouteContext context)
        {
            throw new NotImplementedException();
        }

        public ActionDescriptor SelectBestCandidate(RouteContext context, IReadOnlyList<ActionDescriptor> candidates)
        {
            throw new NotImplementedException();
        }
    }
}
