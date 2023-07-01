using System;
using System.Linq;
using Logic.UI;

namespace Logic.Countries
{
    public class AnswerFromLetters : Answer
    {
        private string[] _answer;
        private string[] _selectedLetters;
        private int[] _numbersOfSelectedLetters;
        private int _amountOpenedLetters;

        private const string FullPlaceholder = "   ";
        private const string ShortPlaceholder = "  ";

        public void CustomizeAnswer()
        {
            GetAnswer();
            GetAnswerLength();
            UpdateAnswerField();
        }

        private void GetAnswer() =>
            _answer = _tasks.CountriesStaticData.Questions[_tasks.CurrentQuestion].Answer;

        private void GetAnswerLength()
        {
            int length = _answer.Length;
            _selectedLetters = new string[length];
            _numbersOfSelectedLetters = new int[length];
            _amountOpenedLetters = 0;
        }

        private void UpdateAnswerField()
        {
            _textAnswer.text = "";

            for (int i = 0; i < _selectedLetters.Length; i++)
            {
                _textAnswer.text += _selectedLetters[i] ?? "*";

                if (i < _selectedLetters.Length - 1)
                    _textAnswer.text += CheckAspectRatio();
            }
        }

        private string CheckAspectRatio() =>
            AspectRatio.Ratio <= 0.46 ? ShortPlaceholder : FullPlaceholder;

        public bool GetPressedButton(string letter, int buttonNumber)
        {
            if (_amountOpenedLetters < _selectedLetters.Length)
            {
                _selectedLetters[_amountOpenedLetters] = letter;
                _numbersOfSelectedLetters[_amountOpenedLetters] = buttonNumber;
                _amountOpenedLetters++;
                UpdateAnswerField();
                return true;
            }

            return false;
        }

        public int GetLastOpenedLetter()
        {
            if (_amountOpenedLetters > 0)
                return _numbersOfSelectedLetters[_amountOpenedLetters - 1];

            return -1;
        }

        public void RemoveLetterFromAnswer()
        {
            _selectedLetters[_amountOpenedLetters - 1] = null;
            _amountOpenedLetters -= 1;
            UpdateAnswerField();
        }

        public void CheckAnswer(bool isSkipped)
        {
            if (_amountOpenedLetters != _answer.Length)
                return;

            if (_answer.SequenceEqual(_selectedLetters))
            {
                _textAnswer.text = _tasks.CountriesStaticData.Questions[_tasks.CurrentQuestion].FullAnswer;
                _textQuestion.text = _tasks.CountriesStaticData.Questions[_tasks.CurrentQuestion].Description;
                
                ReportCorrectAnswer();
                UpdateCategoryProgress(isSkipped);

                if (isSkipped == false)
                    ShowWinningEffect();

                _updateTask.ToggleButton(state: true);
                
                if (_tasks.CurrentQuestion > 6 && _tasks.CurrentQuestion % 7 == 0)
                    _adService.ShowInterstitialAd();
            }
            else
            {
                _textAnswerAnimator.Play(FlashingAnimation);
                _progressService.UserProgress.CountriesData.WrongAnswers += 1;
                _saveLoadService.SaveProgress();
            }
        }

        public int FindFirstLetterOfAnswer() =>
            Array.IndexOf(_tasks.CountriesStaticData.Questions[_tasks.CurrentQuestion].Letters, _answer[0]);

        public int FindLetter(int number) =>
            Array.IndexOf(_answer, _tasks.CountriesStaticData.Questions[_tasks.CurrentQuestion].Letters[number]);

        public void ResetPlayerAnswer()
        {
            Array.Clear(_selectedLetters, 0, _selectedLetters.Length);
            Array.Clear(_numbersOfSelectedLetters, 0, _numbersOfSelectedLetters.Length);
            _amountOpenedLetters = 0;
            
            UpdateAnswerField();
            ResetTextAnimation();
        }

        public void SkipTask()
        {
            _selectedLetters = _answer;
            _amountOpenedLetters = _answer.Length;
            CheckAnswer(isSkipped: true);
        }
    }
}