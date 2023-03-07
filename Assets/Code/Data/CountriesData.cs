using System;
using Code.Logic.Countries;

namespace Code.Data
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