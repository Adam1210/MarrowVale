using MarrowVale.Business.Entities.Entities;
using MarrowVale.Data.Contracts;
using Neo4jClient;
using Neo4jClient.Cypher;
using OpenAI_API;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MarrowVale.Data.Repositories
{
    public class PromptRepository : BaseRepository<PromptType>, IPromptRepository
    {
        public PromptRepository(IGraphClient graphClient) : base(graphClient){}

        public async Task CreateDefaultPromptSetting(string promptType, string subPromptType, CompletionRequest request)
        {
            await devToolDatabase()
                .Match("(promptType:PromptType { Name: $promptType })-[:SUBCLASS]->(subPromptType:SubPromptType { Name: $subPromptType })")
                .Merge("(subPromptType)-[:DEFAULT]->(request:Setting)")
                .OnCreate()
                .Set("request = $request")
                .WithParams(new
                {
                    promptType = promptType,
                    subPromptType = subPromptType,
                    request = request
                })
                .ExecuteWithoutResultsAsync();
        }

        public async Task<CompletionRequest> GetDefaultPromptSetting(string promptType, string subPromptType, CompletionRequest request)
        {
            return devToolDatabase()
                .Match("(promptType:PromptType { Name: $promptType })-[:SUBCLASS]->(subPromptType:SubPromptType { Name: $subPromptType })-[:DEFAULT]->(setting:Setting)")
                .WithParams(new
                {
                    promptType = promptType,
                    subPromptType = subPromptType,
                })
                .Return(setting => setting.As<CompletionRequest>())
                .ResultsAsync.Result.FirstOrDefault();
        }

        public Task UpdateDefaultPromptSetting(Player player)
        {
            throw new NotImplementedException();
        }

        public async Task EnsurePromptCreated(PromptType promptType, PromptSubType promptSubType)
        {
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
            }).ExecuteWithoutResultsAsync();
        }

    }
}
