using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using System.Collections.Generic;
using Xunit;

namespace MarrowVale.Business.Tests
{
    public class NpcTests
    {


        public NpcTests()
        {

        }

        private Npc CreateNpc()
        {
            var newNpc = new Npc(CreateDialogue(), NpcRaceEnum.Human, ClassEnum.Warrior, "Bob", "A simple villager. Looks to somewhat poor and dirty.", 10, 10, 2);


            return newNpc;
        }

        private IList<Dialogue> CreateDialogue()
        {
            var dialogues = new List<Dialogue>();
            var newDialogue = new Dialogue();

            newDialogue.Text = "Greetings traveler, how are you this day?";
            newDialogue.TriggerText = "Greeting";
            newDialogue.Dialogues.Add(new Dialogue
            {
                Text = "",
                TriggerText = "",
            });

            dialogues.Add(newDialogue);

            return dialogues;
        }

        [Fact]
        public void GetGreetingText()
        {
            var npc = CreateNpc();

            var text = npc.Speak("Greeting");

            Assert.True(text == "Greetings traveler, how are you this day?");
        }
    }
}
