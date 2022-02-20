using MarrowVale.Business.Entities.Entities;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarrowVale.Data.Contracts
{
    public interface IOpenAiEvaluationRepository : IBaseRepository<Evaluation>
    {
        Task CreateEvaluation(PromptType promptType, PromptSubType subPromptType, Evaluation evaluation);
    }
}
