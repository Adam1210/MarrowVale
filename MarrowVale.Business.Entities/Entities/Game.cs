using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Entities.Entities
{
    public class Game
    {
        public Game()
        {

        }

        public Location CurrentLocation { get; private set; }
        public string TestDescription { get; set; }
    }
}
