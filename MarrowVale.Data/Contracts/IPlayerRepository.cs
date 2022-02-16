using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarrowVale.Data.Contracts
{
    public interface IPlayerRepository : IBaseRepository<Player>
    {
        Task CreatePlayer(Player player);
        void MovePlayer(Player player, string currentLocationId, string newLocationId);
        IList<string> PlayerLocationType(Player player);
        Location GetPlayerLocation(Player player);
        Player GetPlayerWithInventory(string playerId);
        Inventory GetInventory(Player player);
    }
}
