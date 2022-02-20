using MarrowVale.Business.Entities.Entities;
using MarrowVale.Data.Contracts;
using Neo4jClient;
using Neo4jClient.Cypher;
using System.Threading.Tasks;

namespace MarrowVale.Data.Repositories
{
    public class OpenAiEvaluationRepository : BaseRepository<Evaluation>, IOpenAiEvaluationRepository
    {
        public OpenAiEvaluationRepository(IGraphClient graphClient) : base(graphClient) { }

        public async Task CreateEvaluation(PromptType promptType, PromptSubType subPromptType, Evaluation evaluation)
        {
            await devToolDatabase()
                .Match("(x:PromptType)-[:SUBCLASS]->(y:SubPromptType)")
                .Where((PromptType x) => x.Name == promptType.Name)
                .AndWhere((PromptSubType y) => y.Name == subPromptType.Name)
                .Create("(y)-[:INSTANCE]->(evaluation:Evaluation $evaluation)")
                .WithParam("evaluation", evaluation)
                .ExecuteWithoutResultsAsync();
        }
    }
}
