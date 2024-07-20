using System;

namespace DZGames.Football.Data
{
    [Serializable]
    public class AdsData
    {
        public event Action AvailabilityChanged;
        
        public bool Activity = true;
        public int NumberOfBonuses;
        public int DateDay;

        public void ChangeAvailability()
        {
            Activity = false;
            AvailabilityChanged?.Invoke();
        }

        public void UpdateDailyBonuses(int day)
        {
            DateDay = day;
            NumberOfBonuses = 3;
        }
    }
}