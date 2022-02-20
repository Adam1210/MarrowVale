using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MarrowVale.Common.Models;
using OpenAI_API;

namespace MarrowVale.Business.Entities.Entities
{
    public class Evaluation : GraphNode
    {
        public Evaluation()
        {
            this.EntityLabel = "Evaluation";
            this.Labels = new List<string>() { EntityLabel };
        }

        public DateTime? RunDate { get; set; }
        public string ApiName { get; set; }
        public string Engine { get; set; }
        public int TokenCount { get; set; }
        public decimal Cost { get; set; }
        public string Response { get; set; }
        public TimeSpan ResponseTime { get; set; }
        public int? Rating { get; set; }
        public object CompiledStop { get; }
        public bool? Echo { get; set; }
        public int? Logprobs { get; set; }
        public bool Stream { get; }
        public int? NumChoicesPerPrompt { get; set; }
        public double? FrequencyPenalty { get; set; }
        public double? TopP { get; set; }
        [JsonIgnore]
        public string[] MultipleStopSequences { get; set; }
        public double? Temperature { get; set; }
        public int? MaxTokens { get; set; }
        [JsonIgnore]
        public string Prompt { get; set; }
        [JsonIgnore]
        public string[] MultiplePrompts { get; set; }
        public object CompiledPrompt { get; }
        public double? PresencePenalty { get; set; }
        public string StopSequence { get; set; }

    }


}
