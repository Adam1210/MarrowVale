using MarrowVale.Business.Entities.Entities;
using System;
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

        private Dialogue CreateDialogue()
        {
            var newDialogue = new Dialogue();

            newDialogue.Text = "Greetings traveler, how are you this day";
            newDialogue.Dialogues.Add(new Dialogue
            {
                Text = "",
                TriggerText = "",
            });

            return newDialogue;
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
