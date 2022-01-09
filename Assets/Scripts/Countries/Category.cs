using UnityEngine;
using UnityEngine.UI;
using Firebase.Analytics;
using TMPro;

namespace Cubra.Countries
{
    public class Category : MonoBehaviour
    {
        [Header("Номер категории")]
        [SerializeField] private int _number;

        [Header("Стоимость открытия")]
        [SerializeField] private int _price;

        [Header("Спрайт открытой категории")]
        [SerializeField] private Sprite _sprite;

        [Header("Идентификатор достижения")]
        [SerializeField] private string _achievement;

        // Номер текущего вопроса
        private int currentQuestion;

        private Sets _sets;
        private TextMeshProUGUI _statistics;
        private Image _imageButton;

        private void Awake()
        {
            _sets = Camera.main.GetComponent<Sets>();
            _statistics = GetComponentInChildren<TextMeshProUGUI>();
            _imageButton = GetComponent<Image>();
        }

        private void Start()
        {
            // Получаем номер текущего вопроса по категории
            currentQuestion = _sets.SetsHelper.arraySets[_number];

            if (currentQuestion > 0)
            {
                UpdateCategory();

                // Если категория полностью пройдена
                if (currentQuestion > _sets.Task[_number])
                    GooglePlayServices.UnlockingAchievement(_achievement);
            }
        }

        /// <summary>
        /// Обновление информации по открытой категории
        /// </summary>
        private void UpdateCategory()
        {
            _imageButton.sprite = _sprite;

            _statistics.color = Color.white;
            _statistics.text = currentQuestion - 1 + " /" + _sets.Task[_number];
        }

        /// <summary>
        /// Открытие или приобретение категории
        /// </summary>
        public void OpenCategory()
        {
            if (currentQuestion > 0 && currentQuestion <= _sets.Task[_number])
            {
                Sets.Category = _number;
                Camera.main.GetComponent<TransitionsManager>().GoToScene((int)TransitionsManager.Scenes.CountriesQuestions);
            }
            else if (currentQuestion > _sets.Task[_number])
            {
                Sets.Category = _number;
                Camera.main.GetComponent<TransitionsManager>().GoToScene((int)TransitionsManager.Scenes.CountriesResult);
            }
            else
            {
                if (PlayerPrefs.GetInt("coins") >= _price)
                {
                    PaymentCategory();

                    // Событие (для статистики) по открытию новой категории
                    FirebaseAnalytics.LogEvent("countries_open_category", new Parameter("number", _number + 1));
                }
                else
                {
                    _sets.TextAnimation.enabled = true;
                    _sets.TextAnimation.Rebind();
                }
            }
        }

        /// <summary>
        /// Покупка новой категории
        /// </summary>
        private void PaymentCategory()
        {
            _sets.PointsEarned.ChangeQuantityCoins(-_price);

            // Открываем категорию
            _sets.SetsHelper.arraySets[_number] = 1;
            currentQuestion++;

            PlayerPrefs.SetString("sets", JsonUtility.ToJson(_sets.SetsHelper));

            UpdateCategory();

            _sets.Effect.transform.position = transform.position;
            _sets.Effect.Rebind();
        }
    }
}