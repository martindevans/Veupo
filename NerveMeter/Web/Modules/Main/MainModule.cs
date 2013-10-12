using Nancy;
using Nancy.Routing;

namespace NerveMeter.Web.Modules.Main
{
    public class MainModule
        : NancyModule
    {
        private readonly IRouteCacheProvider _routeCacheProvider;

        public MainModule(IRouteCacheProvider routeCacheProvider)
        {
            _routeCacheProvider = routeCacheProvider;
            Get["/"] = ListRoutes;
        }

        private dynamic ListRoutes(dynamic parameters)
        {
            return View["routes.cshtml", _routeCacheProvider.GetCache()];
        }
    }
}
