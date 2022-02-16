using System.Collections.Generic;

namespace MarrowVale.Common.Prompts.Examples
{
    public class DialogueExample : BaseExample
    {
        public string Context { get; set; }
        public string Person1 { get; set; }
        public string Person2 { get; set; }
        public List<string> Dialogue { get; set; }
    }
}
