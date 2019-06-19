using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Data.Seeder.DialogueSeeds;

namespace MarrowVale.Data.Seeder.Npcs
{
    public static class VillagersSeed
    {
        public static Npc GetVillager()
        {
            var dialogues = GeneralDialogueSeed.GetDialogues();
            var villager = new Npc(dialogues, NpcRaceEnum.Human, ClassEnum.Warrior, "Bob", "A simple villager. Looks to somewhat poor and dirty.");

            return villager;
        }
    }
}
