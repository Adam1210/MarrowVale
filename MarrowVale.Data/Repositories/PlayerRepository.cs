using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

        private JsonSerializerSettings settings { get; set; }
        
        public PlayerRepository(ILoggerFactory logger, IAppSettingsProvider appSettingsProvider)
        {
            _logger = logger.CreateLogger<PlayerRepository>();
            _appSettingsProvider = appSettingsProvider;
            //PlayerFilePath = $"{_appSettingsProvider.DataFilesLocation}\\PlayerList.json";
            PlayerFilePath = $"{Environment.CurrentDirectory}\\Game Tools\\DataFiles\\PlayerList.json";
            //PlayerFile = File.ReadAllText(PlayerFilePath);
            PlayerFile = File.ReadAllText(PlayerFilePath);

            settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }

        public IList<Player> GetPlayers()
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
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
                var playerList = loadPlayers();

                playerList.Add(player);

                savePlayers(playerList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There was a problem with adding new player to the repository. See Exception Details{Environment.NewLine}" +
                    $"{ex.Message}");
            }
        }

        public void RemovePlayer(string playerName)
        {
            try
            {
                var playerList = loadPlayers();

                var player = playerList.FirstOrDefault(x => x.Name == playerName);

                playerList.Remove(player);

                savePlayers(playerList);
            }
            catch(Exception ex)
            {
                _logger.LogError($"There was a problem with removing player {playerName} from the repository. See Exception Details{Environment.NewLine}" +
                    $"{ex.Message}");
            }
        }

        public int PlayerCount()
        {
            try
            {
                var playerList = loadPlayers();

                return playerList.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError($"There was a problem with couting players . See Exception Details{Environment.NewLine}" +
                    $"{ex.Message}");
                return 0;
            }
        }
               
        public void UpdatePlayer(Player player)
        {
            try
            {
                var playerList = loadPlayers();

                var oldPlayer = playerList.FirstOrDefault(x => x.Name == player.Name);

                player.LastSaveDateTime = DateTime.Now;

                playerList.Remove(oldPlayer);
                playerList.Add(player);
                

                savePlayers(playerList);
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
                var playerList = loadPlayers();

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

        private void savePlayers(IList<Player> players)
        {
            var newJson = JsonConvert.SerializeObject(players, Formatting.Indented, settings);

            File.WriteAllText(PlayerFilePath, newJson);
        }
        
        private IList<Player> loadPlayers()
        {
            return JsonConvert.DeserializeObject<List<Player>>(PlayerFile, settings);
        }
    }
}
