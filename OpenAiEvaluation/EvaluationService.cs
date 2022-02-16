using MarrowVale.Business.Entities.Prompts;
using Neo4jClient;
using OpenAI_API;
using System;
using System.Threading.Tasks;

namespace OpenAiEvaluation
{
    public class EvaluationService
    {
        private readonly IGraphClient _graphClient;
        public EvaluationService(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }


        public async Task<Evaluation> CreateEvaluation(CompletionResult result, string apiName, StandardPrompt prompt, string response)
        {
            var evaluation = new Evaluation
            {
                ApiName = apiName,
                Engine = result.Model,
                Prompt = prompt,
                Response = response,
            };

            evaluation.TokenCount = estimateTokens(evaluation);
            evaluation.Cost = estimateCost(evaluation);


            await _graphClient.Cypher
                .WithDatabase("devToolBox")
                .Create("(evaluation:Evaluation $evaluation)-[:OWNS]->(inventory:Inventory $inventory)-[:PARTOF]->(n)")
                .WithParam("evaluation", evaluation)
                .ExecuteWithoutResultsAsync();

            return null;
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
    }
}
