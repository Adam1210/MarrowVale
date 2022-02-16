using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Contracts
{
    public interface IEntityToTextFactoryService
    {
        public IEntityToTextService GetEntityToTextService(string entityType);
    }
}
