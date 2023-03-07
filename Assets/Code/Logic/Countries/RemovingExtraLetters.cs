namespace Code.Logic.Countries
{
    public class RemovingExtraLetters : HintBase
    {
        public bool ButtonsWereHidden { get; private set; }

        protected override void Start()
        {
            base.Start();
            _button.onClick.AddListener(UseHint);
        }

        protected override void UseHint()
        {
            _progressService.UserProgress.SubtractionCoins(_price);
            _progressService.UserProgress.CountriesData.Hints += 1;
            _saveLoadService.SaveProgress();
            _availability = false;
            _hints.SwitchPopup();
            
            _arrangementOfLetters.ShowAllLetters();
            _answerFromLetters.ResetPlayerAnswer();

            ButtonsWereHidden = true;
            FindAndDisableButtons();
        }

        public void FindAndDisableButtons()
        {
            for (int i = 0; i < 12; i++)
            {
                int letter = _answerFromLetters.FindLetter(i);
                if (letter < 0) _arrangementOfLetters.HideSpecifiedButton(i);
            }
        }

        public void ResetHintUsage() =>
            ButtonsWereHidden = false;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _button.onClick.RemoveListener(UseHint);
        }
    }
}