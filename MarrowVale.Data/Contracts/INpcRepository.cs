using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Entities.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Data.Contracts
{
    public interface INpcRepository : IBaseRepository<Npc>
    {
        Npc GetNpcByName(string firstName);
        bool IsPlayerNearby(Player player, Npc npc);
        Conversation MemoryofPlayer(Npc npc);
        void CreateMemory(Npc c, string s);
        IEnumerable<BaseRelationship> AllMemories(Npc npc);
        IEnumerable<Item> MerchantInventory(Npc npc);
        Item BuyItem(Npc npc, Player player, Item item, int price, int quantity = 1);
        Item SellItem(Npc npc, Player player, Item item, int price, int quantity = 1);
        GraphNode AllKnowledge(Npc npc);
        void SetCombatEquipment(Npc npc);
        void SaveCombatEquipment(Npc npc);


    }
}
