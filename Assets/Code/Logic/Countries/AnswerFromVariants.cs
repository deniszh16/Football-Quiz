namespace Code.Logic.Countries
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
                
                if (_tasks.CurrentQuestion > 6 && _tasks.CurrentQuestion % 7 == 0)
                    _adService.ShowInterstitialAd();
            }
            else
            {
                _progressService.UserProgress.CountriesData.WrongAnswers += 1;
                _progressService.UserProgress.SubtractionCoins(20);
                _saveLoadService.SaveProgress();
            }
        }
    }
}