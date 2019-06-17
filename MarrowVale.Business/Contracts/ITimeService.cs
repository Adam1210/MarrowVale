using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Contracts
{
    public interface ITimeService
    {
        TimeEnum GetGameTime(Time time);
    }
}
