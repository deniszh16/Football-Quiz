using System;
using System.Collections.Generic;
using Logic.Legends;

namespace Data
{
    [Serializable]
    public class LegendsData
    {
        public List<LegendStatus> Legends;
        public int ReceivedCards;

        public void InitializeList(int quantity)
        {
            Legends = new List<LegendStatus>(quantity);

            for (int i = 0; i < quantity; i++)
                Legends.Add(LegendStatus.Closed);
        }

        public void EnlargeList(int itemsToAdd)
        {
            for (int i = 0; i < itemsToAdd; i++)
                Legends.Add(LegendStatus.Closed);
        }
    }
}