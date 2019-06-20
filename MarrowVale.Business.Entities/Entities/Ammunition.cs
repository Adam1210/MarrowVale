namespace MarrowVale.Business.Entities.Entities
{
    public class Ammunition : IItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public bool IsVisible { get; set; }
        public int BaseWorth { get; set; }

        public string EnvironmentalDescription { get; private set; }
        
        public string GetDescription()
        {
            // Add the logic to build this description.
            return Description;
        }

        public override string ToString()
        {
            return $"{Name} - {GetDescription()}";
        }
    }
}
