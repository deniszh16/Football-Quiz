using UnityEngine;

namespace DZGames.Football.Countries
{
    public class FirstLetter : HintBase
    {
        [Header("Подсказка удаления букв")]
        [SerializeField] private RemovingExtraLetters _removingExtraLetters;
        
        protected override void Start()
        {
            base.Start();
            _button.onClick.AddListener(UseHint);
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            _button.onClick.RemoveListener(UseHint);
        }

        protected override void UseHint()
        {
            _progressService.GetUserProgress.SubtractionCoins(_price);
            _progressService.GetUserProgress.CountriesData.Hints += 1;
            _saveLoadService.SaveProgress();
            _availability = false;
            _hints.SwitchPopup();

            var letter = _answerFromLetters.FindFirstLetterOfAnswer();
            if (letter > 0)
            {
                _arrangementOfLetters.ShowAllLetters();
                _arrangementOfLetters.RecolorFirstLetter(number: letter);
                _answerFromLetters.ResetPlayerAnswer();
                CheckHidingButtons();
            }
        }

        private void CheckHidingButtons()
        {
            if (_removingExtraLetters.ButtonsWereHidden)
                _removingExtraLetters.FindAndDisableButtons();
        }
    }
}