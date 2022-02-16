using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI_API;

namespace OpenAiEvaluation
{
    public class Evaluation
    {
        public Evaluation()
        {

        }

        public string ApiName { get; set; }
        public string Engine { get; set; }
        public int TokenCount { get; set; }
        public decimal Cost { get; set; }
        public string Prompt { get; set; }
        public string Response { get; set; }
        public string ResponseTime { get; set; }
        public int Rating { get; set; }


        public string MaxResponseLength { get; set; }
        public string TopP { get; set; }
        public string FrequencyPenalty { get; set; }
        public string PrecensePenalty { get; set; }
        public string BestOf { get; set; }
        public string StopSequences { get; set; }
    }
}
