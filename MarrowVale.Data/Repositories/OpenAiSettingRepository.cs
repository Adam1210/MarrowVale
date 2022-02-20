using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Evaluator;
using MarrowVale.Data.Contracts;
using Neo4jClient;
using Neo4jClient.Cypher;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MarrowVale.Data.Repositories
{
    public class OpenAiSettingRepository : BaseRepository<OpenAiSettings>, IOpenAiSettingRepository
    {
        private readonly string[] stopOn = new string[] { "\n" };

        public OpenAiSettingRepository(IGraphClient graphClient) : base(graphClient)
        {

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
            defaultSetting.MultipleStopSequences = stopOn;

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
        }


        public Task UpdateDefaultPromptSetting()
        {
            throw new NotImplementedException();
        }

    }
}
