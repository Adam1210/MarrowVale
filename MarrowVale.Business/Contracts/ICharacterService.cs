using MarrowVale.Business.Entities.Dtos;
using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Contracts
{
    public interface ICharacterService
    {
        void NewCharacter(GameDto gameDto);
        void LoadCharacter(GameDto gameDto);
        Inventory GetInventory(Player player);
    }
}
