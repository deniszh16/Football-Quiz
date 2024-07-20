using UnityEngine;
using UnityEngine.UI;

namespace DZGames.Football.Countries
{
    public class DeleteLetter : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private AnswerFromLetters _answerFromLetters;
        [SerializeField] private ArrangementOfLetters _arrangementOfLetters;
        
        [Header("Кнопка удаления")]
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(DeleteLastLetter);
            _answerFromLetters.TaskCompleted += HideDeleteButton;
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(DeleteLastLetter);
            _answerFromLetters.TaskCompleted -= HideDeleteButton;
        }

        private void DeleteLastLetter()
        {
            int buttonNumber = _answerFromLetters.GetLastOpenedLetter();
            if (buttonNumber > 0)
            {
                _arrangementOfLetters.RestoreHiddenButton(buttonNumber);
                _answerFromLetters.RemoveLetterFromAnswer();
                _answerFromLetters.ResetTextAnimation();
            }
        }

        public void HideDeleteButton() =>
            gameObject.SetActive(false);
    }
}