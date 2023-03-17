using Code.Logic.Legends;
using Code.StaticData.Questions.Facts;
using UnityEngine;

namespace Code.Data.OldProgress
{
    public class MigrationOldProgress
    {
        public void CheckOldProgress(UserProgress userProgress)
        {
            if (PlayerPrefs.HasKey("score"))
                userProgress.Score = PlayerPrefs.GetInt("score");
            
            if (PlayerPrefs.HasKey("coins"))
                userProgress.Coins = PlayerPrefs.GetInt("coins");

            if (PlayerPrefs.HasKey("sets"))
            {
                var categoriesArrays = new CategoriesArrays();
                categoriesArrays = JsonUtility.FromJson<CategoriesArrays>(PlayerPrefs.GetString("sets"));

                for (int i = 0; i < categoriesArrays.arraySets.Length; i++)
                    userProgress.CountriesData.Sets[i] = categoriesArrays.arraySets[i];
            }

            if (PlayerPrefs.HasKey("countries-answer"))
                userProgress.CountriesData.RightAnswers = PlayerPrefs.GetInt("countries-answer");
            
            if (PlayerPrefs.HasKey("countries-error"))
                userProgress.CountriesData.WrongAnswers = PlayerPrefs.GetInt("countries-error");
            
            if (PlayerPrefs.HasKey("countries-tips"))
                userProgress.CountriesData.Hints = PlayerPrefs.GetInt("countries-tips");
            
            if (PlayerPrefs.HasKey("countries-pass"))
                userProgress.CountriesData.Pass = PlayerPrefs.GetInt("countries-pass");

            if (PlayerPrefs.HasKey("facts"))
            {
                var statusesList = new StatusesList();
                statusesList = JsonUtility.FromJson<StatusesList>(PlayerPrefs.GetString("facts"));

                for (int i = 0; i < statusesList.status.Count; i++)
                {
                    string value = statusesList.status[i];
                    
                    if (value == "loss")
                        userProgress.FactsData.Availability[i] = FactsAccessibility.Lost;

                    if (value == "victory")
                        userProgress.FactsData.Availability[i] = FactsAccessibility.Won;
                }
            }

            if (PlayerPrefs.HasKey("facts-quantity"))
                userProgress.FactsData.Completed = PlayerPrefs.GetInt("facts-quantity");
            
            if (PlayerPrefs.HasKey("facts-victory"))
                userProgress.FactsData.Victory = PlayerPrefs.GetInt("facts-victory");
            
            if (PlayerPrefs.HasKey("facts-answer"))
                userProgress.FactsData.RightAnswers = PlayerPrefs.GetInt("facts-answer");
            
            if (PlayerPrefs.HasKey("facts-errors"))
                userProgress.FactsData.WrongAnswers = PlayerPrefs.GetInt("facts-errors");

            if (PlayerPrefs.HasKey("legends"))
            {
                var statusesList = new StatusesList();
                statusesList = JsonUtility.FromJson<StatusesList>(PlayerPrefs.GetString("legends"));

                for (int i = 0; i < statusesList.status.Count; i++)
                {
                    string value = statusesList.status[i];
                    
                    if (value == "yes")
                        userProgress.LegendsData.Legends[i] = LegendStatus.Opened;
                }
            }
            
            if (PlayerPrefs.HasKey("legends-open"))
                userProgress.LegendsData.ReceivedCards = PlayerPrefs.GetInt("legends-open");

            if (PlayerPrefs.HasKey("photo-quiz"))
            {
                var categoriesArrays = new CategoriesArrays();
                categoriesArrays = JsonUtility.FromJson<CategoriesArrays>(PlayerPrefs.GetString("photo-quiz"));

                for (int i = 0; i < categoriesArrays.arraySets.Length; i++)
                    userProgress.PlayersData.Sets[i] = categoriesArrays.arraySets[i];
            }
            
            if (PlayerPrefs.HasKey("photo-quiz-successfully"))
                userProgress.PlayersData.Completed = PlayerPrefs.GetInt("photo-quiz-successfully");
            
            if (PlayerPrefs.HasKey("photo-quiz-answer"))
                userProgress.PlayersData.RightAnswers = PlayerPrefs.GetInt("photo-quiz-answer");
            
            if (PlayerPrefs.HasKey("photo-quiz-errors"))
                userProgress.PlayersData.WrongAnswers = PlayerPrefs.GetInt("photo-quiz-errors");
        }
    }
}