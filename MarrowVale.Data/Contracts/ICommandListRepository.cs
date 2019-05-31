using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Data.Contracts
{
    public interface ICommandListRepository
    {
        IList<GameCommand> GetCommands();
        void PrintCommands();
    }
}
