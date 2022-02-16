using MarrowVale.Common.Prompts;
using System.Threading.Tasks;

namespace MarrowVale.Common.Contracts
{
    public interface IOpenAiProvider
    {
        Task<string> BestMatch(string item, string options);
        Task<string> Search(string query, string[] documents);
        Task<string> Complete(string prompt);
        Task<string> Complete(string prompt, int responseLength, double temperature);
        Task<string> Complete(StandardPrompt prompt);

    }
}
