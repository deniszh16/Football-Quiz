namespace DZGames.Football.Countries
{
    public class AnswerFromVariants : Answer
    {
        private const string EmptyAnswer = "*   *   *";

        public void UpdateAnswerField() =>
            _textAnswer.text = EmptyAnswer;

        public void CheckAnswer(int answerNumber)
        {
            if (_tasks.CountriesStaticData.Questions[_tasks.CurrentQuestion].NumberOfAnswer == answerNumber)
            {
                _textAnswer.text = _tasks.CountriesStaticData.Questions[_tasks.CurrentQuestion].FullAnswer;
                _textQuestion.text = _tasks.CountriesStaticData.Questions[_tasks.CurrentQuestion].Description;
                
                ReportCorrectAnswer();
                UpdateCategoryProgress(isSkipped: false);
                
                ShowWinningEffect();
                _updateTask.ToggleButton(state: true);

                if (_tasks.CurrentQuestion > 8 && _tasks.CurrentQuestion % 9 == 0)
                {
                    if (_progressService.GetUserProgress.AdsData.Activity)
                        _adService.ShowInterstitialAd();
                }
            }
            else
            {
                _progressService.GetUserProgress.CountriesData.WrongAnswers += 1;
                _progressService.GetUserProgress.SubtractionCoins(20);
                _saveLoadService.SaveProgress();
            }
        }
    }
}