using MarrowVale.Business.Entities.Commands;
using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Contracts
{
    public interface ICommandProcessingService
    {
        public string ShowWorld( Player player);
        string ProcessCommand(Command command, Player player);
    }
}
