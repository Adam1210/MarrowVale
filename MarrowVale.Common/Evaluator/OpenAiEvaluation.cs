using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarrowVale.Common.Models;
using OpenAI_API;

namespace OpenAiEvaluation
{
    public class Evaluation : CompletionRequest
    {
        public Evaluation()
        {

        }
        public DateTime? RunDate { get; set; }
        public string ApiName { get; set; }
        public string Engine { get; set; }
        public int TokenCount { get; set; }
        public decimal Cost { get; set; }
        public string Response { get; set; }
        public TimeSpan ResponseTime { get; set; }
        public int? Rating { get; set; }

    }
}
