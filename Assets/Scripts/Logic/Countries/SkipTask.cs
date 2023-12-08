namespace Logic.Countries
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
            _progressService.GetUserProgress.SubtractionCoins(_price);
            _progressService.GetUserProgress.CountriesData.Pass += 1;
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