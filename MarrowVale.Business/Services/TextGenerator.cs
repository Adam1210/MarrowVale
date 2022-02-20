using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Contracts;
using MarrowVale.Common.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Services
{
    public class TextGenerator : ITextGenerator
    {
        private readonly IAiService _aiService;

        public TextGenerator(IAiService aiService)
        {
            _aiService = aiService;
        }

        public string GenerateCharacterDescription(Npc person)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("This generates a random description off a character based off gender and occupation");
            sb.Append("");
            sb.Append($"Gender:{person.Gender}");
            sb.Append($"Occupation:{person.Occupation}");
            sb.Append($"Description:");
            return _aiService.Complete(sb.ToString()).Result;
        }

        public string GenerateCharacterName(Npc person)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("This generates a name based off race and gender");
            sb.Append("");
            sb.Append($"Race:{person.Race.ToString()}");
            sb.Append($"Gender:{person.Gender}");
            sb.Append($"Name:");
            return _aiService.Complete(sb.ToString()).Result;
        }

        public string GenerateCityDescription()
        {
            throw new NotImplementedException();
        }

        public string GenerateCityName()
        {
            throw new NotImplementedException();
        }

        public string GenerateConversationSummary(string conversation)
        {
            throw new NotImplementedException();
        }

        public string GenerateEventSummary(string eventText)
        {
            throw new NotImplementedException();
        }

        public string GenerateRegionDescription()
        {
            throw new NotImplementedException();
        }

        public string GenerateRegionName()
        {
            throw new NotImplementedException();
        }
    }
}
