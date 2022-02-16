using MarrowVale.Common.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Common.Providers
{
    public class AppSettingsProvider : IAppSettingsProvider
    {
        private readonly ILogger _logger;
        private IConfigurationRoot Configuration => BuildConfiguration();
        public string DataFilesLocation => Configuration.GetSection("MySettings").GetSection("DataFilesLocation").Value;
        public string SoundFilesLocation => Configuration.GetSection("MySettings").GetSection("SoundFilesLocation").Value;
        public bool AskForRating => Convert.ToBoolean(Configuration.GetSection("MySettings").GetSection("AskForRating").Value);
        public string BasePromptFileLocation => Configuration.GetSection("PromptFileLocations").GetSection("BaseLocation").Value;
        public string PromptEvaluationLocation => Configuration.GetSection("PromptFileLocations").GetSection("Evaluation").Value;
        public string PromptInterpretCommandLocation => Configuration.GetSection("PromptFileLocations").GetSection("InterpretCommand").Value;
        public string PromptSummaryLocation => Configuration.GetSection("PromptFileLocations").GetSection("Summary").Value;
        public string DialogueFilesLocation => Configuration.GetSection("PromptFileLocations").GetSection("Dialogue").Value;
        public string CombatFileLocation => Configuration.GetSection("PromptFileLocations").GetSection("Combat").Value;
        public string DivineInterventionLocation => Configuration.GetSection("PromptFileLocations").GetSection("DivineIntervention").Value;
        //stored as user secret
        public string OpenAiKey => Configuration["OpenAiKey"];
        public string Neo4jUser => Configuration.GetSection("SystemSettings").GetSection("Neo4jUser").Value;
        public string Neo4jPassword => Configuration.GetSection("SystemSettings").GetSection("Neo4jPassword").Value;
        public string Neo4jUrl => Configuration.GetSection("SystemSettings").GetSection("Neo4jUrl").Value;
        public int MainMenuWait => Convert.ToInt32(Configuration.GetSection("TextTimings").GetSection("MainMenu").Value);
        public int CharacterCreateWait => Convert.ToInt32(Configuration.GetSection("TextTimings").GetSection("CharacterCreation").Value);
        public int TypeSpeed => Convert.ToInt32(Configuration.GetSection("TextTimings").GetSection("TypeSpeed").Value);

        public AppSettingsProvider(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<AppSettingsProvider>();
        }

        private IConfigurationRoot BuildConfiguration()
        {
            var env = "";
            #if DEBUG
                env = "Debug";
            #elif DEVELOPMENT
                env = "Development";
            #elif RELEASE
                env = "Release";
            #endif



            var directory = Environment.CurrentDirectory;

            var builder = new ConfigurationBuilder()
            .SetBasePath(directory)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{env}.json", true, true)
            .AddUserSecrets<AppSettingsProvider>();        

            var configuration = builder.Build();
           
            return configuration;
        }
    }
}
