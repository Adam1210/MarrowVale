using System;
using System.Collections.Generic;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;

namespace MarrowVale.Data.Seeder.DialogueSeeds
{
    public static class GeneralDialogueSeed
    {
        public static IList<Dialogue> GetDialogues()
        {
            var listOfDialogues = new List<Dialogue>();

            var greetingDialogue = new Dialogue("Greetings!",DialogueTypeEnum.Friendly,"Greeting",LanguageEnum.Common, new List<Dialogue>());

            listOfDialogues.Add(greetingDialogue);

            return listOfDialogues;
        }
    }
}
