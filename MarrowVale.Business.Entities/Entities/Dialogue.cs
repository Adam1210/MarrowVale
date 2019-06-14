using MarrowVale.Business.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarrowVale.Business.Entities.Entities
{
    public class Dialogue
    {
        public Dialogue()
        {
            Dialogues = new List<Dialogue>();
        }

        public string Text { get; set; }
        public DialogueTypeEnum Type { get; set; }
        public string TriggerText { get; set; }
        public bool AlreadySpoken { get; set; }
        public LanguageEnum Language { get; set; }
        public IList<Dialogue> Dialogues { get; set; }

        public string GetTriggerText(IList<LanguageEnum> KnownLanguages)
        {
            return string.Join($"{Environment.NewLine}",Dialogues.Where(x => KnownLanguages.Contains(x.Language)).Select(x=>x.TriggerText));
        }

        public string GetOptionText()
        {
            return $"({Language}) {TriggerText}";
        }
    }
}
