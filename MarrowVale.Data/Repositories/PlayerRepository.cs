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
        private IList<Player> players { get; set; }

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
                TypeNameHandling = TypeNameHandling.All,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
            players = loadPlayers();
            if (players == null)
            {
                players = new List<Player>();
            }
        }

        public IList<string> GetPlayers()
        {
            return players.Select(x => x.SaveInfo()).ToList();
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void RemovePlayer(string playerName)
        {
            var player = players.FirstOrDefault(x => x.Name == playerName);

            players.Remove(player);
        }

        public int PlayerCount()
        {
            return players.Count();
        }

        //public void UpdatePlayer(Player player)
        //{
        //    try
        //    {
        //        var playerList = loadPlayers();

        //        var oldPlayer = playerList.FirstOrDefault(x => x.Name == player.Name);

        //        player.LastSaveDateTime = DateTime.Now;

        //        playerList.Remove(oldPlayer);
        //        playerList.Add(player);


        //        savePlayers(playerList);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"There was a problem with adding new player to the repository. See Exception Details{Environment.NewLine}" +
        //            $"{ex.Message}");
        //    }
        //}

        public Player GetPlayer(string playerName)
        {
            var player = players.FirstOrDefault(x => x.Name == playerName);

            if (player == null)
            {
                return null;
            }

            return player;
        }

        public void SavePlayers()
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
