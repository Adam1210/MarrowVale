using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Business.Entities.Enums.Combat;
using MarrowVale.Business.Entities.Prompts;
using MarrowVale.Common.Constants;
using MarrowVale.Common.Contracts;
using MarrowVale.Common.Prompts;
using MarrowVale.Common.Prompts.Contracts;
using Microsoft.Extensions.Logging;
using Neo4jClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace MarrowVale.Business.Services
{
    public class PromptService : IPromptService
    {
        private readonly ILogger _logger;
        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly string _interpretCommandFilePath;
        private readonly string _dialogueFilePath;
        private readonly string _combatFilePath;
        private readonly string _summaryFilePath;
        private readonly string _divineInterventionFilePath;
        private const string jsonExtension = ".json";
        private JsonSerializerSettings Settings { get; set; }

        public PromptService(ILoggerFactory logger, IAppSettingsProvider appSettingsProvider, IGraphClient graphClient)
        {
            _logger = logger.CreateLogger<PromptService>();
            _appSettingsProvider = appSettingsProvider;

            _interpretCommandFilePath = $"{_appSettingsProvider.BasePromptFileLocation}\\{_appSettingsProvider.PromptInterpretCommandLocation}\\";
            _summaryFilePath = $"{_appSettingsProvider.BasePromptFileLocation}\\{_appSettingsProvider.PromptSummaryLocation}\\";
            _dialogueFilePath = $"{_appSettingsProvider.BasePromptFileLocation}\\{_appSettingsProvider.DialogueFilesLocation}\\";
            _combatFilePath = $"{_appSettingsProvider.BasePromptFileLocation}\\{_appSettingsProvider.CombatFileLocation}\\";
            _divineInterventionFilePath = $"{_appSettingsProvider.BasePromptFileLocation}\\{_appSettingsProvider.DivineInterventionLocation}\\";
            Settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
        }


        public StandardPrompt CommandTypePrompt(string command)
        {
            var commandListFileName = "CommandList";
            var filePath = _interpretCommandFilePath + commandListFileName + jsonExtension;
            return generateStandardPrompt<TextToCommandPrompt>(command, filePath, PromptTypes.TextToCommand, "Default");
        }

        public StandardPrompt GenerateDialogueSummaryPrompt(string dialogue)
        {
            var summaryPromptFileName = "SummarizeConversation";
            var filePath = _dialogueFilePath + summaryPromptFileName + jsonExtension;
            return generateStandardPrompt<DialogueSummaryPrompt>(dialogue, filePath, PromptTypes.DialogueSummary, "Default");
        }
        public StandardPrompt GenerateDialogueIntentPrompt(string text, DialogueTypeEnum dialogueType)
        {
            var filePath = _dialogueFilePath + dialogueType.ToString() + jsonExtension;
            return generateStandardPrompt<DirectObjectPrompt>(text, filePath, PromptTypes.DialogueIntent, dialogueType.ToString());
        }

        public StandardPrompt GenerateCombatIntentPrompt(string text)
        {
            var filePath = _combatFilePath + "InterpretIntent" + jsonExtension;
            return generateStandardPrompt<DirectObjectPrompt>(text, filePath, PromptTypes.CombatIntent, "CombatIntent");
        }

        public StandardPrompt GenerateAttackDescription(string text, CombatActionEnum actionType, CombatSeverityEnum severity)
        {
            var filePath = _combatFilePath + "Attack\\Melee\\" + severity.ToString() + jsonExtension;
            return generateStandardPrompt<CombatPrompt>(text, filePath, PromptTypes.CombatDescription, actionType.ToString());
        }
        public StandardPrompt GenerateDefenseDescription(string text, CombatActionEnum actionType, CombatSeverityEnum severity)
        {
            var filePath = _combatFilePath + "Defense\\Melee\\" + severity.ToString() + jsonExtension;
            return generateStandardPrompt<CombatPrompt>(text, filePath, PromptTypes.CombatDescription, actionType.ToString());
        }

        public StandardPrompt GenerateDialoguePrompt(string input, DialogueResopnseTypeEnum responseType, string outputPrefix)
        {
            var filePath = _dialogueFilePath + responseType.ToString() + jsonExtension;
            return generateStandardPrompt<DialoguePrompt>(input, filePath, PromptTypes.DialogueResponse, responseType.ToString(), outPutPrefix: outputPrefix, useOutputPrefixForExamples:false);
        }

        public StandardPrompt GenerateDirectObjectPrompt(string text, CommandEnum command)
        {
            var filePath = _interpretCommandFilePath + command.ToString() + jsonExtension;
            return generateStandardPrompt<DirectObjectPrompt>(text, filePath, PromptTypes.DirectObjectExtractor, command.ToString());
        }

        public StandardPrompt GenerateSubjectVerbObjectPrompt(string text, CommandEnum command)
        {
            var filePath = _dialogueFilePath + @"SVO/SubjectVerbObject" + jsonExtension;
            return generateStandardPrompt<DirectObjectPrompt>(text, filePath, PromptTypes.SubjectVerbObjectExtractor, command.ToString());
        }


        public StandardPrompt GenerateSummaryDescription(string text, SummaryTypeEnum summaryType)
        {
            var filePath = _summaryFilePath + summaryType.ToString() + jsonExtension;
            return generateStandardPrompt<DirectObjectPrompt>(text, filePath, PromptTypes.SummaryDescription, summaryType.ToString());
        }

        public StandardPrompt GenerateDivineText(string text)
        {
            var filePath = _divineInterventionFilePath + "Explanation" + jsonExtension;
            return generateStandardPrompt<DivineInterventionPrompt>(text, filePath, PromptTypes.SummaryDescription, "Test");
        }

        public StandardPrompt GenerateSmiteText(string text)
        {
            var filePath = _divineInterventionFilePath + "Smite" + jsonExtension;
            return generateStandardPrompt<DivineInterventionPrompt>(text, filePath, PromptTypes.SummaryDescription, "Test");
        }

        private StandardPrompt generateStandardPrompt<T>(string text, string filePath, string type, string subType, string outPutPrefix = null, bool useInputPrefixForExamples = true, bool useOutputPrefixForExamples = true) where T : INonStandardPrompt
        {
            var prompt = JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath)).Standardize();
            prompt.Input = text;
            prompt.Type = type;
            prompt.SubType = subType;
            prompt.OutputPrefix = !string.IsNullOrEmpty(prompt.OutputPrefix) ? prompt.OutputPrefix : outPutPrefix;
            prompt.UseInPutPrefixForExamples = useInputPrefixForExamples;
            prompt.UseOutPutPrefixForExamples = useOutputPrefixForExamples;
            return prompt;
        }


    }
        
}
