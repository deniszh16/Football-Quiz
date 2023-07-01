using System;

namespace Data
{
    [Serializable]
    public class AdsData
    {
        public bool Activity;
        public int NumberOfBonuses;
        public int DateDay;

        public event Action AvailabilityChanged;

        public AdsData() =>
            Activity = true;

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