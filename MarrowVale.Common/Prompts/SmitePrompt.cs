using MarrowVale.Common.Prompts.Contracts;
using MarrowVale.Common.Prompts.Examples;
using System.Collections.Generic;

namespace MarrowVale.Common.Prompts
{
    public class SmitePrompt : BasePrompt, INonStandardPrompt
    {
        public List<SmiteExample> Examples { get; set; }

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

                standardExample.Input = $"Deity: {example.Deity}\nCharacter: {example.Enemy.Name} | Armor: {example.Enemy.Armor}";
                standardExample.Output = example.Output;

                standardizedExamples.Add(standardExample);
            }
            return standardizedExamples;
        }
    }


}
