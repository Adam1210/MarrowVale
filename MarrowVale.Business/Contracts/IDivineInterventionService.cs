using MarrowVale.Business.Entities.Commands;
using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Contracts
{
    public interface IDivineInterventionService
    {
        public string HandleError(string input, string error, string context = null);
        public string Smite(Npc character, Deity deity = null, string requestedAction = null);
    }
}
