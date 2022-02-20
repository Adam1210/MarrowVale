using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Commands;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Entities.Graph;
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
    public class WorldContextService : IWorldContextService
    {
        private Random r = new Random();
        private readonly ITextGenerator _textGenerator;
        private readonly IGraphClient _graphClient;
        private readonly IAiService _aiService;
        private readonly IPlayerRepository _playerRepository;
        private readonly IPromptService _promptService;
        private readonly ILocationRepository _locationRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly INpcRepository _npcRepository;

        public static List<GraphNode> ContextNodes = new List<GraphNode>();

        public WorldContextService(ITextGenerator textGenerator, IGraphClient graphClient, IAiService aiService, IPlayerRepository playerRepository, IPromptService promptService, ILocationRepository locationRepository,
                                    IBuildingRepository buildingRepository, INpcRepository npcRepository, IRoomRepository roomRepository)
        {
            _textGenerator = textGenerator;
            _graphClient = graphClient;
            _aiService = aiService;
            _playerRepository = playerRepository;
            _promptService = promptService;
            _locationRepository = locationRepository;
            _buildingRepository = buildingRepository;
            _npcRepository = npcRepository;
            _roomRepository = roomRepository;
        }
        public void AttachToContext(string label, string id, object entity)
        {
            throw new NotImplementedException();
        }


        public T DirectObjectCommand<T>(string input, string context, GraphNode owner, CommandEnum command) where T : GraphNode
        {

            var prompt = _promptService.GenerateDirectObjectPrompt(input, command);
            var directObject = _aiService.Complete(prompt.ToString()).Result;
            return GetObjectLabelIdPair<T>(directObject, owner, input);
        }

        public string GenerateRoadFlavorText(Location road)
        {
            var nearbyBuildings = _locationRepository.GetNearbyBuildings(road);
            var nearbyRoads = _locationRepository.GetNearbyRoads(road);
            var input = new StringBuilder();
            foreach (var building in nearbyBuildings)
            {
                ContextNodes.Add(building);
                input.Append(building.ShortExteriorDescription());
            }
            foreach (var localRoad in nearbyRoads)
            {
                ContextNodes.Add(localRoad);
                input.Append(localRoad.ShortExteriorDescription());
            }
            var prompt = _promptService.GenerateSummaryDescription(input.ToString(), SummaryTypeEnum.Road);

            return _aiService.Complete(prompt).Result;
        }

        public string GenerateBuildingFlavorText(Location location)
        {
            var building = _buildingRepository.Single(x => x.Id == location.Id).Result;
            var input = building.DescriptionPromptInput();
            var roomPrompt = _promptService.GenerateSummaryDescription(input.ToString(), SummaryTypeEnum.Building);
            var roomOutput = _aiService.Complete(roomPrompt).Result;

            var peopleInsideRoom = _locationRepository.GetNpcsAtLocation(location);
            var npcInput = _promptService.GenerateSummaryDescription(input.ToString(), SummaryTypeEnum.Npcs);
            foreach (var npc in peopleInsideRoom)
            {
                npcInput.Input.Concat(npc.DescriptionPromptInput().ToString());
            }
            var npcPrompt = _promptService.GenerateSummaryDescription(npcInput.ToString(), SummaryTypeEnum.Npcs);
            var npcOutput = _aiService.Complete(roomPrompt).Result;


            return roomOutput + npcOutput;
        }

        public string GenerateRoomFlavorText(Location location)
        {
            var room = _roomRepository.Single(x => x.Id == location.Id).Result;
            var input = room.DescriptionPromptInput();
            room.Description = input.ToString();
            ContextNodes.Add(room);

            var prompt = _promptService.GenerateSummaryDescription(input.ToString(), SummaryTypeEnum.Room);
            var roomOutput = _aiService.Complete(prompt).Result;
            var peopleInsideRoom = _locationRepository.GetNpcsAtLocation(location);

            var npcInput = new StringBuilder($"Location: {roomOutput}\n");
            foreach (var npc in peopleInsideRoom)
            {
                npc.Description = npc.DescriptionPromptInput().ToString();
                ContextNodes.Add(npc);
                npcInput.Append(npc.Description);
            }

            var npcPrompt = _promptService.GenerateSummaryDescription(npcInput.ToString(), SummaryTypeEnum.Npcs);
            var npcOutput = _aiService.Complete(npcPrompt).Result;

            var connectingRooms = _locationRepository.GetConnectingRooms(location);
            var connectingRoomsInput = new StringBuilder();
            foreach (var r in connectingRooms)
            {
                r.Description = r.ShortDescriptionPrompt().ToString();
                ContextNodes.Add(r);
                connectingRoomsInput.Append(r.Description);
            }
            var connectingRoomPrompt = _promptService.GenerateSummaryDescription(connectingRoomsInput.ToString(), SummaryTypeEnum.ConnectingRooms);
            var connectingRoomOutput = _aiService.Complete(connectingRoomPrompt).Result;


            return roomOutput + npcOutput + connectingRoomOutput;
        }

        public GraphNode GetObjectLabelIdPair(string searchTerm, Player player, string input)
        {
            GraphNode node;
            try {
                node = _graphClient.Cypher
                    .Match("(n)")
                    .Where((GraphNode n) => n.Name == searchTerm)
                    .With("{Id: n.Id, Name: n.Name, Description: n.Description, Labels: labels(n)} as generalNode")
                    .Return(generalNode => generalNode.As<GraphNode>())
                    .ResultsAsync.Result.FirstOrDefault();

                if (node == null)
                    throw new Exception("No node found when searching direct object by name");

                //var pair = _graphClient.Cypher
                //            .Match("(n)")
                //            .Where((GraphNode n) => n.Name == searchTerm)
                //            .Return((x,n) => new
                //            {
                //                Id = n.As<GraphNode>(),
                //                Labels = x.Labels()
                //            }).ResultsAsync.Result.FirstOrDefault();

            }
            catch
            {
                return ContextSearch(searchTerm, input);
            }




            return node;
        }

        public T GetObjectLabelIdPair<T>(string searchTerm, GraphNode owner, string input) where T : GraphNode
        {
            try
            {
                return _graphClient.Cypher
                    .Match("(n)")
                    .Where((T n) => n.Name == searchTerm)
                    .Return(n => n.As<T>())
                    .ResultsAsync.Result.FirstOrDefault();
            }
            catch
            {
                return null;
                //return ContextSearch(searchTerm, input);
            }
        }

        public string DescribeConnectedPaths(Player player)
        {
            var currentLocationType = _playerRepository.PlayerLocationType(player);
            var currentLocation = _playerRepository.GetPlayerLocation(player);

            var prompt = new StringBuilder();
            prompt.Append("Generate a summary based on the following\n");
            prompt.Append("Name:Windsor Road | Description:A long winding road\n");
            prompt.Append("Name:The Rusty Nail| Description:A small dilapidating bar\n");
            prompt.Append("Name:Giant's Hotel | Description:A Large Beautiful hotel made out of marble\n");
            prompt.Append("Name:Tom's Cafe | Description:A restaurant advertising cheap drinks\n");
            prompt.Append("Summary: Leaving, you are now near Windsor Road, Rusty Nail, an old and run down bar, the Giant's Hotel is nearby.  You can also see Tom's Cafe\n\n");


            var nearbyBuildings = _graphClient.Cypher
                            .Match("(x:Building)-[:PATH]->(z:Building)")
                            .Where((Building x) => x.Id == currentLocation.Id)
                            .Return(z => z.As<Building>())
                            .ResultsAsync.Result;

            var nearbyRoads = _graphClient.Cypher
                .Match("(x:Building)-[:PATH]->(z:Road)")
                .Where((Building x) => x.Id == currentLocation.Id)
                .Return(z => z.As<Road>())
                .ResultsAsync.Result;

            foreach (var building in nearbyBuildings)
            {
                prompt.Append($"Name:{building.Name} | Description:{building.Description}\n");
            }
            foreach (var localRoad in nearbyRoads)
            {
                prompt.Append($"Name:{localRoad.Name} | Description:{localRoad.Description}\n");
            }
            prompt.Append("Summary:");
            return _aiService.Complete(prompt.ToString()).Result;
        }


        public GraphNode ContextSearch(string searchTerm, string conversation)
        {
            var contextDictionary = ContextNodes.ToDictionary(x => $"{x.Name}: {x.Description}",x => x);
            var documents = contextDictionary.Keys.ToArray();
            var query = $"{searchTerm}";
            var resultKey = _aiService.Search(query, documents).Result;
            return contextDictionary[resultKey];
        }



        public GraphNode ContextSearch(string searchTerm, GraphNode knowledge)
        {
            var contextDictionary = new Dictionary<string, GraphNode>();
            GetContextFromNode(knowledge, contextDictionary, "ARE", "YOU");
            var documents = contextDictionary.Keys.ToArray();
            var query = $"{searchTerm}";
            var resultKey = _aiService.Search(query, documents).Result;
            return contextDictionary[resultKey];
        }


        public void GetContextFromNode(GraphNode node, Dictionary<string, GraphNode> context, string relation, string subject)
        {
            foreach (var r in node.Relations ?? Enumerable.Empty<BaseRelationship>())
            {
                GetContextFromNode(r.Node, context, r.Relation, node.Name);
            }
            var description = $"{subject} {relation} {node.Name}";
            context[description] = node;
        
        }
    }
}
