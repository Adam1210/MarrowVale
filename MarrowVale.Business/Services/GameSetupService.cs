using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Data.Contracts;
using System.Threading;

namespace MarrowVale.Business.Services
{
    public class GameSetupService : IGameSetupService
    {
        private readonly ICharacterService _characterService;
        private readonly IPrintService _printService;
        private readonly IDrawingRepository _drawingRepository;
        private readonly IDrawingService _drawingService;
        private readonly IGameRepository _gameRepository;

        public GameSetupService(ICharacterService characterService, IDrawingRepository drawingRepository, IPrintService printService,
            IDrawingService drawingService, IGameRepository gameRepository)
        {
            _characterService = characterService;
            _printService = printService;
            _drawingRepository = drawingRepository;
            _drawingService = drawingService;
            _gameRepository = gameRepository;
        }

        public Player Setup()
        {
            var title = _drawingRepository.GetTitleArt();
            _drawingService.PrintArtCentered(title);

            _printService.TypeCentered("New Game", 8);
            _printService.TypeCentered("Continue");

            var gameType = _printService.ReadInput();
            
            if(gameType.ToUpper() == "NEW GAME")
            {
                return newGame();
            }
            else if(gameType.ToUpper() == "CONTINUE")
            {
                return continueGame();
            }
            else
            {
                _printService.Print("You must choose to start a New Game or Continue a saved game. Type your choice.");
                Thread.Sleep(4000);
                return Setup();
            }
        }

        private Player newGame()
        {
            var player = _characterService.NewCharacter();
            var game = new Game();
            
            _gameRepository.SaveGame(game, null, player.GameSaveName);

            return player;
        }

        private Player continueGame()
        {
            //display list of characters
            //load character chosen
            var player = _characterService.LoadCharacter();
            return player;
        }

    }
}
