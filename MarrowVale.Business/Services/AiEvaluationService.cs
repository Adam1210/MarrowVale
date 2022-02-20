using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Contracts;
using MarrowVale.Common.Prompts;
using MarrowVale.Data.Contracts;
using Neo4jClient;
using Neo4jClient.Cypher;
using OpenAI_API;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MarrowVale.Common.Evaluator
{
    public class AiEvaluationService : IAiEvaluationService
    {
        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly IPromptRepository _promptRepository;
        private readonly IOpenAiEvaluationRepository _openAiEvaluationRepository;
        public AiEvaluationService(IAppSettingsProvider appSettingsProvider, 
                                   IPromptRepository promptRepository, IOpenAiEvaluationRepository openAiEvaluationRepository)
        {
            _appSettingsProvider = appSettingsProvider;
            _promptRepository = promptRepository;
            _openAiEvaluationRepository = openAiEvaluationRepository;
        }

        public async Task<Evaluation> CreateEvaluation(CompletionResult result, string apiName, StandardPrompt prompt, OpenAiSettings settings)
        {
            var evaluation = new Evaluation
            {
                RunDate = DateTime.Now,
                ApiName = apiName,
                Engine = result.Model,
                Prompt = prompt.ToString(),
                Response = result.ToString(),
                ResponseTime = result.ProcessingTime,
                FrequencyPenalty = settings.FrequencyPenalty,
                MaxTokens = settings.MaxTokens,
                MultipleStopSequences = settings.MultipleStopSequences,
                PresencePenalty = settings.PresencePenalty,
                Logprobs = settings.Logprobs,
                StopSequence = settings.StopSequence,
                Temperature = settings.Temperature,
                TopP = settings.TopP
            };

            evaluation.TokenCount = estimateTokens(evaluation);
            evaluation.Cost = estimateCost(evaluation);
            evaluation.Rating = ratePrompt(prompt, result);


            var promptType = new PromptType { Name = prompt.Type };
            var promptSubType = new PromptSubType { Name = prompt.SubType };
            await _promptRepository.EnsurePromptCreated(promptType, promptSubType);
            await _openAiEvaluationRepository.CreateEvaluation(promptType, promptSubType, evaluation);



            return evaluation;
        }


        private int estimateTokens(Evaluation evaluation)
        {
            return evaluation.Prompt.Length / 4;
        }
        private decimal estimateCost(Evaluation evaluation)
        {
            decimal pricePerThousandTokens = 0;
            if (evaluation.Engine == Engine.Ada)
                pricePerThousandTokens = 0.0008m;
            if (evaluation.Engine == Engine.Babbage)
                pricePerThousandTokens = 0.0008m;
            if (evaluation.Engine == Engine.Curie)
                pricePerThousandTokens = 0.0008m;
            if (evaluation.Engine == Engine.Davinci)
                pricePerThousandTokens = 0.0008m;

            return evaluation.TokenCount * (pricePerThousandTokens / 1000);

        }

        private int? ratePrompt(StandardPrompt prompt, CompletionResult response)
        {
            if (!_appSettingsProvider.AskForRating)
                return null;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(prompt.ToString());
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("-----------------------------------");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(response.ToString());

            int ratingNumber = -1;
            while (ratingNumber < 0 || ratingNumber > 10)
            {
                Console.WriteLine("Rate Prompt (1-10):");
                var rating = Console.ReadLine();
                Int32.TryParse(rating, out ratingNumber);
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            return ratingNumber;
        }


    }
}
