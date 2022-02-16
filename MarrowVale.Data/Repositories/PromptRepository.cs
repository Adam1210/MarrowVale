using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using Neo4jClient;
using Neo4jClient.Cypher;
using Newtonsoft.Json;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarrowVale.Data.Repositories
{
    public class PromptRepository : IPromptRepository
    {
        private readonly IGraphClient _graphClient;
        public PromptRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

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

        private ICypherFluentQuery devToolDatabase()
        {
            return _graphClient.Cypher
                .WithDatabase("devToolBox");
        }
    }
}
