using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Constants;
using MarrowVale.Common.Evaluator;
using OpenAI_API;
using System.Collections.Generic;

namespace MarrowVale.Data.Seeder
{
    public class OpenAiSettingSeeds
    {
        private readonly string[] stopOn = new string[] { "\n" };


        public OpenAiSettingSeeds()
        {
        }

        public Dictionary<string, OpenAiSettings> DefaultSettingCollection()
        {
            var settingDictionary = new Dictionary<string, OpenAiSettings>();

            var summarizationSettings = new OpenAiSettings(temperature: 0.5, max_tokens: 50, frequencyPenalty: .2, presencePenalty: 0, stopSequences: stopOn, engineName: Engine.Curie);
            settingDictionary[PromptTypes.DialogueSummary] = summarizationSettings;

            var dialogueIntentSettings = new OpenAiSettings(temperature: 0.1, max_tokens: 3, frequencyPenalty: 0, presencePenalty: 0, stopSequences: stopOn, top_p: 1, engineName: Engine.Babbage);
            settingDictionary[PromptTypes.DialogueIntent] = dialogueIntentSettings;

            var dialogueResponseSetting = new OpenAiSettings(temperature: 0.65, max_tokens: 35, frequencyPenalty: 0, presencePenalty: .6, stopSequences: stopOn, engineName: Engine.Curie);
            settingDictionary[PromptTypes.DialogueResponse] = dialogueResponseSetting;

            var summaryDescription = new OpenAiSettings(temperature: 0.18, max_tokens: 80, frequencyPenalty: .2, presencePenalty: 0, stopSequences: stopOn, engineName: Engine.Curie);
            settingDictionary[PromptTypes.SummaryDescription] = summaryDescription;


            var combatIntentSetting = new OpenAiSettings(temperature: 0.1, max_tokens: 3, frequencyPenalty: 0, presencePenalty: 0, stopSequences: stopOn, top_p: 1, engineName: Engine.Babbage);
            settingDictionary[PromptTypes.CombatIntent] = combatIntentSetting;

            var combatDescriptionSetting = new OpenAiSettings(temperature: 0.18, max_tokens: 80, frequencyPenalty: .2, presencePenalty: 0, stopSequences: stopOn, engineName: Engine.Curie);
            settingDictionary[PromptTypes.CombatDescription] = combatDescriptionSetting;

            var textToCommandSetting = new OpenAiSettings(temperature: 0.65, max_tokens: 35, frequencyPenalty: 0, presencePenalty: .6, stopSequences: stopOn, engineName: Engine.Babbage);
            settingDictionary[PromptTypes.TextToCommand] = textToCommandSetting;

            var divineInterventionSetting = new OpenAiSettings(temperature: .8, max_tokens: 60, frequencyPenalty: 0, presencePenalty: .6, stopSequences: stopOn, engineName: Engine.Davinci);
            settingDictionary[PromptTypes.DivineIntervention] = divineInterventionSetting;

            var defaultTemp = new OpenAiSettings(temperature: 0.18, max_tokens: 80, frequencyPenalty: .2, presencePenalty: 0, stopSequences: stopOn);
            settingDictionary[PromptTypes.DirectObjectExtractor] = defaultTemp;
            settingDictionary[PromptTypes.SubjectVerbObjectExtractor] = defaultTemp;

            return settingDictionary;

        }

        public List<PromptType> DefaultPromptTypes()
        {
            var promptTypes = new List<PromptType>();
            promptTypes.Add(new PromptType(PromptTypes.DialogueIntent));
            promptTypes.Add(new PromptType(PromptTypes.DialogueResponse));
            promptTypes.Add(new PromptType(PromptTypes.DialogueSummary));
            promptTypes.Add(new PromptType(PromptTypes.DirectObjectExtractor));
            promptTypes.Add(new PromptType(PromptTypes.SubjectVerbObjectExtractor));
            promptTypes.Add(new PromptType(PromptTypes.SummaryDescription));
            promptTypes.Add(new PromptType(PromptTypes.CombatIntent));
            promptTypes.Add(new PromptType(PromptTypes.CombatDescription));
            return promptTypes;

        }

    }
}
