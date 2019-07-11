using MarrowVale.Common.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Common.Providers
{
    public class AppSettingsProvider : IAppSettingsProvider
    {
        private readonly ILogger _logger;
        private IConfigurationRoot Configuration => BuildConfiguration();

        //Modified paths to work on any OS
        public string ResourceDirectory => Path.Combine(Environment.CurrentDirectory, Configuration.GetSection("MySettings").GetSection("ResourceDirectory").Value);
        public string DataFilesLocation => Path.Combine(ResourceDirectory, Configuration.GetSection("MySettings").GetSection("DataFilesLocation").Value);
        public string SoundFilesLocation => Path.Combine(ResourceDirectory, Configuration.GetSection("MySettings").GetSection("SoundFilesLocation").Value);

        public AppSettingsProvider(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<AppSettingsProvider>();
        }

        private IConfigurationRoot BuildConfiguration()
        {
            var directory = Environment.CurrentDirectory;

            var builder = new ConfigurationBuilder()
            .SetBasePath(directory)
            .AddJsonFile("appsettings.json", true, true);          

            var configuration = builder.Build();
           
            return configuration;
        }
    }
}
