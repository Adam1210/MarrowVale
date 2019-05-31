using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Entities.Entities
{
    public class Player
    {
        public string Name { get; set; }
        public Class Class { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public Inventory Inventory { get; set; }
    }
}
