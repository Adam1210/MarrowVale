using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MarrowVale.Data.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ILogger _logger;
        private readonly IAppSettingsProvider _appSettingsProvider;
        private string ClassFile { get; set; }
        public ClassRepository(ILoggerFactory logger, IAppSettingsProvider appSettingsProvider)
        {
            _logger = logger.CreateLogger<ClassRepository>();
            _appSettingsProvider = appSettingsProvider;
            ClassFile = File.ReadAllText(_appSettingsProvider.DataFilesLocation + "\\CLassList.json");
        }

        public IList<Class> GetClasses()
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All

            };

            var classList = JsonConvert.DeserializeObject<List<Class>>(ClassFile, settings);

            if(classList == null)
            {
                _logger.LogError("There are no contents in ClassList.json");
            }

            return classList;
        }

        public Class GetClass(ClassEnum className)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };

            var classList = JsonConvert.DeserializeObject<List<Class>>(ClassFile, settings);

            var characterClass = classList.FirstOrDefault(x => x.Type == className);

            if(characterClass == null)
            {
                _logger.LogError("There is no class by that name in the repository.");
            }

            return characterClass;
        }
    }
}
