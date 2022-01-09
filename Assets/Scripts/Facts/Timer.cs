using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Cubra.Facts
{
    public class Timer : MonoBehaviour
    {
        // Событие по окончанию времени
        public UnityEvent TimeIsOver;

        [Header("Элементы таймера")]
        [SerializeField] private GameObject[] _dashes;

        // Количество секунд
        public int Seconds { get; private set; } = 14;

        // Последняя активная черточка
        private int _number = 7;

        /// <summary>
        /// Обратный отсчет таймера
        /// </summary>
        public IEnumerator Countdown()
        {
            while (Seconds > 0)
            {
                yield return new WaitForSeconds(2f);
                Seconds -= 2;

                _dashes[_number - 1].SetActive(false);
                _number--;
            }

            TimeIsOver?.Invoke();
        }

        /// <summary>
        /// Сброс таймера
        /// </summary>
        public void ResetTimer()
        {
            for (int i = 0; i < _dashes.Length; i++)
                _dashes[i].SetActive(true);

            Seconds = 14;
            _number = 7;
        }
    }
}