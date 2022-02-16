using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Entities.Graph;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using Neo4jClient;
using Neo4jClient.ApiModels.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Data.Repositories
{
    public class NpcRepository : BaseRepository<Npc>, INpcRepository
    {
        public NpcRepository(IGraphClient graphClient) : base(graphClient)
        {

        }

        public Npc GetNpcByName(string firstName)
        {
            return Single(x => x.Name == firstName).Result;
        }

        public bool IsPlayerNearby(Player player, Npc npc)
        {
            if (npc == null || player == null)
                return false;

            return RelatedToAndFrom<Room, Player, GraphRelationship>(x => x.Id == npc.Id, z => true, y => y.Id == player.Id, new GraphRelationship("INSIDE"), new GraphRelationship("AT"), relation2Out: false).ResultsAsync.Result.Any();

        }

        public Conversation MemoryofPlayer(Npc npc)
        {
            return _graphClient.Cypher
                        .Match("(x:Character)-[:REMEMBERS]-(z:Conversation)-[:MENTIONS]-(y:Player)")
                        .Where((Npc x) => x.Id == npc.Id)
                        .Return(z => z.As<Conversation>())
                        .ResultsAsync.Result.FirstOrDefault();
        }

        public IEnumerable<BaseRelationship> AllMemories(Npc npc)
        {
            return _graphClient.Cypher
                        .Match("(x:Character)-[r:REMEMBERS]-(z)")
                        .With("{Relationship: type(r), Node: z} as objectNode")
                        .Where((Npc x) => x.Id == npc.Id)
                        .Return(objectNode => objectNode.CollectAs<BaseRelationship>()).ResultsAsync.Result.FirstOrDefault();
        }

        public GraphNode AllKnowledge(Npc npc)
        {
            var knowledge = _graphClient.Cypher
                        .Match("(x:Character)-[r]->(item)")
                        .Where((Npc x) => x.Id == npc.Id)
                        .With("{Id: x.Id, Name: x.Name, Relations: collect({Relation: type(r), Node: item})} as characterNode")
                        .Return(characterNode => characterNode.As<GraphNode>()).ResultsAsync.Result.FirstOrDefault();

            //var initialKnowledge = _graphClient.Cypher
            //            .Match("(x:Character)-[r]->(item)")
            //            .OptionalMatch("(item)-[:PARTOF]->(subItem)")
            //            .With("x,{Relation: type(r), Node: item, Labels: labels(item)} as relations")
            //            .Where((Npc x) => x.Id == npc.Id)
            //            .Return((x, relations) => new GraphNodeData
            //            {
            //                Data = x.As<GraphNode>(),
            //                Relations = relations.CollectAs<BaseRelationship>(),

            //            }).ResultsAsync.Result.FirstOrDefault();

            foreach (var k in knowledge.Relations)
            {
                var metaKnowledge = _graphClient.Cypher
                        .Match("(item)-[r:PARTOF]->(subItem)")
                        .Where((GraphNode item) => item.Id == k.Node.Id)
                        .With("{Relation: type(r), Node: subItem} as test")
                        .Return(test => test.As<BaseRelationship>()).ResultsAsync.Result;
                k.Node.Relations = k.Node.Relations.Concat(metaKnowledge);
            }


            return knowledge;
        }

        public IEnumerable<Item> MerchantInventory(Npc npc)
        {
            var test = AllKnowledge(npc);

            return _graphClient.Cypher
                        .Match("(x:Character)-[r:SELLS]->(z)-[]->(item:Item)")
                        .Where((Npc x) => x.Id == npc.Id)
                        .Return(item => item.As<Item>()).ResultsAsync.Result;
        }

        public Item BuyItem(Npc npc, Player player, Item item, int price, int quantity = 1)
        {
            return _graphClient.Cypher
                .Match("(seller:Character)-[r1:SELLS]->(sellerInventory:Inventory)-[r2:PARTOF]->(tradedItem:Item),(p:Player)-[r3:OWNS]->(playerInventory:Inventory)")
                .Where((Player p) => p.Id == player.Id)
                .AndWhere((Npc seller) => seller.Id == npc.Id)
                .AndWhere((Item tradedItem) => tradedItem.Id == item.Id)
                .Set($"playerInventory.CurrentCurrency = playerInventory.CurrentCurrency - {price}")
                .Set($"sellerInventory.CurrentCurrency = sellerInventory.CurrentCurrency + {price}")
                .Create("(playerInventory)-[:PARTOF]->(tradedItem)")
                .Delete("r2")
                .Return(tradedItem => tradedItem.As<Item>()).ResultsAsync.Result.FirstOrDefault();
        }

        public Item SellItem(Npc npc, Player player, Item item, int price, int quantity = 1)
        {
            return _graphClient.Cypher
                .Match("(p:Player)-[r1:OWNS]->(playerInventory:Inventory)-[r2:PARTOF]->(tradedItem:Item),(merchant:Character)-[r3:SELLS]->(merchantInventory:Inventory)")
                .Where((Player p) => p.Id == player.Id)
                .AndWhere((Npc merchant) => merchant.Id == npc.Id)
                .AndWhere((Item tradedItem) => tradedItem.Id == item.Id)
                .Set($"playerInventory.CurrentCurrency = playerInventory.CurrentCurrency + {price}")
                .Set($"merchantInventory.CurrentCurrency = merchantInventory.CurrentCurrency - {price}")
                .Create("(merchantInventory)-[:PARTOF]->(tradedItem)")
                .Delete("r2")
                .Return(tradedItem => tradedItem.As<Item>()).ResultsAsync.Result.FirstOrDefault();
        }

        public void CreateMemory(Npc c, string s)
        {
            var conversation = new Conversation { Id = Guid.NewGuid().ToString(), Summary = s };

            _graphClient.Cypher
                .Create("(conversation:Conversation $x)")
                .WithParam("x", conversation)
                .ExecuteWithoutResultsAsync().Wait();

            _graphClient.Cypher
               .Match("(npc:Character)", "(con:Conversation)")
               .Where((Npc npc) => npc.Id == c.Id)
               .AndWhere((Conversation con) => con.Id == conversation.Id)
               .Create("(npc)-[:MEMORY]->(con)")
               .ExecuteWithoutResultsAsync().Wait();

            _graphClient.Cypher
               .Match("(player:Player)", "(con:Conversation)")
               .Where((Conversation con) => con.Id == conversation.Id)
               .Create($"(con)-[:MENTIONED]->(player)")
               .ExecuteWithoutResultsAsync().Wait();

        }

        public void SetCombatEquipment(Npc npc)
        {
            npc.Armor = RelatedTo<Armor, GraphRelationship>(x => x.Id == npc.Id, y => true, new GraphRelationship("EQUIPPED")).ResultsAsync.Result.FirstOrDefault();
            npc.Weapon = RelatedTo<Weapon, GraphRelationship>(x => x.Id == npc.Id, y => true, new GraphRelationship("EQUIPPED")).ResultsAsync.Result.FirstOrDefault();
        }

        public async void SaveCombatEquipment(Npc npc)
        {
            if (npc.Armor != null)
                await AddAndRelate(x => x.Id == npc.Id, npc.Armor, new GraphRelationship("EQUIPPED"));
            if (npc.Weapon != null)
                await AddAndRelate(x => x.Id == npc.Id, npc.Weapon, new GraphRelationship("EQUIPPED"));
        }

    }
}
