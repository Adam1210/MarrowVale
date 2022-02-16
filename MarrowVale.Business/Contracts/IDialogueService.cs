using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Contracts
{
    public interface IDialogueService
    {
        void Talk(Player player, Npc npc);
    }
}
