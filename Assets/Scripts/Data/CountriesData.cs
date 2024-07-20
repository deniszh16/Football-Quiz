using System;
using DZGames.Football.Countries;

namespace DZGames.Football.Data
{
    [Serializable]
    public class CountriesData
    {
        public int[] Sets = new int[16];
        public CategoryAccessibility[] Accessibility = new CategoryAccessibility[16];
        public int RightAnswers;
        public int WrongAnswers;
        public int Hints;
        public int Pass;
    }
}