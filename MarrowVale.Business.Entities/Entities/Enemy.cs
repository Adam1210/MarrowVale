using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Entities.Entities
{
    // This would most likely be a base class, and may want to make more enemy types that inherit this.
    public class Enemy
    {
        public string Name { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
    }
}
