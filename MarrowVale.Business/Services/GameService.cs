using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarrowVale.Business.Services
{
    public class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly IPrintService _printService;
        private readonly IGlobalItemsProvider _globalItemsProvider;
        private readonly IDrawingRepository _drawingRepository;
        private readonly IDrawingService _drawingService;
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IInputProcessingService _inputProcessingService;
        private readonly IEntityGenerator _entityGenerator;

        private readonly ICombatService _combatService;
        private readonly ICommandProcessingService _commandProccesingService;
        
        private Player Player { get; set; }
        private Game Game { get; set; }
        
        public GameService(ILoggerFactory loggerFactory, IPrintService printService, IGlobalItemsProvider globalItemsProvider,
            IDrawingRepository drawingRepository, IDrawingService drawingService, IGameSetupService gameSetupService, IGameRepository gameRepository,
            ICombatService combatService, IInputProcessingService inputProcessingService, IPlayerRepository playerRepository, IEntityGenerator entityGenerator, ICommandProcessingService commandProccesingService)
        {
            _logger = loggerFactory.CreateLogger<GameService>();
            _printService = printService;
            _globalItemsProvider = globalItemsProvider;
            _drawingRepository = drawingRepository;
            _drawingService = drawingService;
            _gameRepository = gameRepository;
            _combatService = combatService;
            _playerRepository = playerRepository;
            _inputProcessingService = inputProcessingService;
            _entityGenerator = entityGenerator;
            _commandProccesingService = commandProccesingService;
            Player = gameSetupService.Setup();
            Game = _gameRepository.LoadGame(Player.GameSaveName);
        }

        public void Start()
        {
            Console.Clear();
            _printService.Type(_commandProccesingService.ShowWorld(Player));
            while (true)
            {
                var playerInput = _printService.ReadInput();
                if (playerInput == "QUIT")
                    break;

                var command = _inputProcessingService.ProcessInput(playerInput, "", Player);
                _printService.Type(_commandProccesingService.ProcessCommand(command, Player));

            }
        }

        private CancellationTokenSource startGameClock()
        {
            var tokenSource = new CancellationTokenSource();
            var cancellableTask = Task.Run(() =>
            {
                while (true) {
                    if (tokenSource.IsCancellationRequested)
                    {
                        break;
                    }
                    Game.GameTime.IncrementTime();
                    Thread.Sleep(5 * 60 * 1000);
                }
            }, tokenSource.Token);

            return tokenSource;
        }
    }
}
