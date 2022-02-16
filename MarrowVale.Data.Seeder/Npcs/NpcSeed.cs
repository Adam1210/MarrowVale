using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Data.Seeder.DialogueSeeds;
using System;

namespace MarrowVale.Data.Seeder.Npcs
{
    public class NpcSeed
    {
        public Tuple<Npc,Npc> GetOriginHumans()
        {
            var adam = new Npc
            {
                Id = Guid.NewGuid().ToString(),
                Race = NpcRaceEnum.Human,
                Class = ClassEnum.Warrior,
                Name = "Adam",
                LastName = "Dosto",
                Description = "Man of the red clay",
                CurrentHealth = 100,
                MaxHealth = 100,
                Age = 105,
                IsAlive = true,
                Gender = "Male",
                Sentiment = SentimentEnum.Excited,
                Occupation = OccupationEnum.Duke,
                Objective = "In search of Adventurer's to defend the town from nightly goblin raids."
            };

            var eve = new Npc
            {
                Id = Guid.NewGuid().ToString(),
                Race = NpcRaceEnum.Human,
                Class = ClassEnum.Warrior,
                Name = "Eve",
                LastName = "Dosto",
                Description = "First Woman",
                CurrentHealth = 100,
                MaxHealth = 100,
                Age = 107,
                IsAlive = true,
                Gender = "Female",
            };
            adam.AddRelationship(eve, RelationEnum.Married);
            return Tuple.Create(adam, eve);
        }


        public Tuple<Npc, Npc> GetOriginElfs()
        {
            var adam = new Npc
            {
                Race = NpcRaceEnum.Elf,
                Class = ClassEnum.Ranger,
                Name = "Adma",
                LastName = "Lotherian",
                Description = "First Elf man",
                CurrentHealth = 100,
                MaxHealth = 100,
                Age = 212,
                IsAlive = false,
                Gender = "Male",
            };

            var eve = new Npc
            {
                Race = NpcRaceEnum.Elf,
                Class = ClassEnum.Ranger,
                Name = "Evelyn",
                LastName = "Lotherian",
                Description = "First Elf Woman",
                CurrentHealth = 100,
                MaxHealth = 100,
                Age = 215,
                IsAlive = false,
                Gender = "Female",
            };
            adam.AddRelationship(eve, RelationEnum.Married);
            return Tuple.Create(adam, eve);
        }

        public Tuple<Npc, Npc> GetOriginGoblin()
        {
            var adam = new Npc
            {
                Race = NpcRaceEnum.Goblin,
                Class = ClassEnum.Mage,
                Name = "Gorang",
                LastName = "Vo",
                Description = "Gorang gorang gorang",
                CurrentHealth = 100,
                MaxHealth = 100,
                Age = 212,
                IsAlive = false,
                Gender = "Male",
            };

            var eve = new Npc
            {
                Race = NpcRaceEnum.Goblin,
                Class = ClassEnum.Mage,
                Name = "Gogo",
                LastName = "Vogo",
                Description = "Gogo love smash",
                CurrentHealth = 100,
                MaxHealth = 100,
                Age = 215,
                IsAlive = false,
                Gender = "Female",
            };
            adam.AddRelationship(eve, RelationEnum.Married);
            return Tuple.Create(adam, eve);
        }

    }
}
