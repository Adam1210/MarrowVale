using MarrowVale.Business.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ILogger<CharacterService> _logger;

        public CharacterService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CharacterService>();
        }

        public void NewCharacter()
        {
            Console.WriteLine("New Character Here!!");
        }
    }
}
