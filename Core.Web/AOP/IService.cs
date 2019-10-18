using Core.Web.Core;

namespace Core.Web.AOP
{
    public interface IService
    {
        IAppContext Context { get; }
    }
}
