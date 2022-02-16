using MarrowVale.Common.Prompts.Contracts;
using MarrowVale.Common.Prompts.Examples;
using System.Collections.Generic;

namespace MarrowVale.Common.Prompts
{
    public class TextToCommandPrompt : BasePrompt, INonStandardPrompt
    {
        public List<TextToCommandExample> Examples { get; set; }

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

                standardExample.Output = example.Command;
                standardExample.Input = example.Example;
                standardizedExamples.Add(standardExample);
            }
            return standardizedExamples;
        }
    }


}
