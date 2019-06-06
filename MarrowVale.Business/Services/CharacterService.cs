using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Dtos;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Contracts;
using Microsoft.Extensions.Logging;
using System;

namespace MarrowVale.Business.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ILogger<CharacterService> _logger;
        private readonly IPrintService _printService;
        private readonly IGlobalItemsProvider _globalItemsProvider;

        public CharacterService(ILoggerFactory loggerFactory, IPrintService printService, IGlobalItemsProvider globalItemsProvider)
        {
            _logger = loggerFactory.CreateLogger<CharacterService>();
            _printService = printService;
            _globalItemsProvider = globalItemsProvider;
        }

        public void NewCharacter(GameDto gameDto)
        {
            _printService.Print("Now we are going to create a new character for your adventure.");

            PickGender(gameDto);            
            PickName(gameDto);

            Console.WriteLine($"\nName: {gameDto.Player.Name}");
            Console.WriteLine($"Gender: {gameDto.Player.Gender}");
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
            Console.Clear();

            _printService.Print("What do you want you character's name to be?");

            var name = _globalItemsProvider.UpperFirstChar(Console.ReadLine());

            var choiceMade = false;

            while (!choiceMade)
            {
                _printService.Print($"Are you sure you want your character's name to be {name}?  yes | no");

                var choice = Console.ReadLine();

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

        }

        private void PickClass(GameDto gameDto)
        {

        }

        private void PickGender(GameDto gameDto)
        {
            Console.Clear();
          
            _printService.Print("Do you want your character to be a male or a female?");

            var gender = Console.ReadLine();

            if(gender.ToUpper() == "MALE" || gender.ToUpper() == "FEMALE")
            {
                var choiceMade = false;

                while (!choiceMade)
                {
                    _printService.Print($"Are you sure you want your character to be {gender}?  yes | no");

                    var decision = Console.ReadLine();

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
                PickGender(gameDto);
            }
        }


    }
}
