using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using FileProviders.Zip;

namespace SampleSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostContext, configBuilder) =>
                {
                    hostContext.HostingEnvironment.WebRootFileProvider = new ZipFileProvider(Path.Combine(hostContext.HostingEnvironment.WebRootPath, "static.zip"));
                })
                .Build();
    }
}
