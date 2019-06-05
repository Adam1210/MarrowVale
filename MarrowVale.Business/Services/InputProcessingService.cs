using MarrowVale.Business.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

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

            foreach(var command in inputCommands)
            {
                //trims inputs from user
                command.Trim();
            }


            switch(input.ToUpper())
            {
                case "/HELP":
                    Console.WriteLine("help");
                    break;
                case "OPEN BAG":
                    Console.WriteLine("Open Bag");
                    break;
                case "NORTH":
                    Console.WriteLine("North");
                    break;
                case "SOUTH":
                    Console.WriteLine("South");
                    break;
                case "EAST":
                    Console.WriteLine("East");
                    break;
                case "WEST":
                    Console.WriteLine("West");
                    break;
                case "NORTHEAST":
                    Console.WriteLine("Northeast");
                    break;
                case "NORTHWEST":
                    Console.WriteLine("Northwest");
                    break;
                case "SOUTHEAST":
                    Console.WriteLine("Southeast");
                    break;
                case "SOUTHWEST":
                    Console.WriteLine("Southwest");
                    break;
                case "CLIMB UP {structure}":
                    Console.WriteLine("Climb up stucture");
                    break;
                case "CLIMB DOWN {structure}":
                    Console.WriteLine("Climb down stucture");
                    break;
                case "TAKE {item}":
                    Console.WriteLine("Take item");
                    break;
                case "GIVE {item} to {target}":
                    Console.WriteLine("Give item to target");
                    break;
                case "ATTACK {target}":
                    Console.WriteLine("Attack target");
                    break;
                case "EQUIP {item}":
                    Console.WriteLine("Equip item");
                    break;
                case "USE {item}":
                    Console.WriteLine("Use item");
                    break;
                case "READ {item}":
                    Console.WriteLine("Read item");
                    break;
                case "CAST {spell name} AT {target}":
                    Console.WriteLine("Cast spell at target");
                    break;
                case "MOVE {structure}":
                    Console.WriteLine("Move structure");
                    break;
                case "HEALTH":
                    Console.WriteLine("Shows current health");
                    break;
            }

            return "";
        }
    }
}
