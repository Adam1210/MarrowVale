using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarrowVale.Business.Entities.Entities
{
    // This would most likely be a base class, and may want to make more enemy types that inherit this.
    public class Npc : GraphNode
    {
        public Npc()
        {
            IsAlive = true;
            Abilities = new List<Ability>();
            SpellBook = new List<Spell>();
            Items = new List<Item>();
            Relationships = new List<Relationship>();
            this.EntityLabel = "Character";
            this.Labels = new List<string>() { EntityLabel };
        }

        public Npc(IList<Dialogue> Dialogue, NpcRaceEnum Race, ClassEnum Class, string Name, string Description, int CurrentHealth, int MaxHealth, int BaseDamage, int age)
        {
            this.StartingDialogue = Dialogue;
            this.Race = Race;
            this.Class = Class;
            this.Name = Name;
            this.Description = Description;
            this.CurrentHealth = CurrentHealth;
            this.MaxHealth = MaxHealth;
            this.BaseDamage = BaseDamage;
            this.Age = age;
        }

        [JsonIgnore]
        public string FullName { get { return $"{this.Name} {this.LastName}";}}
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool IsAlive { get; set; }
        public string Gender { get; set; }
        public decimal FeelingTowardsPlayer { get; set; }
        public NpcTypeEnum Type { get; private set; }


        #region Purpose
        public string Objective { get; set; }
        public SentimentEnum Sentiment { get; set; }
        public OccupationEnum Occupation { get; set; }
        public string Religion { get; set; }
        public decimal Piousness { get; set; }
        #endregion

        #region Pysiology
        public int MaxHealth { get; set; }
        public int BaseDamage { get; set; }
        public int CurrentHealth { get; set; }
        public NpcRaceEnum Race { get; set; }
        public ClassEnum Class { get; set; }
        #endregion

        #region Personality
        public decimal Extraversion { get; set; }
        public decimal Conscientiousness { get; set; }
        public decimal Agreeableness { get; set; }
        public decimal Neuroticism { get; set; }
        public decimal Openness { get; set; }
        #endregion

        [JsonIgnore]
        public Armor Armor { get; set; }
        [JsonIgnore]
        public Weapon Weapon { get; set; }

        [JsonProperty]
        public bool Visible { get; private set; }
        [JsonIgnore]
        public IList<Relationship> Relationships { get; set; }

        public IList<Ability> Abilities { get; }
        public IList<Spell> SpellBook { get; }
        public IList<Item> Items { get; }

        private IList<Dialogue> StartingDialogue { get; }




        public void Show()
        {
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }

        public StringBuilder DescriptionPromptInput()
        {
            var input = new StringBuilder();
            //TODO: Give info based on vision/knowledge
            if (true)
            {
                input.Append($"Known Person: {FullName}\n");
                //input.Append($"Personality: {Sentiment.ToString()}\n");
                //input.Append($"Race: {Race.ToString()}\n");
                //input.Append($"Occupation: {Occupation.ToString()}\n");
                return input;
            }
        }

        public string CombatDescription()
        {
            return $"Enemy: {Name} | Weapon: {Weapon?.Name} Armor: {Armor?.Name}";
        }



        public void AddRelationship(Npc relationTo, RelationEnum relation)
        {
            var relationShip = new Relationship
            {
                Toward = relationTo,
                Relation = relation
            };

            if (relation == RelationEnum.Married)
            {
                if (!this.Relationships.Any(x => x.Relation == RelationEnum.Married))
                {
                    this.Relationships.Add(relationShip);
                    relationTo.AddRelationship(this, RelationEnum.Married);
                }
            }
            else if (relation == RelationEnum.Parent)
            {
                this.Relationships.Add(relationShip);
                relationTo.AddRelationship(this, RelationEnum.Child);
            }
            else
            {
                this.Relationships.Add(relationShip);
            }


        }



    }
}
