using System.Collections.Generic;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Ninject;
using Nancy.ViewEngines.Razor;
using Ninject;

namespace NerveMeter.Web
{
    public class Bootstrapper
        : NinjectNancyBootstrapper
    {
        private readonly IKernel _kernel;

        public Bootstrapper(IKernel kernel)
        {
            _kernel = kernel;
        }

        protected override IKernel GetApplicationContainer()
        {
            _kernel.Load<FactoryModule>();
            return _kernel;
        }

        protected override void ApplicationStartup(IKernel container, IPipelines pipelines)
        {
            Conventions.ViewLocationConventions.Add((viewName, model, context) => string.Concat("Web/Modules/", context.ModuleName, "/", viewName));
            Conventions.ViewLocationConventions.Add((viewName, model, context) => string.Concat("Web/Views/", context.ModuleName, "/", viewName));

            base.ApplicationStartup(container, pipelines);
        }
    }

    public class RazorConfiguration : IRazorConfiguration
    {
        public IEnumerable<string> GetAssemblyNames()
        {
            return new string[0];
        }

        public IEnumerable<string> GetDefaultNamespaces()
        {
            return new string[0];
        }

        public bool AutoIncludeModelNamespace
        {
            get { return true; }
        }
    }
}
