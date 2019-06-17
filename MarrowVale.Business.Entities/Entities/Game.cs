using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Entities.Entities
{
    public class Game
    {
        public Game()
        {
            GameTime = new Time()
            {
                TimeOfDay = 14,
                DaysElapsed = 0
            };
        }

        public Location CurrentLocation { get; private set; }
        public string TestDescription { get; set; }
        public Time GameTime { get; set; }
    }
}
