using MarrowVale.Business.Contracts;
using MarrowVale.Business.Services;
using MarrowVale.Common.Contracts;
using MarrowVale.Common.Evaluator;
using MarrowVale.Common.Providers;
using MarrowVale.Data.Contracts;
using MarrowVale.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Neo4jClient;
using System;

namespace MarrowVale
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Marrow Vale - The Realm of Dragons";
            Console.ForegroundColor = ConsoleColor.Green;

            //Configure DI
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);
            ConfigureRepositories(serviceCollection);
            ConfigureProviders(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            //var inputProcessor = serviceProvider.GetService<IInputProcessingService>();

            //inputProcessor.ProcessInput("Hello");

            //var soundRepo = serviceProvider.GetService<ISoundRepository>();
                     
            //var audio = soundRepo.GetMusicLooping("Title.wav");

            var gameService = serviceProvider.GetService<IGameService>();
            gameService.Start();
            
            //var artRepo = serviceProvider.GetService<IDrawingRepository>();
            //var title = artRepo.GetTitleArt();
            //artRepo.PrintArtCentered(title);

            //var commandRepo = serviceProvider.GetService<ICommandListRepository>();
            //commandRepo.PrintCommands();

            //var classRepo = serviceProvider.GetService<IClassRepository>();
            //classRepo.GetClasses();

            //soundRepo.DisposeMusic(audio);

            Console.ReadKey();
        }

        #region Service Life Times
        /*        
        Transient: services are created every time they are injected or requested.

        Scoped: services are created per scope.In a web application, every web request 
                creates a new separated service scope.That means scoped services are generally created per web request.

        Singleton: services are created per DI container. That generally means that they are created only one time 
                   per application and then used for whole the application life time.

        Good Practices:

        1.    Register your services as transient wherever possible.Because it’s simple to design transient services.
                You generally don’t care about multi-threading and memory leaks and you know the service has a short life.

        2.    Use scoped service lifetime carefully since it can be tricky if you create child service scopes or use these 
                services from a non-web application.

        3.    Use singleton lifetime carefully since then you need to deal with multi-threading and potential memory leak problems.

        4.    Do not depend on a transient or scoped service from a singleton service.Because the transient service becomes a 
              singleton instance when a singleton service injects it and that may cause problems if the transient service is 
              not designed to support such a scenario.ASP.NET Core’s default DI container already throws exceptions in such cases.*/
        #endregion

        private static void ConfigureServices(IServiceCollection services)
        {
            LogLevel logLevel;

            #if DEBUG
                logLevel = LogLevel.Information;
            #elif DEVELOPMENT
                logLevel = LogLevel.Information;
#elif RELEASE
                logLevel = LogLevel.Error;
#endif


            //add services to service collection
            services.AddLogging(configure => configure.AddConsole().SetMinimumLevel(logLevel))
                .AddTransient<ICombatService, CombatService>()
                .AddTransient<ICharacterService, CharacterService>()
                .AddTransient<IInputProcessingService, InputProcessingService>()
                .AddTransient<IPrintService, PrintService>()
                .AddTransient<IGameSetupService, GameSetupService>()
                .AddTransient<IEntityGenerator, EntityGenerator>()
                .AddTransient<ITextGenerator, TextGenerator>()
                .AddTransient<ITimeService, TimeService>()
                .AddSingleton<IGameService, GameService>()
                .AddTransient<IDrawingService, DrawingService>()
                .AddTransient<IDialogueService, DialogueService>()
                .AddSingleton<IWorldContextService, WorldContextService>()
                .AddTransient<ICommandProcessingService, CommandProcessingService>()
                .AddTransient<IPromptService, PromptService>()
                .AddTransient<INpcActionService, NpcActionService>()
                .AddTransient<IDivineInterventionService, DivineInterventionService>()
                .AddTransient<IAiService, AiService>()
                .AddTransient<IAiEvaluationService, AiEvaluationService>();
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            //add repos to service collection
            //use dbcontextscope?
            services.AddTransient<ICommandListRepository, CommandListRepository>()
                .AddTransient<IDrawingRepository, DrawingRepository>()
                .AddTransient<ISoundRepository, SoundRepository>()
                .AddTransient<IClassRepository, ClassRepository>()
                .AddSingleton<IGameRepository, GameRepository>()
                .AddSingleton<IPlayerRepository, PlayerRepository>()
                .AddSingleton<ILocationRepository, LocationRepository>()
                .AddSingleton<IBuildingRepository, BuildingRepository>() 
                .AddSingleton<IRoomRepository, RoomRepository>()
                .AddSingleton<INpcRepository, NpcRepository>()
                .AddSingleton<IDeityRepository, DeityRepository>()
                .AddSingleton<IOpenAiSettingRepository, OpenAiSettingRepository>()
                .AddSingleton<IOpenAiEvaluationRepository, OpenAiEvaluationRepository>()
                .AddSingleton<IPromptRepository, PromptRepository>();
        }

        private static void ConfigureProviders(IServiceCollection services)
        {
            //add providers to service collection
            services.AddTransient<IAppSettingsProvider, AppSettingsProvider>()
                .AddSingleton<IGlobalItemsProvider, GlobalItemsProvider>()
                .AddSingleton<IOpenAiProvider, OpenAiProvider>()
                .AddSingleton<IGraphClient>(context =>
                {
                    var graphClient = new GraphClient(new Uri("http://localhost:7474"),"test","test");
                    graphClient.ConnectAsync().Wait();
                    return graphClient;
                });


        }
    }
}
