using MarrowVale.Business.Entities.Entities;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Common.Evaluator
{
    public class OpenAiSettings : GraphNode
    {
        public OpenAiSettings()
        {
            this.EntityLabel = "SubPromptType";
            this.Labels = new List<string>() { EntityLabel };
        }
        public OpenAiSettings(int? max_tokens = null, double? temperature = null, string engineName = null, double? top_p = null, int? numOutputs = null, double? presencePenalty = null, double? frequencyPenalty = null, int? logProbs = null, bool? echo = null, params string[] stopSequences)
        {
            this.MaxTokens = max_tokens;
            this.Temperature = temperature;
            this.Echo = echo;
            this.TopP = top_p;
            this.Logprobs = logProbs;
            this.FrequencyPenalty = frequencyPenalty;
            this.PresencePenalty = presencePenalty;
            this.EngineName = engineName ?? Engine.Babbage;
            //this.MultipleStopSequences = stopSequences;
        }

        public bool? Echo { get; set; }
        public int? Logprobs { get; set; }
        public double? FrequencyPenalty { get; set; }
        public string[] MultipleStopSequences { get; set; }
        public double? Temperature { get; set; }
        public double? TopP { get; set; }
        public int? MaxTokens { get; set; }
        //public string[] MultiplePrompts { get; set; }
        public double? PresencePenalty { get; set; }
        public string StopSequence { get; set; }
        public string EngineName { get; set; }
    }
}
