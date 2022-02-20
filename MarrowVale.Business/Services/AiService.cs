using MarrowVale.Business.Contracts;
using MarrowVale.Common.Contracts;
using MarrowVale.Common.Evaluator;
using MarrowVale.Common.Prompts;
using MarrowVale.Data.Contracts;
using OpenAI_API;
using System;
using System.Threading.Tasks;

namespace MarrowVale.Business.Services
{
    public class AiService : IAiService
    {
        private readonly IOpenAiProvider _openAiProvider;
        private readonly IAiEvaluationService _aiEvaluatorService;
        private readonly IOpenAiSettingRepository _openAiSettingRepository;
        private readonly string[] stopOn = new string[] { "\n" };

        public AiService(IOpenAiProvider openAiProvider, IAiEvaluationService aiEvaluatorService, IOpenAiSettingRepository openAiSettingRepository)
        {
            _openAiProvider = openAiProvider;
            _aiEvaluatorService = aiEvaluatorService;
            _openAiSettingRepository = openAiSettingRepository;
        }

        public async Task<string> BestMatch(string item, string options)
        {
            throw new NotImplementedException();
        }

        //Refactor to use StandardPrompt
        public async Task<string> Complete(string prompt)
        {
            return await _openAiProvider.Complete(prompt);
        }

        public async Task<string> Complete(StandardPrompt prompt)
        {
            var settings = _openAiSettingRepository.GetSetting(prompt.Type, prompt.SubType);
            var completionRequest = createCompletionRequest(settings, prompt.ToString());
            var result = await _openAiProvider.Complete(completionRequest, settings.EngineName);

            var apiName = "Completion";
            await _aiEvaluatorService.CreateEvaluation(result, apiName, prompt, settings);
            return result.ToString();
        }

        public async Task<string> Search(string query, string[] documents)
        {
            return await _openAiProvider.Search(query, documents);

        }

        private CompletionRequest createCompletionRequest(OpenAiSettings settings, string prompt)
        {
            return new CompletionRequest(prompt, 
                                        temperature: settings.Temperature, 
                                        max_tokens: settings.MaxTokens, 
                                        frequencyPenalty: settings.FrequencyPenalty, 
                                        presencePenalty: settings.PresencePenalty, 
                                        stopSequences: stopOn);
        }
    }
}
