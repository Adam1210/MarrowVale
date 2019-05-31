using MarrowVale.Common.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Common.Providers
{
    public class GlobalItemsProvider : IGlobalItemsProvider
    {
        private readonly ILogger _logger;
        public int WindowWidth { get; }

        public GlobalItemsProvider(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<GlobalItemsProvider>();
            WindowWidth = 120;
        }
    }
}
