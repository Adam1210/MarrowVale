using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarrowVale.Business.Entities.Entities
{
    // This would most likely be a base class, and may want to make more enemy types that inherit this.
    public class Npc
    {
        private Npc()
        {
            Abilities = new List<Ability>();
            SpellBook = new List<Spell>();
            Items = new List<IItem>();
        }

        [JsonConstructor]
        public Npc(IList<Dialogue> Dialogue, NpcRaceEnum Race, ClassEnum Class, string Name, string Description): this()
        {
            this.StartingDialogue = Dialogue;
            this.Race = Race;
            this.Class = Class;
            this.Name = Name;
            this.Description = Description;
        }

        //ToDo: add a way to track players status with the npc (hostile, neutral, friend)

        public string Name { get; }
        public string Description { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; }
        public int BaseDamage { get; }
        public bool Visible { get; private set; }

        public IList<Ability> Abilities { get; }
        public IList<Spell> SpellBook { get; }
        public IList<IItem> Items { get; }

        private IList<Dialogue> StartingDialogue { get; }
        private Dialogue CurrentDialogue { get; set; }
               
        public NpcTypeEnum Type { get; private set; }
        public NpcRaceEnum Race { get; }
        public ClassEnum Class { get; }

        public string Speak(string responseText = null)
        {
            if (string.IsNullOrWhiteSpace(responseText))
            {
                return "Do you need something?";
            }

            if(CurrentDialogue == null)
            {
                var startDialogue = StartingDialogue.FirstOrDefault(x => x.TriggerText.Equals(responseText, StringComparison.CurrentCultureIgnoreCase));

                CurrentDialogue = startDialogue;
                return startDialogue.Text;
            }

            var nextDialogue = CurrentDialogue.Dialogues.FirstOrDefault(x => x.TriggerText.Equals(responseText,StringComparison.CurrentCultureIgnoreCase));

            if(nextDialogue != null)
            {
                CurrentDialogue = nextDialogue;
                return nextDialogue.Text;
            }

            return $"I don't understand try again.{Environment.NewLine}{CurrentDialogue.Text}";
        } 
        
        public string GetOptionsText(IList<LanguageEnum> KnownLanguages)
        {
            return CurrentDialogue.GetTriggerText(KnownLanguages);
        }
    }
}
