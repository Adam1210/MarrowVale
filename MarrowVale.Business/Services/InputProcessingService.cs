using MarrowVale.Business.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MarrowVale.Business.Services
{
    public class InputProcessingService : IInputProcessingService
    {
        private readonly ILogger _logger;
        private readonly ICommandListRepository _commandListRepository;
        public InputProcessingService(ILoggerFactory logger, ICommandListRepository commandListRepository)
        {
            _logger = logger.CreateLogger<InputProcessingService>();
            _commandListRepository = commandListRepository;
        }

        public string ProcessInput(string input)
        {        
            //account for multiple commands separated by a commassssssssss
            var commands = _commandListRepository.GetCommands();

            var inputCommands = input.Split(',');

            foreach (var command in inputCommands)
            {
                var cmdTokens = command.Trim().Split(' ');

                switch (cmdTokens[0])
                {
                    case "/HELP":
                        Console.WriteLine("help");
                        break;
                    case "OPEN":
                        Console.WriteLine("Open Bag");
                        break;
                    case "MOVE":
                        Console.WriteLine("North");
                        break;                 
                    case "CLIMB":
                        Console.WriteLine("Climb up stucture");
                        break;                
                    case "TAKE":
                        Console.WriteLine("Take item");
                        break;
                    case "GIVE":
                        Console.WriteLine("Give item to target");
                        break;
                    case "ATTACK":
                        Console.WriteLine("Attack target");
                        break;
                    case "EQUIP":
                        Console.WriteLine("Equip item");
                        break;
                    case "USE":
                        Console.WriteLine("Use item");
                        break;
                    case "READ":
                        Console.WriteLine("Read item");
                        break;
                    case "CAST":
                        Console.WriteLine("Cast spell at target");
                        break;                
                    case "HEALTH":
                        Console.WriteLine("Shows current health");
                        break;
                    case "CLASS":
                        Console.WriteLine("Displays class abilities");
                        break;
                    case "SPEAK":
                        Console.WriteLine("Starts a dialogue with the given character");
                        break;
                }
            }

            //foreach (var command in commands)
            //{
            //    //trims inputs from user
            //    var cmd = command.Command.Trim();
            //    Regex rx = new Regex("(\\{)(.*?)(\\})");
            //    var result = rx.Replace(cmd, "");
            //    Console.WriteLine(result);
            //}

            //switch (input.ToUpper())
            //{
            //    case "/HELP":
            //        Console.WriteLine("help");
            //        break;
            //    case "OPEN BAG":
            //        Console.WriteLine("Open Bag");
            //        break;
            //    case "MOVE NORTH":
            //        Console.WriteLine("North");
            //        break;
            //    case "MOVE SOUTH":
            //        Console.WriteLine("South");
            //        break;
            //    case "MOVE EAST":
            //        Console.WriteLine("East");
            //        break;
            //    case "MOVE WEST":
            //        Console.WriteLine("West");
            //        break;
            //    case "MOVE NORTHEAST":
            //        Console.WriteLine("Northeast");
            //        break;
            //    case "MOVE NORTHWEST":
            //        Console.WriteLine("Northwest");
            //        break;
            //    case "MOVE SOUTHEAST":
            //        Console.WriteLine("Southeast");
            //        break;
            //    case "MOVE SOUTHWEST":
            //        Console.WriteLine("Southwest");
            //        break;
            //    case "CLIMB UP {structure}":
            //        Console.WriteLine("Climb up stucture");
            //        break;
            //    case "CLIMB DOWN {structure}":
            //        Console.WriteLine("Climb down stucture");
            //        break;
            //    case "TAKE {item}":
            //        Console.WriteLine("Take item");
            //        break;
            //    case "GIVE {item} to {target}":
            //        Console.WriteLine("Give item to target");
            //        break;
            //    case "ATTACK {target}":
            //        Console.WriteLine("Attack target");
            //        break;
            //    case "EQUIP {item}":
            //        Console.WriteLine("Equip item");
            //        break;
            //    case "USE {item}":
            //        Console.WriteLine("Use item");
            //        break;
            //    case "READ {item}":
            //        Console.WriteLine("Read item");
            //        break;
            //    case "CAST {spell name} AT {target}":
            //        Console.WriteLine("Cast spell at target");
            //        break;
            //    case "MOVE {structure}":
            //        Console.WriteLine("Move structure");
            //        break;
            //    case "HEALTH":
            //        Console.WriteLine("Shows current health");
            //        break;
            //    case "CLASS ABILITIES":
            //        Console.WriteLine("Displays class abilities");
            //        break;
            //    case "SPEAK TO {character}":
            //        Console.WriteLine("Starts a dialogue with the given character");
            //        break;
            //}

            return "";
        }

        private string parseMoveCommand(string[] cmdTokens)
        {


            return "";
        }
    }
}
