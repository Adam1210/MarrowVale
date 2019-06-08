
using Newtonsoft.Json;
using System.ComponentModel;

namespace MarrowVale.Business.Entities.Entities
{
    public class Ammunition : IItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public bool IsVisible { get; set; }
        public int BaseWorth { get; set; }
    }
}
