using MarrowVale.Common.Evaluator;
using MarrowVale.Data.Contracts;
using Neo4jClient;
using Neo4jClient.Cypher;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Data.Repositories
{
    public class OpenAiSettingRepository : IOpenAiSettingRepository
    {
        private readonly IGraphClient _graphClient;
        public OpenAiSettingRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public async Task CreateSetting(string promptType, OpenAiSettings settings, string subPromptType = null)
        {
            if (subPromptType != null)
            {
                await devToolDatabase()
                    .Match("(promptType:PromptType { Name: $promptType })-[:SUBCLASS]->(subPromptType:SubPromptType { Name: $subPromptType })")
                    .Merge("(subPromptType)-[:DEFAULT]->(settings:Setting)")
                    .OnCreate()
                    .Set("settings = $settings")
                    .WithParams(new
                    {
                        promptType = promptType,
                        subPromptType = subPromptType,
                        settings = settings
                    })
                    .ExecuteWithoutResultsAsync();
            }
            else 
            {
                await devToolDatabase()
                .Match("(promptType:PromptType { Name: $promptType })")
                .Merge("(promptType)-[:DEFAULT]->(settings:Setting)")
                .OnCreate()
                .Set("settings = $settings")
                .WithParams(new
                {
                    promptType = promptType,
                    settings = settings
                })
                .ExecuteWithoutResultsAsync();
            }


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
            return defaultSetting;
        }

        public async Task CreatePrompts(PromptType promptType, string subType = null)
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

            //await devToolDatabase()
            //    .Match("(promptType:PromptType { Name: $promptType })")
            //    .Merge("(promptType)-[:SUBCLASS]->(subPromptType:SubPromptType{ Name: $subPromptType })")
            //    .WithParams(new
            //    {
            //        promptType = promptType.Name,
            //        subPromptType = promptSubType.Name
            //    })
            //    .ExecuteWithoutResultsAsync();
        }


        public Task UpdateDefaultPromptSetting()
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
