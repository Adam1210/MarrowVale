using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MarrowVale.Data.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ILogger _logger;
        private readonly IAppSettingsProvider _appSettingsProvider;
        private string PlayerFile { get; set; }
        private string PlayerFilePath { get; set; }
        public PlayerRepository(ILoggerFactory logger, IAppSettingsProvider appSettingsProvider)
        {
            _logger = logger.CreateLogger<PlayerRepository>();
            _appSettingsProvider = appSettingsProvider;
            //PlayerFilePath = $"{_appSettingsProvider.DataFilesLocation}\\PlayerList.json";
            PlayerFilePath = $"{Environment.CurrentDirectory}\\Game Tools\\DataFiles\\PlayerList.json";
            //PlayerFile = File.ReadAllText(PlayerFilePath);
            PlayerFile = File.ReadAllText(PlayerFilePath);
        }

        public IList<Player> GetPlayers()
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };

            var playerList = JsonConvert.DeserializeObject<List<Player>>(PlayerFile, settings);

            if (playerList == null)
            {
                _logger.LogError("There are no contents in PlayerList.json");
            }

            return playerList;
        }

        public void AddPlayer(Player player)
        {
            try
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                };

                var playerList = JsonConvert.DeserializeObject<IList<Player>>(PlayerFile, settings);

                playerList.Add(player);

                var newJson = JsonConvert.SerializeObject(playerList, Formatting.Indented, settings);

                File.WriteAllText(PlayerFilePath, newJson);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There was a problem with adding new player to the repository. See Exception Details{Environment.NewLine}" +
                    $"{ex.Message}");
            }
        }

        public Player GetPlayer(string playerName)
        {       
            try
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                };

                var playerList = JsonConvert.DeserializeObject<List<Player>>(PlayerFile, settings);

                var player = playerList.FirstOrDefault(x => x.Name == playerName);

                if(player == null)
                {
                    return null;
                }

                return player;
            }
            catch (Exception ex)
            {
                _logger.LogError($"There was a problem with adding new player to the repository. See Exception Details{Environment.NewLine}" +
                    $"{ex.Message}");
                return null;
            }
        }        
    }
}
