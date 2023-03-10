using System;

namespace Code.Data
{
    [Serializable]
    public class AdsData
    {
        public bool Activity;

        public event Action AvailabilityChanged;

        public AdsData() =>
            Activity = true;

        public void ChangeAvailability()
        {
            Activity = false;
            AvailabilityChanged?.Invoke();
        }
    }
}