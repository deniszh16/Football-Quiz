using System;
using Logic.Countries;

namespace Data
{
    [Serializable]
    public class CountriesData
    {
        public int[] Sets;
        public CategoryAccessibility[] Accessibility;
        public int RightAnswers;
        public int WrongAnswers;
        public int Hints;
        public int Pass;

        public CountriesData()
        {
            Sets = new int[16];
            Accessibility = new CategoryAccessibility[16];
        }
    }
}