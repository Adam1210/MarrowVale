using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Contracts
{
    public interface ITextGenerator
    {
        public string GenerateCharacterName(Npc person);
        public string GenerateCharacterDescription(Npc person);
        public string GenerateConversationSummary(string conversation);
        public string GenerateEventSummary(string eventText);
        public string GenerateCityName();
        public string GenerateCityDescription();
        public string GenerateRegionName();
        public string GenerateRegionDescription();

    }
}
