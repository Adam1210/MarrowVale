using MarrowVale.Common.Prompts.Examples;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarrowVale.Common.Prompts
{
    public class StandardPrompt
    {
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Input { get; set; }
        public string Description { get; set; }
        public string InputPrefix { get; set; }
        public string OutputPrefix { get; set; }
        public bool UseInPutPrefixForExamples { get; set; }
        public bool UseOutPutPrefixForExamples { get; set; }
        public int? UseExampleCount { get; set; }
        public bool UseDescription { get; set; }
        public List<StandardExample> Examples { get; set; }


        public override string ToString()
        {
            var prompt = new StringBuilder();
            if (UseDescription)
                prompt.Append($"{Description}\n");

            var examples = Examples.OrderByDescending(x => x.Rating).Take(UseExampleCount ?? Examples.Count);

            foreach (var example in examples)
            {
                prompt.Append($"{(UseInPutPrefixForExamples ? InputPrefix : "")}{example.Input}\n");
                prompt.Append($"{(UseOutPutPrefixForExamples ? OutputPrefix : "")}{example.Output}\n\n");
            }
            prompt.Append($"{InputPrefix}{Input}");
            prompt.Append($"\n{OutputPrefix}");

            return prompt.ToString();
        }
    }
}
