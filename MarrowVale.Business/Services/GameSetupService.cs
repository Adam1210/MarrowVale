using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Dtos;
using MarrowVale.Business.Entities.Entities;
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
        private readonly IDrawingService _drawingService;
        public GameSetupService(ICharacterService characterService, IDrawingRepository drawingRepository, IPrintService printService,
            IDrawingService drawingService)
        {
            _characterService = characterService;
            _printService = printService;
            _drawingRepository = drawingRepository;
            _drawingService = drawingService;
        }

        public Player Setup()
        {
            var title = _drawingRepository.GetTitleArt();
            _drawingService.PrintArtCentered(title);

            _printService.TypeCentered("New Game", 8);
            _printService.TypeCentered("Continue");

            var gameType = _printService.ReadInput();

            var playerDto = new PlayerDto();
            var player = new Player();

            if(gameType.ToUpper() == "NEW GAME")
            {
                player = newGame(playerDto);
            }
            else if(gameType.ToUpper() == "CONTINUE")
            {
                player = continueGame(playerDto);
            }
            else
            {
                _printService.Print("You must choose to start a New Game or Continue a saved game. Type your choice.");
                Thread.Sleep(4000);
                player = runSetup(playerDto);
            }

            return player;
        }

        private Player newGame(PlayerDto playerDto)
        {
            var player = _characterService.NewCharacter(playerDto);

            return player;
        }

        private Player continueGame(PlayerDto playerDto)
        {
            //display list of characters
            //load character chosen
            var player = _characterService.LoadCharacter(playerDto);
            return player;
        }

        private Player runSetup(PlayerDto playerDto)
        {
            _printService.ClearConsole();

            var title = _drawingRepository.GetTitleArt();
            _drawingService.PrintArtCentered(title);

            _printService.TypeCentered("New Game");
            _printService.TypeCentered("Continue");

            var gameType = _printService.ReadInput();

            var player = new Player();

            if (gameType.ToUpper() == "NEW GAME")
            {
                player = newGame(playerDto);
            }
            else if (gameType.ToUpper() == "CONTINUE")
            {
                player = continueGame(playerDto);
            }
            else
            {
                _printService.Print("You must choose to start a New Game or Continue a saved game. Type your choice.");
                player = runSetup(playerDto);
            }

            return player;
        }
    }
}
