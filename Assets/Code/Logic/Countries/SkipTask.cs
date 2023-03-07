namespace Code.Logic.Countries
{
    public class SkipTask : HintBase
    {
        protected override void Start()
        {
            base.Start();
            _button.onClick.AddListener(UseHint);
        }

        protected override void UseHint()
        {
            _progressService.UserProgress.SubtractionCoins(_price);
            _progressService.UserProgress.CountriesData.Pass += 1;
            _saveLoadService.SaveProgress();
            _availability = false;
            
            _hints.SwitchPopup();
            _answerFromLetters.SkipTask();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _button.onClick.RemoveListener(UseHint);
        }
    }
}