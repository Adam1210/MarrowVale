using MarrowVale.Business.Entities.Entities;
using System;
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
            var newNpc = new Npc(CreateDialogue());


            return newNpc;
        }

        private IList<Dialogue> CreateDialogue()
        {
            var dialogues = new List<Dialogue>();
            var newDialogue = new Dialogue();

            newDialogue.Text = "Greetings traveler, how are you this day";
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
        public void Test1()
        {

        }
    }
}
