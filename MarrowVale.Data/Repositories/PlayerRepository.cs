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
        private string PlayerFilePath { get; set; }

        private JsonSerializerSettings Settings { get; set; }
        private IList<Player> Players { get; set; }

        public PlayerRepository(ILoggerFactory logger, IAppSettingsProvider appSettingsProvider)
        {
            _logger = logger.CreateLogger<PlayerRepository>();
            _appSettingsProvider = appSettingsProvider;
            //PlayerFilePath = $"{_appSettingsProvider.DataFilesLocation}\\PlayerList.json";
            PlayerFilePath = $"{Environment.CurrentDirectory}\\Game Tools\\DataFiles\\PlayerList.json";

            Settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
            Players = loadPlayers();
            if (Players == null)
            {
                Players = new List<Player>();
            }
        }

        public IList<string> GetPlayers()
        {
            return Players.Select(x => x.SaveInfo()).ToList();
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void RemovePlayer(string playerName)
        {
            var player = Players.FirstOrDefault(x => x.Name == playerName);

            Players.Remove(player);
        }

        public int PlayerCount()
        {
            return Players.Count();
        }

        public Player GetPlayer(string playerName)
        {
            var player = Players.FirstOrDefault(x => x.Name == playerName);

            if (player == null)
            {
                return null;
            }

            return player;
        }

        public void SavePlayers()
        {
            var newJson = JsonConvert.SerializeObject(Players, Formatting.Indented, Settings);

            File.WriteAllText(PlayerFilePath, newJson);
        }

        private IList<Player> loadPlayers()
        {
            var playerFile = File.ReadAllText(PlayerFilePath);
            return JsonConvert.DeserializeObject<List<Player>>(playerFile, Settings);
        }
    }
}
