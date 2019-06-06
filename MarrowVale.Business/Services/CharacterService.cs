using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities;
using MarrowVale.Business.Entities.Dtos;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using System;
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

        public CharacterService(ILoggerFactory loggerFactory, IPrintService printService, IGlobalItemsProvider globalItemsProvider, 
            IDrawingRepository drawingRepository, IDrawingService drawingService)
        {
            _logger = loggerFactory.CreateLogger<CharacterService>();
            _printService = printService;
            _globalItemsProvider = globalItemsProvider;
            _drawingRepository = drawingRepository;
            _drawingService = drawingService;
        }

        public void NewCharacter(GameDto gameDto)
        {
            _printService.Print("Now we are going to create a new character for your adventure.");

            PickRace(gameDto);
            PickGender(gameDto);
            PickName(gameDto);           
            PickClass(gameDto);

            //Next we get their starting items etc. Save everything to database/json.

            Console.WriteLine($"\nName: {gameDto.Player.Name}");
            Console.WriteLine($"Gender: {gameDto.Player.Gender}");
            Console.WriteLine($"Race: {gameDto.Player.Race}");
            Console.WriteLine($"Class: {gameDto.Player.Class.Name}");
        }

        public void LoadCharacter(GameDto gameDto)
        {
            //gets character
            //loads inventory and location
        }

        public Inventory GetInventory(Player player)
        {
            return new Inventory();
        }

        private void PickName(GameDto gameDto)
        {
            var characterName = _drawingRepository.GetCharacterCreationStateArt(PlayerCreationStateEnum.Name);

            _printService.ClearConsole();

            _drawingService.PrintArtCentered(characterName);

            _printService.Print("What do you want you character's name to be?");

            var name = _globalItemsProvider.UpperFirstChar(_printService.ReadInput());

            var choiceMade = false;

            while (!choiceMade)
            {
                _printService.Print($"Are you sure you want your character's name to be {name}?  yes | no");

                var choice = _printService.ReadInput();

                if (choice.ToUpper() == "YES")
                {
                    gameDto.Player.Name = name;
                    choiceMade = true;
                }
                else if (choice.ToUpper() == "NO")
                {
                    PickName(gameDto);
                    choiceMade = true;
                }
                else
                {
                    _printService.Print("You must type in yes or no");
                }
            }
        }

        private void PickRace(GameDto gameDto)
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
                        gameDto.Player.Race = RaceEnum.Human;
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        PickRace(gameDto);
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
                        gameDto.Player.Race = RaceEnum.Elf;
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        PickRace(gameDto);
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
                        gameDto.Player.Race = RaceEnum.Dwarf;
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        PickRace(gameDto);
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
                PickRace(gameDto);
            }
        }

        private void PickClass(GameDto gameDto)
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
                        gameDto.Player.Class = new Class()
                        {
                            Name = "Warrior",
                            Description = warriorDescription
                        };
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        repickClass(gameDto);
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
                        gameDto.Player.Class = new Class()
                        {
                            Name = "Ranger",
                            Description = rangerDescription
                        };
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        repickClass(gameDto);
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
                        gameDto.Player.Class = new Class()
                        {
                            Name = "Mage",
                            Description = mageDescription
                        };
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        repickClass(gameDto);
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
                repickClass(gameDto);
            }

        }

        private void PickGender(GameDto gameDto)
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
                        PickGender(gameDto);
                        choiceMade = true;
                    }
                    else if (decision.ToUpper() == "YES")
                    {
                        gameDto.Player.Gender = _globalItemsProvider.UpperFirstChar(gender);
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
                PickGender(gameDto);
            }
        }

        private void repickClass(GameDto gameDto)
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
                        gameDto.Player.Class = new Class()
                        {
                            Name = "Warrior",
                            Description = warriorDescription
                        };
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        repickClass(gameDto);
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
                        gameDto.Player.Class = new Class()
                        {
                            Name = "Ranger",
                            Description = rangerDescription
                        };
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        repickClass(gameDto);
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
                        gameDto.Player.Class = new Class()
                        {
                            Name = "Mage",
                            Description = mageDescription
                        };
                        choiceMade = true;
                    }
                    else if (choice.ToUpper() == "NO")
                    {
                        repickClass(gameDto);
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
                repickClass(gameDto);
            }

        }
    }
}
