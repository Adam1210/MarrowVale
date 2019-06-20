using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Entities.Entities
{
    public class Game
    {
        public Game()
        {
            GameTime = new Time();
        }

        [JsonConstructor]
        private Game(Time GameTime, Location CurrentLocation)
        {
            this.GameTime = GameTime;
            this.CurrentLocation = CurrentLocation;
        }

        public Location CurrentLocation { get; private set; }
        public Time GameTime { get; set; }
    }
}
