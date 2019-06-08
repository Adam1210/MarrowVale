using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Entities.Dtos
{
    public class GameDto
    {
        public PlayerDto Player { get; set; }
        public Inventory Inventory { get; set; }
        public Location CurrentLocation { get; set; }

        public GameDto()
        {
            Player = new PlayerDto();
            Inventory = new Inventory();
            CurrentLocation = new Location();
        }
    }
}
