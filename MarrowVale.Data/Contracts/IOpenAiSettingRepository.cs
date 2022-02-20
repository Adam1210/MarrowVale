using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Evaluator;
using System.Threading.Tasks;

namespace MarrowVale.Data.Contracts
{
    public interface IOpenAiSettingRepository : IBaseRepository<OpenAiSettings>
    {
        Task CreateSetting(string promptType, OpenAiSettings settings, string subPromptType = null);
        OpenAiSettings GetSetting(string promptType, string promptSubType);
        Task UpdateDefaultPromptSetting();
        Task CreatePrompts(PromptType promptType, string subType = null);
    }
}
