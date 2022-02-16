using MarrowVale.Common.Prompts.Contracts;
using MarrowVale.Common.Prompts.Examples;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Common.Prompts
{
    public class DialogueSummaryPrompt : BasePrompt, INonStandardPrompt
    {
        public List<DialogueSummaryExample> Examples { get; set; }

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

                StringBuilder input = new StringBuilder();
                input.Append($"{example.Context}\n");

                var isfirstPerson = true;
                foreach (var line in example.Dialogue)
                {
                    if (isfirstPerson)
                        input.Append($"{example.Person1}:{line}\n");
                    else
                        input.Append($"{example.Person2}:{line}\n");
                    isfirstPerson = !isfirstPerson;
                }
                standardExample.Input = input.ToString();
                standardExample.Output = example.Summary;

                standardizedExamples.Add(standardExample);
            }
            return standardizedExamples;
        }
    }


}
