using MarrowVale.Common.Prompts.Examples;
using System.Collections.Generic;


namespace MarrowVale.Common.Prompts
{
    public abstract class BasePrompt
    {
        public string Description { get; set; }
        public string InputPrefix { get; set; }
        public string OutputPrefix { get; set; }
        public int? UseExampleCount { get; set; }
        public bool UseDescription { get; set; }

        public StandardPrompt Standardize()
        {
            var generalPrompt = new StandardPrompt
            {
                Description = this.Description,
                InputPrefix = this.InputPrefix,
                OutputPrefix = this.OutputPrefix,
                UseDescription = this.UseDescription,
                UseExampleCount = this.UseExampleCount,
                Examples = StandardizeExamples()
            };
            return generalPrompt;
        }

        public abstract List<StandardExample> StandardizeExamples();

    }
}
