using MarrowVale.Common.Contracts;
using Microsoft.Extensions.Logging;

namespace MarrowVale.Common.Providers
{
    public class GlobalItemsProvider : IGlobalItemsProvider
    {
        private readonly ILogger _logger;
        public int WindowWidth { get; }

        public GlobalItemsProvider(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<GlobalItemsProvider>();
            WindowWidth = 120;
        }

        public string UpperFirstChar(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            return char.ToUpper(text[0]) + ((text.Length > 1) ? text.Substring(1).ToLower() : string.Empty);
        }
    }
}
