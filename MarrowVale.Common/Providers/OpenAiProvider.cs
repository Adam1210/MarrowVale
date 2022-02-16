using MarrowVale.Business.Entities.Prompts;
using MarrowVale.Common.Contracts;
using MarrowVale.Common.Evaluator;
using MarrowVale.Common.Models;
using MarrowVale.Common.Prompts;
using Microsoft.Extensions.Logging;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Common.Providers
{
    public class OpenAiProvider : IOpenAiProvider
    {
        private readonly string[] stopOn = new string[] { "\n" };
        private readonly ILogger _logger;
        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly string apiKey;
        private readonly IAiEvaluator _aiEvaluator;


        public OpenAiProvider(ILoggerFactory logger, IAppSettingsProvider appSettingsProvider, IAiEvaluator aiEvaluator)
        {
            _logger = logger.CreateLogger<OpenAiProvider>();
            _appSettingsProvider = appSettingsProvider;
            apiKey = _appSettingsProvider.OpenAiKey;
            _aiEvaluator = aiEvaluator;
        }
        public Task<string> BestMatch(string item, string options)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Search(string query, string[] documents)
        {
            var api = new OpenAIAPI(engine: Engine.Davinci, apiKeys: apiKey);
            var result = await api.Search.GetBestMatchAsync(query, documents);
            return result.ToString();
        }

        public async Task<string> Complete(string text)
        {
            var api = new OpenAIAPI(engine: Engine.Curie, apiKeys: apiKey);
            var result = await api.Completions.CreateCompletionAsync(text, temperature: 0.65, max_tokens:35, frequencyPenalty:0, presencePenalty:.6, stopSequences: stopOn);
            return result.ToString();
        }

        public async Task<string> Complete(string text, int responseLength, double temp)
        {
            var api = new OpenAIAPI(engine: Engine.Curie, apiKeys: apiKey);
            var result = await api.Completions.CreateCompletionAsync(text, temperature: temp, max_tokens: responseLength);
            return result.ToString();
        }


        public async Task<string> Complete(StandardPrompt prompt)
        {
            var settings = _aiEvaluator.GetSetting(prompt.Type, prompt.SubType);
            var completionRequest = createCompletionRequest(settings, prompt.ToString());

            var api = new OpenAIAPI(engine: settings.EngineName, apiKeys: apiKey);
            var result = await api.Completions.CreateCompletionAsync(completionRequest);
            await _aiEvaluator.CreateEvaluation(result, "Completion", prompt, settings);
            return result.ToString();
        }

        private CompletionRequest createCompletionRequest(OpenAiSettings settings, string prompt)
        {
            return new CompletionRequest(prompt, temperature: 0.5, max_tokens: 50, frequencyPenalty: .2, presencePenalty: 0, stopSequences: stopOn);
        }



    }
}
