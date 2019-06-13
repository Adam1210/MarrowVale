using MarrowVale.Business.Entities.Enums;
using System.Collections.Generic;

namespace MarrowVale.Business.Entities.Entities
{
    public class Dialogue
    {
        public string Text { get; set; }
        public string Type { get; set; }
        public string TriggerText { get; set; }
        public bool AlreadySpoken { get; set; }
        public LanguageEnum Language { get; set; }
        public IList<Dialogue> Dialogues { get; set; }
    }
}
