using MarrowVale.Business.Entities.Entities;
using MarrowVale.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using MarrowVale.Common.Contracts;
using MarrowVale.Business.Contracts;

namespace MarrowVale.Data.Repositories
{
    public class CommandListRepository : ICommandListRepository
    {
        private readonly ILogger _logger;
        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly IGlobalItemsProvider _globalItemsProvider;
        private string CommandFile { get; set; }
        public CommandListRepository(ILoggerFactory logger, IAppSettingsProvider appSettingsProvider, IGlobalItemsProvider globalItemsProvider)
        {
            _logger = logger.CreateLogger<CommandListRepository>();
            _appSettingsProvider = appSettingsProvider;
            _globalItemsProvider = globalItemsProvider;
            CommandFile = File.ReadAllText(_appSettingsProvider.DataFilesLocation + "\\CommandList.json");
            //gets directory from bin folder
            //CommandFile = File.ReadAllText(Environment.CurrentDirectory + "\\Game Tools\\DataFiles\\CommandList.json");
        }

        public IList<GameCommand> GetCommands()
        { 
            var commandList = new List<GameCommand>();

            try
            {
                var file = JObject.Parse(CommandFile);

                if (file == null)
                {
                    _logger.LogWarning($"File CommandList.json has no contents");
                }               

                foreach(var item in file)
                {
                    var command = new GameCommand()
                    {
                        Command = item.Key,
                        Description = item.Value.ToString()
                    };

                    commandList.Add(command);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return commandList;
        }

        public void PrintCommands()
        {
            var commandList = new List<GameCommand>();

            try
            {
                var file = JObject.Parse(CommandFile);

                if (file == null)
                {
                    _logger.LogWarning($"File CommandList.json has no contents");
                }

                foreach (var item in file)
                {
                    var command = new GameCommand()
                    {
                        Command = item.Key,
                        Description = item.Value.ToString()
                    };

                    commandList.Add(command);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            var border = "-----------------------------------------------------------------------------------";

            Console.WriteLine();
            Console.WindowWidth = _globalItemsProvider.WindowWidth;
            Console.SetCursorPosition((Console.WindowWidth - border.Length) / 2, Console.CursorTop);
            Console.WriteLine("GAME COMMANDS");            
           
            Console.SetCursorPosition((Console.WindowWidth - border.Length) / 2, Console.CursorTop);
            Console.WriteLine(border);

            foreach(var command in commandList)
            {
                Console.SetCursorPosition((Console.WindowWidth - border.Length) / 2, Console.CursorTop);
                Console.WriteLine($"{command.Command} : {command.Description}");
            }

            Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - border.Length) / 2, Console.CursorTop);
            Console.WriteLine("The game saves automatically");

            Console.SetCursorPosition((Console.WindowWidth - border.Length) / 2, Console.CursorTop);
            Console.WriteLine(border + "\n");
        }
    }
}
