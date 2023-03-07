using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Code.Logic.Countries
{
    public class ArrangementOfLetters : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Tasks _tasks;
        [SerializeField] private AnswerFromLetters _answerFromLetters;
        
        [Header("Буквы для задания")]
        [SerializeField] private TextMeshProUGUI[] _letters;

        [Header("Спрайты кнопок")]
        [SerializeField] private Sprite _firstLetter;
        [SerializeField] private Sprite _standardSprite;

        private void Awake() =>
            _answerFromLetters.TaskCompleted += HideAllLetters;

        public void ArrangeLetters()
        {
            for (int i = 0; i < _letters.Length; i++)
            {
                _letters[i].transform.parent.gameObject.SetActive(true);
                _letters[i].transform.parent.gameObject.GetComponent<Image>().sprite = _standardSprite;
                _letters[i].text = _tasks.CountriesStaticData.Questions[_tasks.CurrentQuestion].Letters[i];
            }
        }

        public void RestoreHiddenButton(int number) =>
            _letters[number - 1].transform.parent.gameObject.SetActive(true);

        public void HideSpecifiedButton(int number) =>
            _letters[number].transform.parent.gameObject.SetActive(false);

        private void HideAllLetters() =>
            gameObject.SetActive(false);

        public void ShowAllLetters()
        {
            gameObject.SetActive(true);
            foreach (var letter in _letters)
                letter.transform.parent.gameObject.SetActive(true);
        }

        public void RecolorFirstLetter(int number) =>
            _letters[number].transform.parent.gameObject.GetComponent<Image>().sprite = _firstLetter;

        private void OnDestroy() =>
            _answerFromLetters.TaskCompleted -= HideAllLetters;
    }
}