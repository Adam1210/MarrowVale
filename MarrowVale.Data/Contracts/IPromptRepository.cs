using MarrowVale.Business.Entities.Entities;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarrowVale.Data.Contracts
{
    public interface IPromptRepository : IBaseRepository<PromptType>
    {
        Task CreateDefaultPromptSetting(string promptType, string subPromptType, CompletionRequest request);
        Task<CompletionRequest> GetDefaultPromptSetting(string promptType, string subPromptType, CompletionRequest request);
        Task UpdateDefaultPromptSetting(Player player);
        Task EnsurePromptCreated(PromptType promptType, PromptSubType promptSubType);
    }
}
