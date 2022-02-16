using MarrowVale.Common.Prompts.Contracts;
using MarrowVale.Common.Prompts.Examples;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Common.Prompts
{
    public class DivineInterventionPrompt : BasePrompt, INonStandardPrompt
    {
        public List<DivineInterventionExample> Examples { get; set; }

        public override List<StandardExample> StandardizeExamples()
        {
            var standardizedExamples = new List<StandardExample>();
            foreach (var example in Examples)
            {
                var standardExample = new StandardExample
                {
                    Id = example.Id,
                    Rating = example.Rating
                };

                standardExample.Output = example.Output;

                var input = new StringBuilder();
                input.AppendNewLine($"Action: {example.Action}");
                input.AppendNewLine($"Error: {example.Error}");
                standardExample.Input = input.ToString();

                standardizedExamples.Add(standardExample);
            }
            return standardizedExamples;
        }
    }


}
