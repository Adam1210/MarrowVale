using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Dtos;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MarrowVale.Business.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ILogger<CharacterService> _logger;
        private readonly IPrintService _printService;
        private readonly IGlobalItemsProvider _globalItemsProvider;
        private readonly IDrawingRepository _drawingRepository;
        private readonly IDrawingService _drawingService;
        private readonly IClassRepository _classRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IAppSettingsProvider _appSettingsProvider;

        public CharacterService(ILoggerFactory loggerFactory, IPrintService printService, IGlobalItemsProvider globalItemsProvider, 
            IDrawingRepository drawingRepository, IDrawingService drawingService, IClassRepository classRepository,
            IPlayerRepository playerRepository, IAppSettingsProvider appSettingsProvider)
        {
            _logger = loggerFactory.CreateLogger<CharacterService>();
            _printService = printService;
            _globalItemsProvider = globalItemsProvider;
            _drawingRepository = drawingRepository;
            _drawingService = drawingService;
            _classRepository = classRepository;
            _playerRepository = playerRepository;
            _appSettingsProvider = appSettingsProvider;
        }

        public Player NewCharacter()
        {
            if (_playerRepository.All().Result.Count() == 10)
            {
                _printService.Type("There are too many saves, you need to delete one to cotinue with your creation.");
                pickPlayerToRemove();
            }

            _printService.ClearConsole();
            _printService.Type("Now we are going to create a new character for your adventure.");

            Thread.Sleep(2000);
            
            var playerDto = new PlayerDto();

            pickRace(playerDto);
            pickGender(playerDto);
            pickName(playerDto);           
            pickClass(playerDto);

            var startingClass = _classRepository.GetClass(playerDto.Class);

            var player = new Player(playerDto);
            loadNewInventory(startingClass, player);
            _playerRepository.CreatePlayer(player).Wait();

            Console.WriteLine($"{Environment.NewLine}Name: {playerDto.Name}");
            Console.WriteLine($"Gender: {playerDto.Gender}");
            Console.WriteLine($"Race: {playerDto.Race}");
            Console.WriteLine($"Class: {playerDto.Class}");

            return player;
        }

        public Player LoadCharacter()
        {
            var players = _playerRepository.All().Result.ToList();
            displaySavedCharacters(players);

            _printService.Type("Enter the name of the player you would like to play.");

            var name = _globalItemsProvider.UpperFirstChar(_printService.ReadInput());


            var playerId = players.FirstOrDefault(x=> x.Name == name).Id;
            var player = _playerRepository.GetPlayerWithInventory(playerId);

            player.LastSaveDateTime = DateTime.Now;

            //_playerRepository.SavePlayers();

            if (player == null)
            {
                _printService.Print($"No player with name {name} exists.");
                Thread.Sleep(2000);
                return LoadCharacter();
            }
            else
            {
                return player;
            }
        }

        public Inventory GetInventory(Player player)
        {            
            return _playerRepository.GetInventory(player);
        }
        
        private void pickPlayerToRemove()
        {
            var players = _playerRepository.All().Result.ToList();
            displaySavedCharacters(players);

            _printService.Type("Enter the name of the player you would like to delete.");

            var name = _globalItemsProvider.UpperFirstChar(_printService.ReadInput());
            var player = _playerRepository.Single(x => x.Name == name).Result;

            if (player == null)
            {
                _printService.Print($"No player with name {name} exists.");
                Thread.Sleep(2000);
                pickPlayerToRemove();
            }

            _playerRepository.Delete(x => x.Id == player.Id);
        }

        private void displaySavedCharacters(IList<Player> players)
        {
            _printService.ClearConsole();

            _drawingService.PrintArtCentered(_drawingRepository.GetLoadSaveArt());

            foreach (var player in players)
            {
                _printService.PrintCentered($"{player.Name}  ||  {player.Race.ToString()}  ||  {player.Class.ToString()}");
            }
        }

        private void pickName(PlayerDto playerDto)
        {
            var characterName = _drawingRepository.GetCharacterCreationStateArt(PlayerCreationStateEnum.Name);

            _printService.ClearConsole();

            _drawingService.PrintArtCentered(characterName);

            _printService.Print("What do you want your character's name to be?");

            var name = _globalItemsProvider.UpperFirstChar(_printService.ReadInput());

            var newName = _playerRepository.Single(x => x.Name.ToUpper() == name.ToUpper()).Result;

            if (newName != null)
            {
                _printService.Print("That name is already taken. Pick a new name.");
                Thread.Sleep(2000);
                pickName(playerDto);
            }

            var choiceMade = false;

            while (!choiceMade)
            {
                _printService.Print($"Are you sure you want your character's name to be {name}?  yes | no");

                var choice = _printService.ReadInput();

                if (choice.ToUpper() == "YES")
                {
                    playerDto.Name = name;
                    choiceMade = true;
                }
                else if (choice.ToUpper() == "NO")
                {
                    pickName(playerDto);
                    choiceMade = true;
                }
                else
                {
                    _printService.Print("You must type in yes or no");
                }
            }
        }

        private void pickRace(PlayerDto playerDto)
        {
            var characterRace = _drawingRepository.GetCharacterCreationStateArt(PlayerCreationStateEnum.Race);

            _printService.ClearConsole();

            _drawingService.PrintArtCentered(characterRace);
         
            _printService.Print("Which race do you want your new character to be?  Human | Elf | Dwarf");          

            var race = _globalItemsProvider.UpperFirstChar(_printService.ReadInput());

            verifyRacePick(playerDto, race);
        }

        private void verifyRacePick(PlayerDto playerDto, string race)
        {
            if(Enum.TryParse<PlayerRaceEnum>(race, out var result))
            {
                var choiceMade = false;

                while (!choiceMade)
                {
                    _printService.Print($"Are you sure you want to be a {race}?  yes | no");

                    var choice = _printService.ReadInput();

                    if (choice.ToUpper() == "YES")
                    {
                        playerDto.Race = result;
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        pickRace(playerDto);
                        choiceMade = true;
                    }
                    else
                    {
                        _printService.Print("You must type yes or no.");
                    }
                }
            }
            else
            {
                _printService.Print("You must type Human, Elf, or Dwarf.");
                Thread.Sleep(3);
                pickRace(playerDto);
            }
        }

        private void pickGender(PlayerDto playerDto)
        {
            var characterGender = _drawingRepository.GetCharacterCreationStateArt(PlayerCreationStateEnum.Gender);

            _printService.ClearConsole();

            _drawingService.PrintArtCentered(characterGender);

            _printService.Print("Do you want your character to be a male or a female?");

            var gender = _printService.ReadInput();

            if (gender.ToUpper() == "MALE" || gender.ToUpper() == "FEMALE")
            {
                var choiceMade = false;

                while (!choiceMade)
                {
                    _printService.Print($"Are you sure you want your character to be {gender}?  yes | no");

                    var decision = _printService.ReadInput();

                    if (decision.ToUpper() == "NO")
                    {
                        pickGender(playerDto);
                        choiceMade = true;
                    }
                    else if (decision.ToUpper() == "YES")
                    {
                        playerDto.Gender = _globalItemsProvider.UpperFirstChar(gender);
                        choiceMade = true;
                    }
                    else
                    {
                        _printService.Print("You must type in yes or no");
                    }
                }
            }
            else
            {
                _printService.Print("You must type in male or female.");
                Thread.Sleep(3);
                pickGender(playerDto);
            }
        }

        private void printClassDescriptions()
        {
            var characterClass = _drawingRepository.GetCharacterCreationStateArt(PlayerCreationStateEnum.Class);

            _printService.ClearConsole();

            _drawingService.PrintArtCentered(characterClass);

            var warriorDescription = $"Only ever knowing violence, the warrior's purpose is to protect people from ever experiencing that pain themselves.{Environment.NewLine}" +
                                     $"Unmatched with blades and axes, the warrior likes to get up close and personal during combat. They rely on their{Environment.NewLine}" +
                                     $"strength above all else to solve their problems.";

            var rangerDescription = $"Born of the forest, a ranger is one with their surroundings. Heightened senses allow the ranger to detect changes in{Environment.NewLine}" +
                                    $"their environment that others may not realize. The ranger excels with a bow. The ranger's soft footsteps developed over{Environment.NewLine}" +
                                    $"years of hunting and tracking are a great tool for getting into places undetected.";

            var mageDescription = $"The mage is born with a connection to the Elemental Plane. Years of study and patience has resulted in an understanding{Environment.NewLine}" +
                                  $"of the magical world that others can't even begin to realize. Typically, the mage prefers non-physical means of solving{Environment.NewLine}" +
                                  $"problems, and with their high intelligence and their knowledge of the magical arts, there are few problems that a mage{Environment.NewLine}" +
                                  $"cannot solve.";

            _printService.Print(warriorDescription);
            _printService.Print(rangerDescription, _appSettingsProvider.CharacterCreateWait);
            _printService.Print(mageDescription, _appSettingsProvider.CharacterCreateWait);
        }

        private void pickClass(PlayerDto playerDto)
        {
            printClassDescriptions();

            _printService.Print("Which class do you want your new character to be?  Warrior | Ranger | Mage", 10);

            var charClass = _globalItemsProvider.UpperFirstChar(_printService.ReadInput());

            verifyClassPick(playerDto, charClass);
        }
                
        private void verifyClassPick(PlayerDto playerDto, string charClass)
        {
            if(Enum.TryParse<ClassEnum>(charClass, out var result)){
                var choiceMade = false;

                while (!choiceMade)
                {
                    _printService.Print($"Are you sure you want to be a {charClass}?  yes | no");

                    var choice = _printService.ReadInput();

                    if (choice.ToUpper() == "YES")
                    {
                        playerDto.Class = result;
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        pickClass(playerDto);
                        choiceMade = true;
                    }
                    else
                    {
                        _printService.Print("You must type yes or no.");
                    }
                }
            }
            else
            {
                _printService.Print("You must type the name of the class that you wish for your character.");
                Thread.Sleep(5000);
                pickClass(playerDto);
            }
        }

        private void loadNewInventory(Class startingClass, Player player)
        {
            player.CurrentWeapon = startingClass.StartingWeapons.FirstOrDefault();
            player.CurrentArmor = new Armor { Name = "Scraps of Leather", BaseWorth = 0, PysicalResistance = 1, MagicResistance = 0, Description = "Cheap Leather Armor" };

            //get weapons
            foreach (var weapon in startingClass.StartingWeapons)
            {
                player.Inventory.AddItem(weapon);
            }

            //get consumables
            foreach(var consumable in startingClass.StartingConsumables)
            {
                player.Inventory.AddItem(consumable);
            }

            //get ammunition
            foreach (var ammoStack in startingClass.StartingAmmunition)
            {
                player.Inventory.AddItem(ammoStack);
            }

            //get spells
            foreach(var spell in startingClass.StartingSpells)
            {
                player.AddSpellToSpellbook(spell);
            }

            //get abilities
            foreach(var ability in startingClass.StartingAbilities)
            {
                player.AddAbility(ability);
            }
        }
    }
}
