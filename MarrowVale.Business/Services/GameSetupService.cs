using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Dtos;
using MarrowVale.Data.Contracts;
using System;
using System.Threading;

namespace MarrowVale.Business.Services
{
    public class GameSetupService : IGameSetupService
    {
        private readonly ICharacterService _characterService;
        private readonly IPrintService _printService;
        private readonly IDrawingRepository _drawingRepository;
        public GameSetupService(ICharacterService characterService, IDrawingRepository drawingRepository, IPrintService printService)
        {
            _characterService = characterService;
            _printService = printService;
            _drawingRepository = drawingRepository;
        }

        public GameDto Setup()
        {
            var title = _drawingRepository.GetTitleArt();
            _drawingRepository.PrintArtCentered(title);

            _printService.TypeCentered("New Game", 8);
            _printService.TypeCentered("Continue");

            var gameType = Console.ReadLine();

            var game = new GameDto();

            if(gameType.ToUpper() == "NEW GAME")
            {
                newGame(game);
            }
            else if(gameType.ToUpper() == "CONTINUE")
            {
                continueGame(game);
            }
            else
            {
                _printService.Print("You must choose to start a New Game or Continue a saved game. Type your choice.");
                Thread.Sleep(4000);
                runSetup(game);
            }

            return game;
        }

        private GameDto newGame(GameDto gameDto)
        {
            _characterService.NewCharacter(gameDto);
            return gameDto;
        }

        private GameDto continueGame(GameDto gameDto)
        {
            //display list of characters
            //load character chosen
            _characterService.LoadCharacter(gameDto);
            return gameDto;
        }

        private GameDto runSetup(GameDto gameDto)
        {
            Console.Clear();

            var title = _drawingRepository.GetTitleArt();
            _drawingRepository.PrintArtCentered(title);

            _printService.TypeCentered("New Game");
            _printService.TypeCentered("Continue");

            var gameType = Console.ReadLine();

            var game = new GameDto();

            if (gameType.ToUpper() == "NEW GAME")
            {
                newGame(game);
            }
            else if (gameType.ToUpper() == "CONTINUE")
            {
                continueGame(game);
            }
            else
            {
                _printService.Print("You must choose to start a New Game or Continue a saved game. Type your choice.");
                runSetup(game);
            }

            return game;
        }
    }
}
