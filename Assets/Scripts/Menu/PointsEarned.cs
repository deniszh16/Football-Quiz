using UnityEngine;
using TMPro;

namespace Cubra
{
    public class PointsEarned : MonoBehaviour
    {
        [Header("Количество очков")]
        [SerializeField] private TextMeshProUGUI _score;

        [Header("Количество монет")]
        [SerializeField] private TextMeshProUGUI _coins;

        private Animator _coinsAnimator;

        private void Awake()
        {
            _coinsAnimator = _coins.gameObject.GetComponentInParent<Animator>();
        }

        private void Start()
        {
            ShowCurrentScore();
            ShowCurrentQuantityCoins();
        }

        /// <summary>
        /// Изменение общего счета
        /// </summary>
        /// <param name="value">значение для изменения</param>
        public void ChangeTotalScore(int value)
        {
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + value);
            ShowCurrentScore();
        }

        /// <summary>
        /// Отображение текущего счета
        /// </summary>
        public void ShowCurrentScore()
        {
            _score.text = PlayerPrefs.GetInt("score").ToString();
        }

        /// <summary>
        /// Изменение количества монет
        /// </summary>
        /// <param name="value">значение для изменения</param>
        public void ChangeQuantityCoins(int value)
        {
            // Подсчет текущего количества монет
            var coins = PlayerPrefs.GetInt("coins") + value;

            PlayerPrefs.SetInt("coins", (coins > 0) ? coins : 0);
            ShowCurrentQuantityCoins(value);
        }

        /// <summary>
        /// Отображение текущего количества монет
        /// </summary>
        /// <param name="lastChange">последнее изменение</param>
        public void ShowCurrentQuantityCoins(int lastChange = 0)
        {
            _coins.text = PlayerPrefs.GetInt("coins").ToString();

            if (lastChange < 0)
                // Проигрываем анимацию вычитания
                _coinsAnimator.Play("Subtraction");
        }
    }
}