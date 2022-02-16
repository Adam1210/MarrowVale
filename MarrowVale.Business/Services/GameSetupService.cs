using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Data.Contracts;
using MarrowVale.Data.Seeder;
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
        private readonly IGameRepository _gameRepository;
        private readonly IEntityGenerator _entityGenerator;
        private readonly IDialogueService _dialogueService;
        private readonly IInputProcessingService _inputProcessingService;
        private readonly IWorldContextService _worldContextService;

        public GameSetupService(ICharacterService characterService, IDrawingRepository drawingRepository, IPrintService printService,
            IDrawingService drawingService, IGameRepository gameRepository, IEntityGenerator entityGenerator, IDialogueService dialogueService, IInputProcessingService inputProcessingService, IWorldContextService worldContextService)
        {
            _characterService = characterService;
            _printService = printService;
            _drawingRepository = drawingRepository;
            _drawingService = drawingService;
            _gameRepository = gameRepository;
            _entityGenerator = entityGenerator;
            _dialogueService = dialogueService;
            _inputProcessingService = inputProcessingService;
            _worldContextService = worldContextService;
        }

        public Player Setup()
        {
            var title = _drawingRepository.GetTitleArt();
            _drawingService.PrintArtCentered(title);

            _printService.TypeCentered("New Game", 8);
            _printService.TypeCentered("Continue");

            var gameType = _printService.ReadInput();

            _entityGenerator.DefaultSettings().Wait();

            if (gameType.ToUpper() == "NEW GAME")
            {
                _entityGenerator.GenerateBuildings();
                _entityGenerator.GenerateCharacters();
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
