using MarrowVale.Business.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Services
{
    public class InputProcessingService : IInputProcessingService
    {
        private readonly ILogger _logger;
        public InputProcessingService(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<InputProcessingService>();
        }
    }
}
