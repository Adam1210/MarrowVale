using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities;
using MarrowVale.Business.Entities.Dtos;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using System;
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

        public CharacterService(ILoggerFactory loggerFactory, IPrintService printService, IGlobalItemsProvider globalItemsProvider, 
            IDrawingRepository drawingRepository, IDrawingService drawingService, IClassRepository classRepository,
            IPlayerRepository playerRepository)
        {
            _logger = loggerFactory.CreateLogger<CharacterService>();
            _printService = printService;
            _globalItemsProvider = globalItemsProvider;
            _drawingRepository = drawingRepository;
            _drawingService = drawingService;
            _classRepository = classRepository;
            _playerRepository = playerRepository;
        }

        public Player NewCharacter(PlayerDto playerDto)
        {
            _printService.Type("Now we are going to create a new character for your adventure.");

            Thread.Sleep(2000);

            pickRace(playerDto);
            pickGender(playerDto);
            pickName(playerDto);           
            pickClass(playerDto);

            var startingClass = _classRepository.GetClass(playerDto.Class);

            var player = new Player(playerDto);

            loadNewInventory(startingClass, player);

            _playerRepository.AddPlayer(player);
                       
            Console.WriteLine($"{Environment.NewLine}Name: {playerDto.Name}");
            Console.WriteLine($"Gender: {playerDto.Gender}");
            Console.WriteLine($"Race: {playerDto.Race}");
            Console.WriteLine($"Class: {playerDto.Class}");

            return player;
        }

        public Player LoadCharacter(PlayerDto playerDto)
        {
            //gets character
            //loads inventory and location
            _printService.ClearConsole();

            var player = _playerRepository.GetPlayers();

            foreach(var item in player)
            {
                _printService.PrintCentered($"{item.Name}: {item.Race} {item.Class}");
            }

            return new Player();
        }

        public Inventory GetInventory(Player player)
        {            
            return player.Inventory;
        }

        private void pickName(PlayerDto playerDto)
        {
            var characterName = _drawingRepository.GetCharacterCreationStateArt(PlayerCreationStateEnum.Name);

            _printService.ClearConsole();

            _drawingService.PrintArtCentered(characterName);

            _printService.Print("What do you want your character's name to be?");

            var name = _globalItemsProvider.UpperFirstChar(_printService.ReadInput());

            var newName = _playerRepository.GetPlayer(name);

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

            if (race == Enum.GetName(typeof(RaceEnum), 0))
            {
                var choiceMade = false;

                while (!choiceMade)
                {
                    //chose human
                    _printService.Print($"Are you sure you want to be a {race}?  yes | no");

                    var choice = _printService.ReadInput();

                    if (choice.ToUpper() == "YES")
                    {
                        playerDto.Race = RaceEnum.Human;
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
            else if (race == Enum.GetName(typeof(RaceEnum), 1))
            {
                var choiceMade = false;

                while (!choiceMade)
                {
                    //chose elf
                    _printService.Print($"Are you sure you want to be a {race}?  yes | no");

                    var choice = _printService.ReadInput();

                    if (choice.ToUpper() == "YES")
                    {
                        playerDto.Race = RaceEnum.Elf;
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
            else if (race == Enum.GetName(typeof(RaceEnum), 2))
            {
                var choiceMade = false;

                while (!choiceMade)
                {
                    //chose dwarf
                    _printService.Print($"Are you sure you want to be a {race}?  yes | no");

                    var choice = _printService.ReadInput();

                    if (choice.ToUpper() == "YES")
                    {
                        playerDto.Race = RaceEnum.Dwarf;
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

        private void pickClass(PlayerDto playerDto)
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
            _printService.Print(rangerDescription, 10);
            _printService.Print(mageDescription, 10);

            _printService.Print("Which class do you want your new character to be?  Warrior | Ranger | Mage", 10);

            var charClass = _globalItemsProvider.UpperFirstChar(_printService.ReadInput());

            if (charClass == "Warrior")
            {
                var choiceMade = false;

                while (!choiceMade)
                {
                    _printService.Print($"Are you sure you want to be a {charClass}?  yes | no");

                    var choice = _printService.ReadInput();

                    if (choice.ToUpper() == "YES")
                    {
                        playerDto.Class = ClassEnum.Warrior;
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        repickClass(playerDto);
                        choiceMade = true;
                    }
                    else
                    {
                        _printService.Print("You must type yes or no.");
                    }
                }
            }
            else if (charClass == "Ranger")
            {
                var choiceMade = false;

                while (!choiceMade)
                {
                    _printService.Print($"Are you sure you want to be a {charClass}?  yes | no");

                    var choice = _printService.ReadInput();

                    if (choice.ToUpper() == "YES")
                    {
                        playerDto.Class = ClassEnum.Ranger;
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        repickClass(playerDto);
                        choiceMade = true;
                    }
                    else
                    {
                        _printService.Print("You must type yes or no.");
                    }
                }
            }
            else if (charClass == "Mage")
            {
                var choiceMade = false;

                while (!choiceMade)
                {
                    _printService.Print($"Are you sure you want to be a {charClass}?  yes | no");

                    var choice = _printService.ReadInput();

                    if (choice.ToUpper() == "YES")
                    {
                        playerDto.Class = ClassEnum.Mage;          
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        repickClass(playerDto);
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
                repickClass(playerDto);
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

        private void repickClass(PlayerDto playerDto)
        {
            var characterClass = _drawingRepository.GetCharacterCreationStateArt(PlayerCreationStateEnum.Class);

            _printService.ClearConsole();

            _drawingService.PrintArtCentered(characterClass);

            var warriorDescription = "Only ever knowing violence, the warrior's purpose is to protect people from ever experiencing that pain themselves.\n" +
                                     "Unmatched with blades and axes, the warrior likes to get up close and personal during combat. They rely on their\n" +
                                     "strength above all else to solve their problems.";

            var rangerDescription = "Born of the forest, a ranger is one with their surroundings. Heightened senses allow the ranger to detect changes in\n" +
                                    "their environment that others may not realize. The ranger excels with a bow. The ranger's soft footsteps developed over\n" +
                                    "years of hunting and tracking are a great tool for getting into places undetected.";

            var mageDescription = "The mage is born with a connection to the Elemental Plane. Years of study and patience has resulted in an understanding\n" +
                                  "of the magical world that others can't even begin to realize. Typically, the mage prefers non-physical means of solving\n" +
                                  "problems, and with their high intelligence and their knowledge of the magical arts, there are few problems that a mage\n" +
                                  "cannot solve.";

            _printService.Print(warriorDescription);
            _printService.Print(rangerDescription);
            _printService.Print(mageDescription);

            _printService.Print("Which class do you want your new character to be?  Warrior | Ranger | Mage");

            var charClass = _globalItemsProvider.UpperFirstChar(_printService.ReadInput());

            if (charClass == "Warrior")
            {
                var choiceMade = false;

                while (!choiceMade)
                {
                    _printService.Print($"Are you sure you want to be a {charClass}?  yes | no");

                    var choice = _printService.ReadInput();

                    if (choice.ToUpper() == "YES")
                    {
                        playerDto.Class = ClassEnum.Warrior;
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        repickClass(playerDto);
                        choiceMade = true;
                    }
                    else
                    {
                        _printService.Print("You must type yes or no.");
                    }
                }
            }
            else if (charClass == "Ranger")
            {
                var choiceMade = false;

                while (!choiceMade)
                {
                    _printService.Print($"Are you sure you want to be a {charClass}?  yes | no");

                    var choice = _printService.ReadInput();

                    if (choice.ToUpper() == "YES")
                    {
                        playerDto.Class = ClassEnum.Ranger;
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        repickClass(playerDto);
                        choiceMade = true;
                    }
                    else
                    {
                        _printService.Print("You must type yes or no.");
                    }
                }
            }
            else if (charClass == "Mage")
            {
                var choiceMade = false;

                while (!choiceMade)
                {
                    _printService.Print($"Are you sure you want to be a {charClass}?  yes | no");

                    var choice = _printService.ReadInput();

                    if (choice.ToUpper() == "YES")
                    {
                        playerDto.Class = ClassEnum.Mage;
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        repickClass(playerDto);
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
                repickClass(playerDto);
            }

        }

        private void loadNewInventory(Class startingClass, Player player)
        {
            //get weapons
            foreach(var weapon in startingClass.StartingWeapons)
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
