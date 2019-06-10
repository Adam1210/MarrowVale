using MarrowVale.Business.Entities.Dtos;
using MarrowVale.Business.Entities.Entities;

namespace MarrowVale.Business.Contracts
{
    public interface ICharacterService
    {
        Player NewCharacter(PlayerDto gameDto);
        Player LoadCharacter();
        Inventory GetInventory(Player player);
    }
}
