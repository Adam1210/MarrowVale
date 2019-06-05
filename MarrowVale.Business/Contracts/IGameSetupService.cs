using MarrowVale.Business.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Contracts
{
    public interface IGameSetupService
    {
        GameDto Setup();
    }
}
