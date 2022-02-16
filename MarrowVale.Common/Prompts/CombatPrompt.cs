using MarrowVale.Common.Prompts.Contracts;
using MarrowVale.Common.Prompts.Examples;
using System.Collections.Generic;

namespace MarrowVale.Common.Prompts
{
    public class CombatPrompt : BasePrompt, INonStandardPrompt
    {
        public List<CombatExample> Examples { get; set; }

        public override List<StandardExample> StandardizeExamples()
        {
            var standardizedExamples = new List<StandardExample>();
            foreach (var example in Examples)
            {
                var standardExample = new StandardExample
                {
                    Id = example.Id,
                    Rating = example.Rating
                };
                var currentHealth = $"{example.Enemy.CurrentHealth}/{example.Enemy.FullHealth}";
                standardExample.Input = $"Player: {example.Player.Name} | Weapon:{example.Player.Weapon} | Armor:{example.Player.Armor}\nEnemy: {example.Enemy.Name} | Armor: {example.Enemy.Armor} | Weapon: {example.Enemy.Weapon}\nAttempted Action: {example.AttemptedAction}";
                standardExample.Output = example.Output;

                standardizedExamples.Add(standardExample);
            }
            return standardizedExamples;
        }
    }


}
