using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Entities.Graph;
using MarrowVale.Data.Contracts;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Data.Repositories
{
    public class ContextRepository : BaseRepository<Context>, IContextRepository
    {
        public ContextRepository(IGraphClient graphClient) : base(graphClient)
        {
        }
    }
}
