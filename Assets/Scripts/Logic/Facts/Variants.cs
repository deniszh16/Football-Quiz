using UnityEngine;

namespace DZGames.Football.Facts
{
    public class Variants : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Answer _answer;
        
        [Header("Кнопки вариантов")]
        [SerializeField] private GameObject _trueAnswer;
        [SerializeField] private GameObject _falseAnswer;

        private void Start() =>
            _answer.TaskCompleted += HideButtons;

        private void OnDestroy() =>
            _answer.TaskCompleted -= HideButtons;

        public void ShowButtons()
        {
            _trueAnswer.SetActive(true);
            _falseAnswer.SetActive(true);
        }

        private void HideButtons()
        {
            _trueAnswer.SetActive(false);
            _falseAnswer.SetActive(false);
        }
    }
}