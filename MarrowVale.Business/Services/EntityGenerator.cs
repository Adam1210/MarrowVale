using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Common.Evaluator;
using MarrowVale.Data.Contracts;
using MarrowVale.Data.Seeder;
using MarrowVale.Data.Seeder.Npcs;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarrowVale.Business.Services
{
    public class EntityGenerator : IEntityGenerator
    {
        private Random r = new Random();
        private readonly ITextGenerator _textGenerator;
        private readonly IGraphClient _graphClient;
        private readonly NpcSeed _npcSeeds;
        private List<Building> buildings = new List<Building>();
        private readonly IOpenAiSettingRepository _openAiSettingRepository;
        private Armor DefaultArmor = new Armor("Basic Cloth Armor", "This armor doesn't protect against much", 2, "Cloth", 0, 0);
        private Weapon DefaultWeapon = new Weapon("Purple Stick", "Strange color for a stick", 0, WeaponTypeEnum.Wand, 1, 0);
        private readonly INpcRepository _npcRepository;

        public EntityGenerator(ITextGenerator textGenerator, IGraphClient graphClient, IOpenAiSettingRepository openAiSettingRepository, INpcRepository npcRepository)
        {
            _textGenerator = textGenerator;
            _graphClient = graphClient;
            _npcSeeds = new NpcSeed();
            _openAiSettingRepository = openAiSettingRepository;
            _npcRepository = npcRepository;
        }

        public void GenerateMap()
        {
            var x = r.Next(9000, 9000);
            var y = r.Next(9000, 9000);

            var map = new Map() { Name = "MarrowVale" };
            map.SetCoordinates(0, 0, x, y, x, y);
            GenerateRegions(map);
        }

        public void GenerateRegions(Map map)
        {
            var startingX = 0;
            var startingY = 0;


            for (int i = 0; i < 3; i++)
            {
                var xSize = r.Next(map.xLength() / 3);
                var ySize = r.Next(map.yLength() / 3);
                for (int j = 0; j < 3; j++)
                {
                    var enviornment = (EnviornmentEnum)r.Next(Enum.GetNames(typeof(EnviornmentEnum)).Length);
                    var region = new Region { Environment = enviornment };
                    region.SetCoordinates(0, 0, xSize, ySize, map.xLength(), map.yLength());
                    GenerateCities(region);
                    map.Regions.Add(region);

                    startingX += xSize;
                }
                startingX = 0;
                startingY += ySize;

            }
        }


        public void GenerateCities(Region region)
        {

        }





        public async void GenerateBuildings()
        {
            var tresendarManor = new Building
            {
                Id = Guid.NewGuid().ToString(),
                BuildingType = BuildingEnum.Manor,
                Description = "Old large dilapidating mansion",
                Name = "Trensadar Manor",
            };
            tresendarManor.GenerateRooms();

            var sleepingGiant = new Building
            {
                Id = Guid.NewGuid().ToString(),
                BuildingType = BuildingEnum.Inn,
                Description = "The inn of the town, quite small",
                Name = "Sleeping Giant"
            };
            sleepingGiant.GenerateRooms();

            var townMastersHall = new Building
            {
                Id = Guid.NewGuid().ToString(),
                BuildingType = BuildingEnum.Inn,
                Description = "Small building that facilities very small amount of local politics",
                Name = "Townmaster's Hall"
            };
            townMastersHall.GenerateRooms();



            var phandalinMinersExchange = new Building
            {
                Id = Guid.NewGuid().ToString(),
                BuildingType = BuildingEnum.Exchange,
                Description = "Contains Many Drwaves selling gems",
                Name = "Phandalin's Miners Exchange"
            };
            phandalinMinersExchange.GenerateRooms();

            var roseRoadStart = new Road
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Rose Road Start",
                Length = 400,
                Description = "Narrow dirt road",
                Units = "feet",
                StartOfRoad = true
            };

            var roseRoadEnd = new Road
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Rose Road End",
                Length = 400,
                Description = "Narrow dirt road",
                Units = "feet",
                StartOfRoad = false
            };


            var gloryRouteStart = new Road
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Glory Route Start",
                Length = 400,
                Description = "Winding long dirt road",
                Units = "feet",
                StartOfRoad = true
            };

            var gloryRouteEnd = new Road
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Glory Route End",
                Length = 400,
                Description = "Winding long dirt road",
                Units = "feet",
                StartOfRoad = false
            };

            var roads = new List<Road>();

            buildings.Add(tresendarManor);
            buildings.Add(sleepingGiant);
            buildings.Add(townMastersHall);
            buildings.Add(phandalinMinersExchange);
            roads.Add(gloryRouteEnd);
            roads.Add(gloryRouteStart);
            roads.Add(roseRoadStart);
            roads.Add(roseRoadEnd);

            tresendarManor.LeadsTo.Add(gloryRouteEnd);
            sleepingGiant.LeadsTo.Add(gloryRouteEnd);
            townMastersHall.LeadsTo.Add(gloryRouteStart);
            townMastersHall.LeadsTo.Add(roseRoadStart);
            phandalinMinersExchange.LeadsTo.Add(roseRoadEnd);
            gloryRouteEnd.LeadsTo.Add(tresendarManor);
            gloryRouteEnd.LeadsTo.Add(sleepingGiant);
            gloryRouteEnd.LeadsTo.Add(gloryRouteStart);
            gloryRouteStart.LeadsTo.Add(gloryRouteEnd);
            gloryRouteStart.LeadsTo.Add(townMastersHall);
            gloryRouteStart.LeadsTo.Add(roseRoadStart);
            roseRoadStart.LeadsTo.Add(gloryRouteStart);
            roseRoadStart.LeadsTo.Add(townMastersHall);
            roseRoadStart.LeadsTo.Add(roseRoadEnd);
            roseRoadEnd.LeadsTo.Add(phandalinMinersExchange);
            roseRoadEnd.LeadsTo.Add(roseRoadStart);


            foreach (var building in buildings)
            {
                await CreateBuildings(building);
            }
            foreach (var road in roads)
            {
                await CreateRoads(road);
            }
            foreach (var building in buildings)
            {
                await AddBuildingDoors(building);
            }
            foreach (var building in buildings)
            {
                foreach (var room in building.Rooms)
                {
                    await AddRoomRelations(room);
                }
            }
            foreach (var road in roads)
            {
                await AddPaths(road);
            }




            GenerateCharacters();
        }

        public void GenerateCharacters()
        {
            var (adam,eve) = _npcSeeds.GetOriginHumans();

            AddCharacter(adam);
            AddandRelateCharacter(eve);
            GenerateChildren(adam, eve);
        }

        public void GenerateChildren(Npc dad, Npc mom)
        {

            var person = new Npc
            {
                Id = Guid.NewGuid().ToString(),
                Race = dad.Race,
                Class = dad.Class,
                LastName = dad.LastName,
                Description = "",
                CurrentHealth = 100,
                MaxHealth = 100,
                Age = mom.Age - r.Next(20, Math.Min(mom.Age, 45)),
                Gender = r.Next(0, 1) == 0 ? "Male" : "Female",
                Openness = dad.Openness + mom.Openness,
                Occupation = OccupationEnum.Merchant,
                IsAlive = true,
                Armor = DefaultArmor,
                Weapon = DefaultWeapon
            };
            person.Armor.Id = Guid.NewGuid().ToString();
            person.Weapon.Id = Guid.NewGuid().ToString();

            var name = _textGenerator.GenerateCharacterName(person);
            if (name == dad.Name || name ==  mom.Name)
                name = $"{name} Jr.";

            person.Name = name;
            person.Description = _textGenerator.GenerateCharacterDescription(person);
            person.AddRelationship(dad, RelationEnum.Parent);
            person.AddRelationship(mom, RelationEnum.Parent);

            AddandRelateCharacter(person);


            if (person.Age > 18 && !(person.Relationships.Any(x => x.Relation == RelationEnum.Married)))
            {
                GeneratePartner(person);
            }

            if (person.Age > 18 && (person.Relationships.Any(x => x.Relation == RelationEnum.Married)))
            {
                var partner = person.Relationships.First(x => x.Relation == RelationEnum.Married).Toward;
                var childBearingRange = (partner.Age - 17) > 20 ? 20 : (partner.Age - 17);
                var numberOfChildren = (childBearingRange * r.Next(0, 15))/100;

                for (int i = 0; i < numberOfChildren; i++)
                {
                    GenerateChildren(person, partner);
                }
            }

        }

        public void GeneratePartner(Npc partner)
        {
            var person = new Npc
            {
                Id = Guid.NewGuid().ToString(),
                Race = NpcRaceEnum.Human,
                Class = ClassEnum.Warrior,
                LastName = partner.LastName,
                Description = "",
                CurrentHealth = 100,
                MaxHealth = 100,
                Age = partner.Age,
                Gender = partner.Gender == "Male" ? "Female" : "Male",
                Occupation = OccupationEnum.Merchant,
                IsAlive = true,
                Armor = DefaultArmor,
                Weapon = DefaultWeapon
            };
            person.Armor.Id = Guid.NewGuid().ToString();
            person.Weapon.Id = Guid.NewGuid().ToString();

            person.Name = _textGenerator.GenerateCharacterName(person);
            person.Description = _textGenerator.GenerateCharacterDescription(person);

            person.AddRelationship(partner, RelationEnum.Married);
            AddandRelateCharacter(person);

        }


        public async void AddCharacter(Npc character)
        {
            await _graphClient.Cypher
                .Create("(character:Character $newCharacter)")
                .WithParam("newCharacter", character)
                .ExecuteWithoutResultsAsync();

            _npcRepository.SaveCombatEquipment(character);


            var allRooms = buildings.SelectMany(x => x.Rooms).ToList();
            var randomRoom = allRooms[(r.Next(0, allRooms.Count))];

            await _graphClient.Cypher
                .Match("(ch:Character)", "(room:Room)")
                .Where((Room room) => room.Id == randomRoom.Id)
                .AndWhere((Npc ch) => ch.Id == character.Id)
                .Create($"(ch)-[:INSIDE]->(room)")
                .ExecuteWithoutResultsAsync();
        }

        public async void AddandRelateCharacter(Npc newCharacter)
        {

            await _graphClient.Cypher
                .Create("(character:Character $newCharacter)")
                .WithParam("newCharacter", newCharacter)
                .ExecuteWithoutResultsAsync();

            _npcRepository.SaveCombatEquipment(newCharacter);


            var allRooms = buildings.SelectMany(x => x.Rooms).ToList();
            var randomRoom = allRooms[(r.Next(0, allRooms.Count))];

            await _graphClient.Cypher
                .Match("(ch:Character)", "(room:Room)")
                .Where((Room room) => room.Id == randomRoom.Id)
                .AndWhere((Npc ch) => ch.Id == newCharacter.Id)
                .Create($"(ch)-[:INSIDE]->(room)")
                .ExecuteWithoutResultsAsync();

            foreach (var rel in newCharacter.Relationships)
            {
                var relationTo = rel.Toward;
                var r = $"(character1)-[:{rel.Relation.ToString()}]->(character2)";
                await _graphClient.Cypher
                    .Match("(character1:Character)", "(character2:Character)")
                    .Where((Npc character1) => character1.Id == newCharacter.Id)
                    .AndWhere((Npc character2) => character2.Id == relationTo.Id)
                    .Create(r)
                    .ExecuteWithoutResultsAsync();

                var inverseRelation = rel.Toward.Relationships.First(x => x.Toward?.Id == newCharacter.Id);
                await _graphClient.Cypher
                   .Match("(character1:Character)", "(character2:Character)")
                   .Where((Npc character1) => character1.Id == newCharacter.Id)
                   .AndWhere((Npc character2) => character2.Id == relationTo.Id)
                   .Create($"(character2)-[:{inverseRelation.Relation.ToString()}]->(character1)")
                   .ExecuteWithoutResultsAsync();
            }

        }

        public async Task CreateRoads(Road road)
        {
            await _graphClient.Cypher
                .Create("(road:Road:Location $road)")
                .WithParam("road", road)
                .ExecuteWithoutResultsAsync();
        }

        public async Task CreateBuildings(Building building)
        {
            await _graphClient.Cypher
                .Create("(building:Building:Location $building)")
                .WithParam("building", building)
                .ExecuteWithoutResultsAsync();

            foreach (var room in building.Rooms)
            {
                await _graphClient.Cypher
                .Create("(room:Room:Location $room)")
                .WithParam("room", room)
                .ExecuteWithoutResultsAsync();
            }
        }


        public async Task AddPaths(Road road)
        {
            foreach (var path in road.LeadsTo)
            {
                await _graphClient.Cypher
                    .Match("(start:Road)", "(leadsTo:Road)")
                    .Where((Road start) => start.Id == road.Id)
                    .AndWhere((Road leadsTo) => leadsTo.Id == path.Id)
                    .Create($"(start)-[:PATH]->(leadsTo)")
                    .ExecuteWithoutResultsAsync();
            }

            foreach (var path in road.LeadsTo)
            {
                await _graphClient.Cypher
                    .Match("(start:Road)", "(leadsTo:Building)")
                    .Where((Road start) => start.Id == road.Id)
                    .AndWhere((Building leadsTo) => leadsTo.Id == path.Id)
                    .Create($"(start)-[:PATH]->(leadsTo)")
                    .ExecuteWithoutResultsAsync();
            }
        }

        public async Task AddBuildingDoors(Building building)
        {
            foreach (var path in building.LeadsTo)
            {
                await _graphClient.Cypher
                    .Match("(start:Building)", "(leadsTo:Road)")
                    .Where((Building start) => start.Id == building.Id)
                    .AndWhere((Road leadsTo) => leadsTo.Id == path.Id)
                    .Create($"(start)-[:PATH]->(leadsTo)")
                    .ExecuteWithoutResultsAsync();
            }

            await _graphClient.Cypher
                .Match("(r:Room)", "(b:Building)")
                .Where((Room r) => r.Id == building.Entrance.Id)
                .AndWhere((Building b) => b.Id == building.Id)
                .Create($"(r)-[:PATH]->(b)")
                .Create($"(b)-[:PATH]->(r)")
                .ExecuteWithoutResultsAsync();

        }

        public async Task AddRoomRelations(Room room)
        {
            await _graphClient.Cypher
                .Match("(r:Room)", "(b:Building)")
                .Where((Room r) => r.Id == room.Id)
                .AndWhere((Building b) => b.Id == room.ParentBuilding.Id)
                .Create($"(r)-[:INSIDE]->(b)")
                .Create($"(b)-[:PARTOF]->(r)")
                .ExecuteWithoutResultsAsync();

            foreach (var connectedRoom in room.LeadsTo)
            {
                await _graphClient.Cypher
                    .Match("(r1:Room)", "(r2:Room)")
                    .Where((Room r1) => r1.Id == room.Id)
                    .AndWhere((Room r2) => r2.Id == connectedRoom.Id)
                    .Create($"(r1)-[:PATH]->(r2)")
                    .ExecuteWithoutResultsAsync();
            }

        }

        public async Task DefaultSettings()
        {
            var settingDictionary = new OpenAiSettingSeeds().DefaultSettingCollection();
            foreach (KeyValuePair<string, OpenAiSettings> entry in settingDictionary)
            {
                await _openAiSettingRepository.CreatePrompts(new PromptType(entry.Key));
                await _openAiSettingRepository.CreateSetting(entry.Key, entry.Value);
            }
        }
    }
}
