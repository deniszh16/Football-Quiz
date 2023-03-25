using Code.Data;
using Code.Logic.Countries;
using Code.Logic.Legends;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData.Questions.Facts;
using UnityEngine;

namespace Code.Services.MigrationOldProgress
{
    public class MigrationOldProgressService : IMigrationOldProgressService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public MigrationOldProgressService(IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void CheckOldProgress()
        {
            if (_progressService.UserProgress.OldProgress)
            {
                UpdateScore("score");
                UpdateCoins("coins");
                UpdateCountriesSets("sets");
                UpdateCountriesAnswers("countries-answer");
                UpdateCountriesErrors("countries-error");
                UpdateCountriesTips("countries-tips");
                UpdateCountriesPass("countries-pass");
                UpdateFactsSets("facts");
                UpdateFactsQuantity("facts-quantity");
                UpdateFactsVictory("facts-victory");
                UpdateFactsAnswers("facts-answer");
                UpdateFactsErrors("facts-errors");
                UpdateLegends("legends");
                UpdateLegendsOpen("legends-open");
                UpdatePhotoQuiz("photo-quiz");
                UpdatePhotoQuizSuccessfully("photo-quiz-successfully");
                UpdatePhotoQuizAnswers("photo-quiz-answer");
                UpdatePhotoQuizErrors("photo-quiz-errors");
                
                _progressService.UserProgress.OldProgress = false;
                _saveLoadService.SaveProgress();
            }
        }

        private void UpdateScore(string key)
        {
            if (PlayerPrefs.HasKey(key))
                _progressService.UserProgress.Score = PlayerPrefs.GetInt(key);
        }

        private void UpdateCoins(string key)
        {
            if (PlayerPrefs.HasKey(key))
                _progressService.UserProgress.Coins = PlayerPrefs.GetInt(key);
        }

        private void UpdateCountriesSets(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                var sets = PlayerPrefs.GetString(key)?.ToDeserialized<CategoriesArrays>();
                for (int i = 0; i < 16; i++)
                {
                    _progressService.UserProgress.CountriesData.Sets[i] = sets.arraySets[i];

                    if (_progressService.UserProgress.CountriesData.Sets[i] > 0)
                        _progressService.UserProgress.CountriesData.Accessibility[i] = CategoryAccessibility.Available;
                }
            }
        }

        private void UpdateCountriesAnswers(string key)
        {
            if (PlayerPrefs.HasKey(key))
                _progressService.UserProgress.CountriesData.RightAnswers = PlayerPrefs.GetInt(key);
        }

        private void UpdateCountriesErrors(string key)
        {
            if (PlayerPrefs.HasKey(key))
                _progressService.UserProgress.CountriesData.WrongAnswers = PlayerPrefs.GetInt(key);
        }

        private void UpdateCountriesTips(string key)
        {
            if (PlayerPrefs.HasKey(key))
                _progressService.UserProgress.CountriesData.Hints = PlayerPrefs.GetInt(key);
        }

        private void UpdateCountriesPass(string key)
        {
            if (PlayerPrefs.HasKey(key))
                _progressService.UserProgress.CountriesData.Pass = PlayerPrefs.GetInt(key);
        }

        private void UpdateFactsSets(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                var sets = PlayerPrefs.GetString(key)?.ToDeserialized<StatusesList>();
                _progressService.UserProgress.FactsData.InitializeList(sets.status.Count);
                
                for (int i = 0; i < sets.status.Count; i++)
                {
                    string value = sets.status[i];
                    _progressService.UserProgress.FactsData.Availability[i] = value switch
                    {
                        "loss" => FactsAccessibility.Lost,
                        "victory" => FactsAccessibility.Won,
                        _ => FactsAccessibility.Available
                    };
                }
            }
        }

        private void UpdateFactsQuantity(string key)
        {
            if (PlayerPrefs.HasKey(key))
                _progressService.UserProgress.FactsData.Completed = PlayerPrefs.GetInt(key);
        }

        private void UpdateFactsVictory(string key)
        {
            if (PlayerPrefs.HasKey(key))
                _progressService.UserProgress.FactsData.Victory = PlayerPrefs.GetInt(key);
        }

        private void UpdateFactsAnswers(string key)
        {
            if (PlayerPrefs.HasKey(key))
                _progressService.UserProgress.FactsData.RightAnswers = PlayerPrefs.GetInt(key);
        }

        private void UpdateFactsErrors(string key)
        {
            if (PlayerPrefs.HasKey(key))
                _progressService.UserProgress.FactsData.WrongAnswers = PlayerPrefs.GetInt(key);
        }

        private void UpdateLegends(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                var legends = PlayerPrefs.GetString(key)?.ToDeserialized<StatusesList>();
                _progressService.UserProgress.LegendsData.InitializeList(legends.status.Count);
                
                for (int i = 0; i < legends.status.Count; i++)
                {
                    string value = legends.status[i];
                    if (value == "yes") _progressService.UserProgress.LegendsData.Legends[i] = LegendStatus.Opened;
                }
            }
        }

        private void UpdateLegendsOpen(string key)
        {
            if (PlayerPrefs.HasKey(key))
                _progressService.UserProgress.LegendsData.ReceivedCards = PlayerPrefs.GetInt(key);
        }

        private void UpdatePhotoQuiz(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                var sets = PlayerPrefs.GetString(key)?.ToDeserialized<CategoriesArrays>();
                for (int i = 0; i < sets.arraySets.Length; i++)
                    _progressService.UserProgress.PlayersData.Sets[i] = sets.arraySets[i];
            }
        }

        private void UpdatePhotoQuizSuccessfully(string key)
        {
            if (PlayerPrefs.HasKey(key))
                _progressService.UserProgress.PlayersData.Completed = PlayerPrefs.GetInt(key);
        }

        private void UpdatePhotoQuizAnswers(string key)
        {
            if (PlayerPrefs.HasKey(key))
                _progressService.UserProgress.PlayersData.RightAnswers = PlayerPrefs.GetInt(key);
        }

        private void UpdatePhotoQuizErrors(string key)
        {
            if (PlayerPrefs.HasKey(key))
                _progressService.UserProgress.PlayersData.WrongAnswers = PlayerPrefs.GetInt(key);
        }
    }
}