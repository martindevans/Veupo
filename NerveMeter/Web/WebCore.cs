using System;
using Nancy.Hosting.Self;
using Ninject;
using NLog;

namespace NerveMeter.Web
{
    public static class WebCore
    {
        private static NancyHost _host;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Start(IKernel kernel, ushort port = 55768)
        {
            var uri = new Uri("http://localhost:" + port);
            var config = new HostConfiguration { RewriteLocalhost = false };

            var bootstrapper = new Bootstrapper(kernel);

            _host = new NancyHost(bootstrapper, config, uri);
            _host.Start();

            _logger.Info("Publishing debug information on {0}", uri);
        }

        public static void Update()
        {

        }

        public static void Shutdown()
        {
            _logger.Info("Shutting down web server");

            _host.Stop();
            _host.Dispose();
        }
    }
}
