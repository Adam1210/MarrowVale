using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarrowVale.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ILogger _logger;
        private readonly IAppSettingsProvider _appSettingsProvider;

        private JsonSerializerSettings Settings { get; set; }

        public GameRepository(ILoggerFactory logger, IAppSettingsProvider appSettingsProvider)
        {
            _logger = logger.CreateLogger<GameRepository>();
            _appSettingsProvider = appSettingsProvider;
        }


        public Game LoadGame(string fileName)
        {
            var filePath = generateFilePath(fileName);
            var gameFile = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Game>(gameFile, Settings);
        }

        public void SaveGame(Game game, string oldFileName, string newFileName)
        {
            deleteOldGame(oldFileName);
            saveGameFile(game, newFileName);
        }

        private void saveGameFile(Game game, string fileName)
        {
            var filePath = generateFilePath(fileName);

            var newJson = JsonConvert.SerializeObject(game, Formatting.Indented, Settings);

            File.WriteAllText(filePath, newJson);
        }

        private void deleteOldGame(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            var filePath = generateFilePath(fileName);
            if (File.Exists(filePath))
            {
                return;
            }

            File.Delete(filePath);
        }

        private string generateFilePath(string fileName)
        {
            return $"{Environment.CurrentDirectory}\\Game Tools\\DataFiles\\{fileName}.json";
        }
    }
}
