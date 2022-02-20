using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Commands;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Common;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;

namespace MarrowVale.Business.Services
{
    public class DivineInterventionService : IDivineInterventionService
    {
        private readonly ILogger _logger;
        private readonly IPrintService _printService;
        private readonly IAiService _aiService;
        private readonly IWorldContextService _worldContextService;
        private readonly IPromptService _promptService;
        private readonly IDeityRepository _deityRepository;

        public DivineInterventionService(ILoggerFactory logger, IPrintService printService, IAiService aiService,
            IWorldContextService worldContextService, IPromptService promptService, IDeityRepository deityRepository)
        {
            _logger = logger.CreateLogger<InputProcessingService>();
            _printService = printService;
            _aiService = aiService;
            _worldContextService = worldContextService;
            _promptService = promptService;
            _deityRepository = deityRepository;
        }

        public string HandleError(string context, string input, string error)
        {
            var promptInput = new StringBuilder();
            promptInput.AppendNewLine($"Action: {input}");
            promptInput.AppendNewLine($"Error: {error}");

            var divinePrompt = _promptService.GenerateDivineText(promptInput.ToString());
            var divineResponse = _aiService.Complete(divinePrompt);
            return divineResponse.Result;
        }

        public string Smite(Npc character, Deity deity = null, string requestedAction = null)
        {
            if (deity?.Name == null)
                deity = _deityRepository.DefaultDeity();


            var promptInput = new StringBuilder();
            promptInput.AppendNewLine($"Deity: {deity?.Name ?? "Mithras"}");
            promptInput.AppendNewLine($"Character: {character.FullName}");

            var divinePrompt = _promptService.GenerateSmiteText(promptInput.ToString());
            var divineResponse = _aiService.Complete(divinePrompt).Result;

            _printService.PrintDivineText(divineResponse);

            return "";
        }
    }
}
