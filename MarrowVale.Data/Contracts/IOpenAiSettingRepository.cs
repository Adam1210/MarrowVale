using MarrowVale.Common.Evaluator;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Data.Contracts
{
    public interface IOpenAiSettingRepository
    {
        Task CreateSetting(string promptType, OpenAiSettings settings, string subPromptType = null);
        OpenAiSettings GetSetting(string promptType, string promptSubType);
        Task UpdateDefaultPromptSetting();
        Task CreatePrompts(PromptType promptType, string subType = null);
    }
}
