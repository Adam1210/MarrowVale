using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Contracts
{
    public interface INpcActionService
    {
        void PurchaseItem(Npc seller, Player player, string prompt = null, string conversation = null);
        void SellItem(Npc merchant, Player player, string prompt = null, string conversation = null);
        void GiveItem();
        void Attack();
        void Leave();
        void TalkTo();
        void Accost();
    }
}
