using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Entities.Dtos
{
    public class GameDto
    {
        public Player Player { get; set; }
        public Inventory Inventory { get; set; }
        public Location CurrentLocation { get; set; }

        public GameDto()
        {
            Player = new Player();
            Inventory = new Inventory();
            CurrentLocation = new Location();
        }
    }
}
