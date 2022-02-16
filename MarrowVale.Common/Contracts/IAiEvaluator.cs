using MarrowVale.Common.Evaluator;
using MarrowVale.Common.Models;
using MarrowVale.Common.Prompts;
using Neo4jClient;
using OpenAI_API;
using OpenAiEvaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Common.Contracts
{
    public interface IAiEvaluator
    {
        Task<Evaluation> CreateEvaluation(CompletionResult result, string apiName, StandardPrompt prompt, OpenAiSettings settings);
        OpenAiSettings GetSetting(string promptType, string subPromptType);
    }
}
