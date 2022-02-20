using MarrowVale.Business.Entities.Prompts;
using MarrowVale.Common.Contracts;
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


        public OpenAiProvider(ILoggerFactory logger, IAppSettingsProvider appSettingsProvider)
        {
            _logger = logger.CreateLogger<OpenAiProvider>();
            _appSettingsProvider = appSettingsProvider;
            apiKey = _appSettingsProvider.OpenAiKey;
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
        public async Task<CompletionResult> Complete(CompletionRequest completionRequest, string engineName = "Curie")
        {
            var api = new OpenAIAPI(engine: engineName, apiKeys: apiKey);
            var result = await api.Completions.CreateCompletionAsync(completionRequest);
            return result;
        }




    }
}
