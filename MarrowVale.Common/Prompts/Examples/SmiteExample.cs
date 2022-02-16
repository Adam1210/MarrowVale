using System.Collections.Generic;

namespace MarrowVale.Common.Prompts.Examples
{
    public class SmiteExample : BaseExample
    {
        public DietyExampleDto Deity { get; set; }
        public EnemyExampleDto Enemy { get; set; }
        public string RequestedSmite { get; set; }
        public int Damage { get; set; }
        public string Output { get; set; }
        public List<string> Effects { get; set; }
    }



    public class DietyExampleDto
    {
        public string Name { get; set; }
    }


    public class NpcExampleDto
    {
        public string Name { get; set; }
        public int FullHealth { get; set; }
        public int CurrentHealth { get; set; }
        public string Armor { get; set; }
        public string Weapon { get; set; }
    }



}
