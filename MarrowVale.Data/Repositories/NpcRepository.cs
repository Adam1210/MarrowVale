using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Entities.Graph;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using Neo4jClient;
using Neo4jClient.ApiModels.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Data.Repositories
{
    public class DeityRepository : BaseRepository<Deity>, IDeityRepository
    {
        public DeityRepository(IGraphClient graphClient) : base(graphClient)
        {

        }

        public Deity DefaultDeity()
        {
            var defaultEntityname = "Mithras";
            return GetByName(defaultEntityname).Result;
        }
    }
}
