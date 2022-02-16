using MarrowVale.Common.Prompts.Contracts;
using MarrowVale.Common.Prompts.Examples;
using System.Collections.Generic;

namespace MarrowVale.Common.Prompts
{
    public class SubjectVerbObjectPrompt : BasePrompt, INonStandardPrompt
    {
        public List<SubjectVerbObjectExample> Examples { get; set; }

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
                standardExample.Output = $"Subject: {example.Subject} | Verb:{example.Verb} | Object: {example.Object}";
                standardExample.Input = example.Input;

                standardizedExamples.Add(standardExample);
            }
            return standardizedExamples;
        }
    }


}
