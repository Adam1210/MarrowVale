using System;
using System.Collections.Generic;
using MarrowVale.Business.Entities.Entities;

namespace MarrowVale.Data.Seeder.DialogueSeeds
{
    public static class GeneralDialogueSeed
    {
        private static IList<Dialogue> GetDialogues()
        {
            var listOfDialogues = new List<Dialogue>();

            var greetingDialogue = new Dialogue();

            listOfDialogues.Add(greetingDialogue);

            return listOfDialogues;
        }
    }
}
