using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Entities.Entities
{
    public class Weapon
    {
        public int Range { get; protected set; }
        public int Damage { get; protected set; }
        public string Name { get; protected set; }
    }
}
