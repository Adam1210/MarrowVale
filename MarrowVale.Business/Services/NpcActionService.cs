using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Services
{
    public class NpcActionService : INpcActionService
    {
        private readonly IAiService _aiService;
        private readonly IPromptService _promptService;
        private readonly IPrintService _printService;
        private readonly INpcRepository _npcRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IWorldContextService _worldContextService;

        public NpcActionService(IAiService aiService, IPromptService promptService, IPrintService printService, INpcRepository npcRepository,
                                IWorldContextService worldContextService, IPlayerRepository playerRepository)
        {
            _aiService = aiService;
            _promptService = promptService;
            _printService = printService;
            _npcRepository = npcRepository;
            _worldContextService = worldContextService;
            _playerRepository = playerRepository;
        }

        public void Accost()
        {
            throw new NotImplementedException();
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void GiveItem()
        {
            throw new NotImplementedException();
        }

        public void Leave()
        {
            throw new NotImplementedException();
        }

        public void PurchaseItem(Npc seller, Player player, string prompt = null, string conversation = null)
        {
            if (!string.IsNullOrWhiteSpace(prompt))
            {
                var item = _worldContextService.DirectObjectCommand<Item>(prompt, conversation, seller, CommandEnum.Purchase);

                if (item != null)
                {
                    _printService.Type($"Confirm purchase of {item.Name} for {item.BaseWorth} bronze");
                    if (_printService.ReadInput() == "Y")
                    {
                        _npcRepository.BuyItem(seller, player, item, item.BaseWorth);
                        Console.WriteLine("Purchase Complete");
                    }
                        
                }
                else
                {
                    PurchaseItem(seller, player);
                }
            }
            else
            {
                var items = _npcRepository.MerchantInventory(seller);
                if (items.Count() == 0 || items == null)
                    _printService.Type($"{seller?.FullName} does not have any sellable items in their inventory:");

                else
                {
                    _printService.Type($"{seller?.FullName} has the following goods for sale:");
                    foreach (var item in items)
                    {
                        _printService.Print($"{item.Name} : {item.BaseWorth} bronze");
                    }
                }

            }
        }

        public void SellItem(Npc merchant, Player player, string prompt = null, string conversation = null)
        {
            if (!string.IsNullOrWhiteSpace(prompt))
            {
                var item = _worldContextService.DirectObjectCommand<Item>(prompt, conversation, player, CommandEnum.Purchase);

                if (item != null)
                {
                    _printService.Type($"Confirm sale of {item.Name} for {item.BaseWorth} bronze");
                    if (_printService.ReadInput() == "Y")
                    {
                        _npcRepository.SellItem(merchant, player, item, item.BaseWorth);
                        Console.WriteLine("Purchase Complete");
                    }

                }
                else
                {
                    PurchaseItem(merchant, player);
                }
            }
            else
            {
                var items = _playerRepository.GetInventory(player).Items.ToList();
                if (items.Count == 0 || items == null)
                    _printService.Type($"You do not have any sellable items in your inventory:");
                else
                {
                    _printService.Type($"You have the following items in your inventory:");
                    foreach (var item in items)
                    {
                        _printService.Print($"{item.Name} : {item.BaseWorth} bronze");
                    }
                }

            }     
        }

        public void TalkTo()
        {
            throw new NotImplementedException();
        }
    }
}
