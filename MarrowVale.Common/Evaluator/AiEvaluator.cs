using MarrowVale.Business.Entities.Prompts;
using MarrowVale.Common.Contracts;
using MarrowVale.Common.Prompts;
using Neo4jClient;
using Neo4jClient.Cypher;
using OpenAI_API;
using OpenAiEvaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Common.Evaluator
{
    public class AiEvaluator : IAiEvaluator
    {
        //TODO move this and OpenAiProvider out of Common Project

        private readonly IGraphClient _graphClient;
        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly string[] stopOn = new string[] { "\n" };
        public AiEvaluator(IGraphClient graphClient, IAppSettingsProvider appSettingsProvider)
        {
            _graphClient = graphClient;
            _appSettingsProvider = appSettingsProvider;
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
            var promptSubType = new PromptType { Name = prompt.SubType };

            await devToolDatabase()
                .Merge("(promptType:PromptType { Name: $name })")
                .OnCreate()
                .Set("promptType = $promptType")
                .WithParams(new
                {
                    name = promptType.Name,
                    promptType
                })
                .ExecuteWithoutResultsAsync();

            await devToolDatabase()
                .Match("(promptType:PromptType { Name: $promptType })")
                .Merge("(promptType)-[:SUBCLASS]->(subPromptType:SubPromptType{ Name: $subPromptType })")
                .WithParams(new
                {
                    promptType = promptType.Name,
                    subPromptType = promptSubType.Name
                })
                .ExecuteWithoutResultsAsync();


            await devToolDatabase()
                .Match("(x:PromptType)-[:SUBCLASS]->(y:SubPromptType)")
                .Where((PromptType x) => x.Name == promptType.Name)
                .AndWhere((PromptSubType y) => y.Name == promptSubType.Name)
                .Create("(y)-[:INSTANCE]->(evaluation:Evaluation $evaluation)")
                .WithParam("evaluation", evaluation)
                .ExecuteWithoutResultsAsync();

            return evaluation;
        }

        private ICypherFluentQuery devToolDatabase()
        {
            return _graphClient.Cypher
                .WithDatabase("devToolBox");
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

        public OpenAiSettings GetSetting(string promptType, string promptSubType)
        {
            var defaultSetting = devToolDatabase()
                .Match("(promptType:PromptType { Name: $promptType })-[:SUBCLASS]->(subPromptType:SubPromptType { Name: $subPromptType })-[:DEFAULT]->(setting:Setting)")
                .WithParams(new
                {
                    promptType = promptType,
                    subPromptType = promptSubType,
                })
                .Return(setting => setting.As<OpenAiSettings>())
                .ResultsAsync.Result.FirstOrDefault();

            if (defaultSetting == null)
            {
                defaultSetting = devToolDatabase()
                    .Match("(promptType:PromptType { Name: $promptType })-[:DEFAULT]->(setting:Setting)")
                    .WithParams(new
                    {
                        promptType = promptType,
                    })
                    .Return(setting => setting.As<OpenAiSettings>())
                    .ResultsAsync.Result.FirstOrDefault();
            }
            defaultSetting.MultipleStopSequences = stopOn;

            return defaultSetting;
        }

        public Task UpdateDefaultPromptSetting()
        {
            throw new NotImplementedException();
        }

    }
}
