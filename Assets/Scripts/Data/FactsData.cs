using System;
using System.Collections.Generic;
using StaticData.Questions.Facts;

namespace Data
{
    [Serializable]
    public class FactsData
    {
        public List<int> Sets;
        public List<FactsAccessibility> Availability;
        public List<int> ReceivedCards;

        public int Completed;
        public int Victory;
        public int RightAnswers;
        public int WrongAnswers;

        public void InitializeList(int quantity)
        {
            Sets = new List<int>(quantity);
            ReceivedCards = new List<int>(quantity);
            Availability = new List<FactsAccessibility>(quantity);
            
            for (int i = 0; i < quantity; i++)
            {
                Sets.Add(0);
                ReceivedCards.Add(0);
                Availability.Add(FactsAccessibility.Available);
            }
        }

        public void EnlargeList(int itemsToAdd)
        {
            for (int i = 0; i < itemsToAdd; i++)
            {
                Sets.Add(0);
                ReceivedCards.Add(0);
                Availability.Add(FactsAccessibility.Available);
            }
        }
    }
}