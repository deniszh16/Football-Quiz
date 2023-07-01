using StaticData.Legends;
using StaticData.Questions.Countries;
using StaticData.Questions.Facts;
using StaticData.Questions.Players;

namespace Services.StaticData
{
    public interface IStaticDataService
    {
        public void LoadQuestionsCountries();
        public CountriesStaticData GetCountriesCategory(int categoryNumber);
        public int GetNumberOfTasksInCountries();
        public void LoadQuestionsFacts();
        public FactsStaticData GetFactsCategory(int categoryNumber);
        public int GetNumberOfFactsCategories();
        public int GetNumberOfTasksInFacts();
        public void LoadQuestionsPlayers();
        public PlayersStaticData GetPlayersCategory(int categoryNumber);
        public int GetNumberOfTasksInPlayers();
        public void LoadLegends();
        public LegendsStaticData GetLegends(int cardNumber);
    }
}