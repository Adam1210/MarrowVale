using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Services
{
    public class TimeService : ITimeService
    {
        private readonly ILogger _logger;
        public TimeService(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<TimeService>();
        }

        public TimeEnum GetGameTime(Time gameTime)
        {
            var time = gameTime.TimeOfDay;
            //time will be 1 - 24
            if(time >= 4 && time < 8)
            {
                return TimeEnum.EarlyMorning;
            }
            else if(time >= 8 && time < 12)
            {
                return TimeEnum.Morning;
            }
            else if(time >= 12 && time < 17)
            {
                return TimeEnum.Afternoon;
            }
            else if(time >= 17 && time < 21)
            {
                return TimeEnum.Evening;
            }
            else
            {
                return TimeEnum.Night;
            }
        }

        public int GetDaysElapsed(Time gameTime)
        {
            return gameTime.DaysElapsed;
        }
    }
}
