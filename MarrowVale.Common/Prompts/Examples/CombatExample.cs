using System.Collections.Generic;

namespace MarrowVale.Common.Prompts.Examples
{
    public class CombatExample : BaseExample
    {
        public PlayerExampleDto Player { get; set; }
        public EnemyExampleDto Enemy { get; set; }
        public string AttemptedAction { get; set; }
        public int Damage { get; set; }
        public string Output { get; set; }
        public List<string> Effects { get; set; }
    }



    public class PlayerExampleDto
    {
        public string Name { get; set; }
        public int FullHealth { get; set; }
        public int CurrentHealth { get; set; }
        public string Armor { get; set; }
        public string Weapon { get; set; }
    }


    public class EnemyExampleDto
    {
        public string Name { get; set; }
        public int FullHealth { get; set; }
        public int CurrentHealth { get; set; }
        public string Armor { get; set; }
        public string Weapon { get; set; }
    }



}
