using MarrowVale.Common.Prompts;
using MarrowVale.Common.Prompts.Contracts;
using MarrowVale.Common.Prompts.Examples;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MarrowVale.Business.Entities.Prompts
{
    public class DialoguePrompt : BasePrompt, INonStandardPrompt
    {
        public List<DialogueExample> Examples { get; set; }

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
                standardExample.Output = $"{example.Person2}:{example.Dialogue.Last()}";
                example.Dialogue.RemoveAt(example.Dialogue.Count-1);

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

                input.Remove(input.Length-1,1);

                standardExample.Input = input.ToString();
                standardizedExamples.Add(standardExample);
            }
            return standardizedExamples;
        }
    }


}
