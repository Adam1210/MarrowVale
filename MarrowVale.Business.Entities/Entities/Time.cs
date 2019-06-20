using Newtonsoft.Json;

namespace MarrowVale.Business.Entities.Entities
{
    public class Time
    {
        public Time()
        {
            this.TimeOfDay = 14;
            this.DaysElapsed = 0;
        }

        [JsonConstructor]
        private Time(int TimeOfDay, int DaysElapsed)
        {
            this.TimeOfDay = TimeOfDay;
            this.DaysElapsed = DaysElapsed;
        }

        public int TimeOfDay { get; private set; }
        public int DaysElapsed { get; private set; }

        public void IncrementTime()
        {
            TimeOfDay++;
            if (TimeOfDay > 23)
            {
                DaysElapsed++;
                TimeOfDay = 0;
            }
        }
    }
}
