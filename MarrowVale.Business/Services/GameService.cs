using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;

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
        
        private Player Player { get; set; }
        private Game Game { get; set; }
        
        public GameService(ILoggerFactory loggerFactory, IPrintService printService, IGlobalItemsProvider globalItemsProvider,
            IDrawingRepository drawingRepository, IDrawingService drawingService, IGameSetupService gameSetupService, IGameRepository gameRepository)
        {
            _logger = loggerFactory.CreateLogger<GameService>();
            _printService = printService;
            _globalItemsProvider = globalItemsProvider;
            _drawingRepository = drawingRepository;
            _drawingService = drawingService;
            _gameRepository = gameRepository;
            Player = gameSetupService.Setup();
            Game = _gameRepository.LoadGame(Player.GameSaveName);
        }

        public void Start()
        {
                       


        }
    }
}
