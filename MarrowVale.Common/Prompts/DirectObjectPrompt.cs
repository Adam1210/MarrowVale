using MarrowVale.Common.Prompts.Contracts;
using MarrowVale.Common.Prompts.Examples;
using System.Collections.Generic;

namespace MarrowVale.Common.Prompts
{
    public class DirectObjectPrompt : BasePrompt, INonStandardPrompt
    {
        public List<StandardExample> Examples { get; set; }

        public override List<StandardExample> StandardizeExamples()
        {
            return Examples;
        }
    }


}
