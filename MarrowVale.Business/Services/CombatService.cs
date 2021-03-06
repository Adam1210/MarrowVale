﻿using MarrowVale.Business.Contracts;
using Microsoft.Extensions.Logging;

namespace MarrowVale.Business.Services
{
    public class CombatService : ICombatService
    {
        private readonly ILogger<CombatService> _logger;
        public string Name { get; set; }
        public CombatService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CombatService>();
        }

        public void Attack()
        {
            _logger.LogInformation("The Combat Services Logging Works");
        }
    }
}
