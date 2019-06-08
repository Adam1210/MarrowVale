using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using System.Collections.Generic;

namespace MarrowVale.Data.Contracts
{
    public interface IClassRepository
    {
        IList<Class> GetClasses();
        Class GetClass(ClassEnum className);
    }
}
