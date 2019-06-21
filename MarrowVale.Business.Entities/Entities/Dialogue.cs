using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarrowVale.Business.Entities.Entities
{
    public class Dialogue
    {
        [JsonConstructor]
        public Dialogue(string Text, DialogueTypeEnum Type, string TriggerText, LanguageEnum Language, List<Dialogue> Dialogues, bool AlreadySpoken = false)
        {
            this.Text = Text;
            this.Type = Type;
            this.TriggerText = TriggerText;
            this.Language = Language;
            this.Dialogues = Dialogues ?? new List<Dialogue>();
            this.AlreadySpoken = AlreadySpoken;
        }
            
        public string Text { get; }
        public DialogueTypeEnum Type { get; set; }
        public string TriggerText { get;  }
        public bool AlreadySpoken { get; private set; }
        public LanguageEnum Language { get;  }
        private IList<Dialogue> Dialogues { get; set; }

        public string GetTriggerText(IList<LanguageEnum> KnownLanguages)
        {
            return string.Join($"{Environment.NewLine}",Dialogues.Where(x => KnownLanguages.Contains(x.Language)).Select(x=>x.GetOptionText()));
        }

        public Dialogue GetResponse(string triggerText)
        {
            return Dialogues.FirstOrDefault(x => x.TriggerText.Equals(triggerText, StringComparison.CurrentCultureIgnoreCase));
        }

        public string GetOptionText()
        {
            return $"({Language}) {TriggerText}";
        }
    }
}
