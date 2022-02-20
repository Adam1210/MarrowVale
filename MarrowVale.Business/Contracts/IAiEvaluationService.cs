using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Evaluator;
using MarrowVale.Common.Prompts;
using OpenAI_API;
using System.Threading.Tasks;

namespace MarrowVale.Business.Contracts
{
    public interface IAiEvaluationService
    {
        Task<Evaluation> CreateEvaluation(CompletionResult result, string apiName, StandardPrompt prompt, OpenAiSettings settings);
    }
}
