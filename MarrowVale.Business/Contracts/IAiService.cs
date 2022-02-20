using MarrowVale.Common.Prompts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Contracts
{
    public interface IAiService
    {
        Task<string> BestMatch(string item, string options);
        Task<string> Search(string query, string[] documents);
        Task<string> Complete(string prompt);
        Task<string> Complete(StandardPrompt prompt);

    }
}
