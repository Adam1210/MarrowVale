using MarrowVale.Business.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarrowVale.Business.Entities.Entities
{
    // This would most likely be a base class, and may want to make more enemy types that inherit this.
    public class Npc
    {
        public Npc()
        {
            Abilities = new List<Ability>();
            SpellBook = new List<Spell>();
            Items = new List<IItem>();
        }

        public Npc(Dialogue Dialogue): this()
        {
            this.StartingDialogue = Dialogue;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; }
        public int BaseDamage { get; }

        public IList<Ability> Abilities { get; }
        public IList<Spell> SpellBook { get; }
        public IList<IItem> Items { get; }

        private Dialogue StartingDialogue { get; }
        private Dialogue CurrentDialogue { get; set; }
               
        public NpcTypeEnum Type { get; private set; }

        public string Speak(string responseText = null)
        {
            if(string.IsNullOrWhiteSpace(responseText) && CurrentDialogue == StartingDialogue)
            {
                return StartingDialogue.Text;
            }

            var nextDialogue = CurrentDialogue.Dialogues.FirstOrDefault(x => x.TriggerText.Equals(responseText,StringComparison.CurrentCultureIgnoreCase));

            if(nextDialogue != null)
            {
                CurrentDialogue = nextDialogue;
                return nextDialogue.Text;
            }

            return $"I don't understand try again.{Environment.NewLine}{CurrentDialogue.Text}";
        }
    }
}
