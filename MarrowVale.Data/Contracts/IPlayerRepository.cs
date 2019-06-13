using MarrowVale.Business.Entities.Entities;
using System.Collections.Generic;

namespace MarrowVale.Data.Contracts
{
    public interface IPlayerRepository
    {
        IList<string> GetPlayers();
        void AddPlayer(Player player);
        Player GetPlayer(string playerName);
        void RemovePlayer(string playerName);
        int PlayerCount();
        void SavePlayers();
    }
}
