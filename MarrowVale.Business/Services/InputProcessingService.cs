using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Commands;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MarrowVale.Business.Services
{
    public class InputProcessingService : IInputProcessingService
    {
        private readonly ILogger _logger;
        private readonly IPrintService _printService;
        private readonly IAiService _aiService;
        private readonly IWorldContextService _worldContextService;
        private readonly IPromptService _promptService;
        private readonly IDivineInterventionService _divineInterventionService;

        public InputProcessingService(ILoggerFactory logger, IPrintService printService, IAiService aiService, 
            IWorldContextService worldContextService, IPromptService promptService, IDivineInterventionService divineInterventionService)
        {
            _logger = logger.CreateLogger<InputProcessingService>();
            _printService = printService;
            _aiService = aiService;
            _worldContextService = worldContextService;
            _promptService = promptService;
            _divineInterventionService = divineInterventionService;
        }

        public Command ProcessInput(string input, string context, Player player)
        {
            var prompt = _promptService.CommandTypePrompt(input);
            var commandName = _aiService.Complete(prompt).Result;
            var isValidEnum = CommandEnum.TryParse(commandName, out CommandEnum commandEnum);

            if (!isValidEnum)
                return tryAgain(context, player, input);

            return CreateCommand(input, context, player, commandEnum);

        }


        private Command CreateCommand(string input, string context, Player player, CommandEnum command) =>
            command switch
            {
                CommandEnum.Inventory => directObjectCommand(input, context, player, command),
                CommandEnum.Enter => directObjectCommand(input, context, player, command),
                CommandEnum.Exit => directObjectCommand(input, context, player, command),
                CommandEnum.Traverse => directObjectCommand(input, context, player, command),
                CommandEnum.Attack => directObjectCommand(input, context, player, command),
                CommandEnum.Speak => directObjectCommand(input, context, player, command),
                CommandEnum.Health => new Command(CommandEnum.Health),
                CommandEnum.MoveToward => throw new NotImplementedException(),
                CommandEnum.ClimbUp => throw new NotImplementedException(),
                CommandEnum.ClimbDown => throw new NotImplementedException(),
                CommandEnum.Swim => throw new NotImplementedException(),
                CommandEnum.Dance => throw new NotImplementedException(),
                CommandEnum.Give => throw new NotImplementedException(),
                CommandEnum.Equip => throw new NotImplementedException(),
                CommandEnum.Use => throw new NotImplementedException(),
                CommandEnum.Cast => throw new NotImplementedException(),
                CommandEnum.Abilities => throw new NotImplementedException(),
                //CommandEnum.Read => throw new NotImplementedException(),
                _ => tryAgain(context, player, input)
            };


        private Command directObjectCommand(string input, string context, Player player, CommandEnum command)
        {
            while (true)
            {
                try
                {
                    var prompt = _promptService.GenerateDirectObjectPrompt(input, command);
                    var directObject = _aiService.Complete(prompt.ToString()).Result;
                    var node = _worldContextService.GetObjectLabelIdPair(directObject, player, input);
                    if (string.IsNullOrWhiteSpace(node?.Id))
                    {
                        var divineResponse = _divineInterventionService.HandleError(input, "Not found");
                        Debug.WriteLine("Direct Object Node Not Found");
                        _printService.PrintDivineText(divineResponse);
                        input = Console.ReadLine();
                    }
                    else
                        return new Command { Type = command, DirectObjectNode = node };
                }
                catch
                {
                    var divineResponse = _divineInterventionService.HandleError(input, "Unexpected Error");
                    _printService.PrintDivineText(divineResponse);
                    input = _printService.ReadInput();
                }

            }
        }

        private Command tryAgain(string context, Player player, string input)
        {
            var divineResponse = _divineInterventionService.HandleError(input, "Unexpected Command");
            _printService.PrintDivineText(divineResponse);
            input = Console.ReadLine();
            return ProcessInput(input, context, player);
        }

    }
}
