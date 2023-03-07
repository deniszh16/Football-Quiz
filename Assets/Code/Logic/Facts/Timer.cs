using System.Collections;
using UnityEngine;

namespace Code.Logic.Facts
{
    public class Timer : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Answer _answer;
        
        [Header("Черточки таймера")]
        [SerializeField] private GameObject[] _dashes;

        public bool TimeIsOver =>_timeIsOver;

        private float _seconds = 10.5f;
        private int _amountOfElements = 7;
        private bool _timeIsOver;

        private void Start() =>
            _answer.TaskCompleted += StopCountdown;

        public void StartCountdown() =>
            StartCoroutine(TimerOperation());

        private void StopCountdown() =>
            StopAllCoroutines();

        private IEnumerator TimerOperation()
        {
            while (_seconds > 0)
            {
                yield return new WaitForSeconds(1.5f);
                _seconds -= 1.5f;
                _dashes[_amountOfElements - 1].SetActive(false);
                _amountOfElements--;
            }

            _timeIsOver = true;
            _answer.CheckAnswer(selectedAnswer: !_answer.CurrentAnswer);
        }

        public void ResetTimer()
        {
            foreach (GameObject item in _dashes)
                item.SetActive(true);
            
            _seconds = 10.5f;
            _amountOfElements = 7;
            _timeIsOver = false;
        }

        private void OnDestroy() =>
            _answer.TaskCompleted -= StopCountdown;
    }
}