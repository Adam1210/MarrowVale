using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Entities.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Data.Contracts
{
    public interface IDeityRepository : IBaseRepository<Deity>
    {
        Deity DefaultDeity();


    }
}
